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

    public Pen selectedPen;
    public bool isPenAvailable = true;

    public GameObject hand;
    
    void Start()
    {
        SelectPen(defaultPen);
        SelectNextPaper();
    }

    void Update()
    {
        if (Input.GetKeyDown("0"))
        {
            SelectPen(defaultPen);
        }
        else if (Input.GetKeyDown("1"))
        {
            SelectPen(pens[0]);
        }
        else if (Input.GetKeyDown("2"))
        {
            SelectPen(pens[1]);
        }
        else if (Input.GetKeyDown("3"))
        {
            SelectPen(pens[2]);
        }
        else if (Input.GetKeyDown("4"))
        {
            SelectPen(markers[0]);
        }
    }

    void SelectNextPaper()
    {
        currentPaperIndex = Random.Range(0, availablePapers.Length);
        
        currentPaper = Instantiate(availablePapers[currentPaperIndex], new Vector3(0, 5.01f, -3.08f), Quaternion.identity);
        currentPaper.gm = this;
    }

    void SelectPen(Pen pen)
    {
        isPenAvailable = false;
        
        selectedPen = pen;
        Debug.Log("Selected pen: " + pen);
        // ... animate ... 
        
        isPenAvailable = true;
    }
}
