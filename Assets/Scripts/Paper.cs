using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Paper : MonoBehaviour
{
    public GameManager gm;
    
    public GameObject wordsContainer;
    public GameObject[] _words;

    public string correctPaperText;
    public TextMeshProUGUI paperTextTMP;
    public int writtenProgress = 0;
    
    public string writtenText;
    public TextMeshProUGUI writtenTextTMP;
    public Pen nextColor = null;

    private bool _colorChanged = false;
    private Pen _colorRemoved = null;

    private bool lastWasBr = false;

    private int combo = 1;

    private bool textComplete = false;
    
    void Start()
    {
        _words = new GameObject[wordsContainer.transform.childCount];
        
        for (int i = 0; i < wordsContainer.transform.childCount; i++)
        {
            GameObject childObject = wordsContainer.transform.GetChild(i).gameObject;
            _words[i] = childObject;
        }

        string[] correctWords = correctPaperText.Split(" ");

        bool isBold = false;
        bool isColored = false;
        bool decoloredNow = false;
        
        for (int i = 0; i < correctWords.Length; i++)
        {
            string word = correctWords[i];
            if (word.Contains("</b>"))
            {
                isBold = false;
                decoloredNow = false;
                continue;
            }
            if (word.Contains("<b>") || isBold)
            {
                isBold = true;
                decoloredNow = false;
                continue;
            }

            if (!isColored && Random.Range(0, 10) < 3 && !decoloredNow)
            {
                correctWords[i] = "<color=" + gm.pens[Random.Range(0, gm.pens.Length)].color + ">" + word;
                isColored = true;
            }
            else if (isColored && Random.Range(0, 10) < 7)
            {
                correctWords[i] = word + "</color>";
                isColored = false;
                decoloredNow = true;
                continue;
            }
            decoloredNow = false;
        }
        
        correctPaperText = String.Join(' ', correctWords);

        string templateCorrect = correctPaperText;
        foreach (Pen pen in gm.pens)
        {
            templateCorrect = templateCorrect.Replace(pen.color, pen.fadedColor);
        }
        foreach (Pen pen in gm.markers)
        {
            templateCorrect = templateCorrect.Replace(pen.color, pen.fadedColor);
        }
        paperTextTMP.text = templateCorrect;
        
        // MoveHand();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit))
            {
                if (_words.Contains(hit.collider.gameObject))
                {
                    GameObject word = hit.collider.gameObject;
                    word.GetComponent<MeshRenderer>().enabled = true;
                }
            }
        }

        if (gm.isPenAvailable) {
            string nextChar = GetNextChar();
        
        
            if (nextChar == " " && Input.GetKeyDown("space"))
            {
                // add letter
                if (_colorRemoved)
                {
                    writtenText += _colorRemoved.GetTerminationString();
                    _colorRemoved = null;
                }
                if (_colorChanged)
                {
                    writtenText += nextColor.GetDefinitionString();
                    _colorChanged = false;
                }
                // add space
                writtenText += " ";
                writtenTextTMP.SetText(writtenText + " ");
                writtenProgress += 1;
                MoveHand();
            }
            else if (nextChar != " " && Input.GetKeyDown(nextChar.ToLower()) && IsCorrectColor())
            {
                // add letter
                if (_colorRemoved)
                {
                    writtenText += _colorRemoved.GetTerminationString();
                    _colorRemoved = null;
                }
                if (_colorChanged)
                {
                    writtenText += nextColor.GetDefinitionString();
                    _colorChanged = false;
                }

                if (writtenText.Length >= 4 && writtenText.Substring(writtenText.Length - 4, 4) == "br> ")
                {
                    writtenText = writtenText.Substring(0, writtenText.Length - 1);
                }
                writtenText += nextChar;
                writtenTextTMP.SetText(writtenText + " ");
                combo += 1;
                ViewersCount.AddViewers(combo * 10);
                if (!lastWasBr)
                {
                    gm.handAnimator.SetTrigger("Write");
                }

                gm.isPenAvailable = false;
                writtenProgress += 1;
                MoveHand();
            }
            else if (nextChar != " " && Input.anyKeyDown)
            {
                for (KeyCode key = KeyCode.A; key <= KeyCode.Z; key++)
                {
                    if (Input.GetKeyDown(key))
                    {
                        // bad character
                        combo = 1;
                        break; // Exit the loop after the first match
                    }
                }
            }
        }
        
    }

    bool IsCorrectColor()
    {
        return (gm.selectedPen == gm.defaultPen && nextColor is null) || nextColor == gm.selectedPen;
    }

    string GetNextChar()
    {
        if (writtenProgress >= correctPaperText.Length)
        {
            if (!textComplete)
            {
                textComplete = true;
                gm.SelectNextPaper();
            }
            return " ";
        }
        if (correctPaperText[writtenProgress] == '<')
        {
            if (correctPaperText.Substring(writtenProgress, 4) == "<br>")
            {
                writtenText += "<br>";
                writtenTextTMP.SetText(writtenText + " ");
                writtenProgress += 4;
                MoveHand();
                lastWasBr = true;
            }
            if (correctPaperText.Substring(writtenProgress, 3) == "<b>")
            {
                foreach (Pen p in gm.markers)
                {
                    if (p.color == correctPaperText.Substring(writtenProgress + 10, 7))
                    {
                        nextColor = p;
                        writtenProgress += 18;
                        _colorChanged = true;
                        break;
                    }
                }
            }
            else if (correctPaperText.Substring(writtenProgress, 3) == "<co")
            {
                foreach (Pen p in gm.pens)
                {
                    if (p.color == correctPaperText.Substring(writtenProgress + 7, 7))
                    {
                        nextColor = p;
                        writtenProgress += 15;
                        _colorChanged = true;
                        break;
                    }
                }
            }
            else if (correctPaperText.Substring(writtenProgress, 12) == "</color></b>")
            {
                _colorRemoved = nextColor;
                nextColor = null;
                writtenProgress += 12;
            }
            else if (correctPaperText.Substring(writtenProgress,  8) == "</color>")
            {
                _colorRemoved = nextColor;
                nextColor = null;
                writtenProgress += 8;
            }
        }
        if (writtenProgress >= correctPaperText.Length)
        {
            if (!textComplete)
            {
                textComplete = true;
                gm.SelectNextPaper();
            }
            return " ";
        }

        return correctPaperText[writtenProgress].ToString();
    }

    void MoveHand()
    {
        TMP_TextInfo textInfo = writtenTextTMP.textInfo;
        if (textInfo.characterCount == 0)
        {
            gm.handAnimator.SetTrigger("EndWriting");
            Debug.Log("Pen available");
            gm.isPenAvailable = true;
            return;
        }
        TMP_CharacterInfo lastCharacterInfo = textInfo.characterInfo[textInfo.characterCount - 1];
        

        // Get the position of the right side of the last character in local space
        Vector3 rightSideLocalPosition = new Vector3(lastCharacterInfo.bottomRight.x, lastCharacterInfo.baseLine, lastCharacterInfo.bottomRight.z);
        // rightSideLocalPosition += lastCharacterInfo.baseLine - lastCharacterInfo.topLeft;

        // Convert local position to world position
        Vector3 rightSideWorldPosition = writtenTextTMP.transform.TransformPoint(rightSideLocalPosition) + new Vector3(0.1f, 0f, 0f);
        
        StartCoroutine(gm.AnimateWrite(rightSideWorldPosition, !lastWasBr));
        lastWasBr = false;
    }
}
