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
    
    
    
    
    void Start()
    {
        SelectNextPaper();
    }

    void Update()
    {
        
    }

    void SelectNextPaper()
    {
        currentPaperIndex = Random.Range(0, availablePapers.Length);
        
        currentPaper = Instantiate(availablePapers[currentPaperIndex], new Vector3(0, 5.01f, -3.08f), Quaternion.identity);
        
    }
}
