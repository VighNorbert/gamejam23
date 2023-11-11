using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int views = 0;

    public Paper[] availablePapers;
    
    public Paper currentPaper;
    public int currentPaperIndex;
    
    public ChatController chatController;

    public Pen defaultPen;
    public Pen[] pens;
    public Pen[] markers;

    public Pen nextPen;

    public Pen selectedPen;
    public bool isPenAvailable = true;

    public Hand hand;
    public Vector3 originalHandPosition;

    public Animator handAnimator;

    public ViewersCount vc;
    
    void Start()
    {
        handAnimator = GetComponent<Animator>();
        SelectPen(defaultPen);
        SelectNextPaper();
    }

    void Update()
    {
        if (isPenAvailable)
        {
            if (Input.GetKeyDown("1") && !selectedPen.Equals(defaultPen))
            {
                ChangePen(defaultPen);
            }
            else if (Input.GetKeyDown("2") && !selectedPen.Equals(pens[0]))
            {
                ChangePen(pens[0]);
            }
            else if (Input.GetKeyDown("3") && !selectedPen.Equals(pens[1]))
            {
                ChangePen(pens[1]);
            }
            else if (Input.GetKeyDown("4") && !selectedPen.Equals(pens[2]))
            {
                ChangePen(pens[2]);
            }
            else if (Input.GetKeyDown("5") && !selectedPen.Equals(markers[0]))
            {
                ChangePen(markers[0]);
            }
        }
    }

    void SelectNextPaper()
    {
        currentPaperIndex = Random.Range(0, availablePapers.Length);
        
        currentPaper = Instantiate(availablePapers[currentPaperIndex], new Vector3(0, 5.01f, -3.08f), Quaternion.identity);
        currentPaper.gm = this;
    }

    void ChangePen(Pen pen)
    {
        StartCoroutine(PenChanger(pen));
    }

    void SelectPen(Pen pen)
    {
        if (hand.transform.position.Equals(new Vector3(0, 5, -3.47f)))
        {
            Debug.Log("Setting first hand position");
            originalHandPosition = new Vector3(-0.872f, 5, -1.939f);
        }
        else
        {
            originalHandPosition = hand.transform.position;
        }

        isPenAvailable = false;
        
        selectedPen = pen;
        Debug.Log("Selected pen: " + pen);
        StartCoroutine(PenSelector());
    }
    
    IEnumerator PenSelector()
    {
        handAnimator.SetTrigger("PickUpPen");
        Vector3 startPos = hand.transform.position;
        float total = Vector3.Distance(startPos, selectedPen.transform.position) * .6f;
        float start = Time.time;
        while (true)
        {
            float t = (Time.time - start) / total;
            hand.transform.position = Vector3.Lerp(startPos, selectedPen.transform.position, t);
            if (Time.time - start >= total)
            {
                hand.transform.position = selectedPen.transform.position;
                break;
            }
            yield return new WaitForSeconds(0.001f);
        }
    }
    
    IEnumerator PenChanger(Pen pen)
    {
        originalHandPosition = hand.transform.position;
        handAnimator.SetTrigger("SwitchPen");
        nextPen = pen;
        Vector3 startPos = hand.transform.position;
        float total = Vector3.Distance(startPos, selectedPen.transform.position) * .6f;
        float start = Time.time;
        while (true)
        {
            float t = (Time.time - start) / total;
            hand.transform.position = Vector3.Lerp(startPos, selectedPen.transform.position, t);
            if (Time.time - start >= total)
            {
                hand.transform.position = selectedPen.transform.position;
                break;
            }
            yield return new WaitForSeconds(0.001f);
        }
    }

    public void OnPenPutDown()
    {
        isPenAvailable = false;
            
        selectedPen.GetComponentInChildren<MeshRenderer>().enabled = true;
        hand.pen.SetActive(false);
        hand.marker.SetActive(false);
        hand.highlighter.SetActive(false);
        selectedPen = nextPen;
        nextPen = null;
        StartCoroutine(PenSelector());
    }

    public void OnPenPickedUp()
    {
        selectedPen.GetComponentInChildren<MeshRenderer>().enabled = false;
        if (selectedPen.isForWriting)
        {
            hand.pen.SetActive(true);
        }
        else if (selectedPen.isMarker)
        {
            hand.marker.SetActive(true);
        }
        else if (selectedPen.isForHighlighting)
        {
            hand.highlighter.SetActive(true);
        }
        
        StartCoroutine(HandReturnToPosition());
    }

    public void OnPenFinished()
    {
        // isPenAvailable = true;
    }

    IEnumerator HandReturnToPosition()
    {
        Vector3 startPos = hand.transform.position;
        float total = Vector3.Distance(startPos, originalHandPosition) * .6f;
        float start = Time.time;
        while (true)
        {
            float t = (Time.time - start) / total;
            hand.transform.position = Vector3.Lerp(startPos, originalHandPosition, t);
            if (Time.time - start >= total)
            {
                hand.transform.position = originalHandPosition;
                isPenAvailable = true;
                break;
            }
            yield return new WaitForSeconds(0.001f);
        }
    }

    public IEnumerator AnimateWrite(Vector3 endPosition, bool writing)
    {
        Vector3 startPos = hand.transform.position;
        float total = writing ? Vector3.Distance(startPos, endPosition) * 5f : Vector3.Distance(startPos, endPosition) * .5f;

        Debug.Log(total);
        float start = Time.time;
        while (true)
        {
            float t = (Time.time - start) / total;
            hand.transform.position = Vector3.Lerp(startPos, endPosition, t);
            if (Time.time - start >= total)
            {
                hand.transform.position = endPosition;
                Debug.Log("Ending write");
                if (writing) handAnimator.SetTrigger("EndWriting");
                isPenAvailable = true;
                break;
            }
            yield return new WaitForSeconds(0.001f);
        }
    }
}
