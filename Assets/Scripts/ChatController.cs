using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ChatController : MonoBehaviour
{
    
    public Image[] texts;
    
    public int currentSideTask = -1;
    public int nextSideTaskIn;
    
    public int currentRandomEvent = -1;
    public int nextRandomEventIn;
    
    private float random;
    private int sideTaskIndex = -1;

    private int lastMessage = 0;

    private int minNextSideTask = 5;
    private int maxNextSideTask = 8;
    private int minNextRandomEvent = 10;
    private int maxNextRandomEvent = 15;
    
    private List<string> messages;
    private List<string> fanMessages = new List<string>
    {
        "Hello, streamer!",
        "I love this content!",
        "Great gameplay!",
        "Can you play my favorite game?",
        "Greetings from [ViewerName]!",
        "This stream is awesome!",
        "Keep up the good work!",
        "You're my favorite streamer!",
        "Hello from Slovakia!",
        "I just subscribed!",
    };

    private List<(string message, int weight)> sideTaskMessages = new List<(string, int)>
    {
        ("Aren't you thirsty?", 1),
        ("Can you turn the volume up?", 1),
        ("Can you turn the volume down?", 1),
        ("This song sucks, give us another one", 4),
        ("Can you play that last song again?", 1),
        ("Can you start the song from the beginning?", 1),
        ("That flower looks sad, give it some water!", 1),
        ("Notice me senpai!", 5),
        ("Pet the cat!", 3)
    };

    private int totalWeights = 18;
    

    // Start is called before the first frame update
    void Start()
    {
        messages = new List<string>();
        nextSideTaskIn = Random.Range(minNextSideTask, maxNextSideTask);  // generate after how many messages next side task will trigger
        nextRandomEventIn = Random.Range(minNextRandomEvent, maxNextRandomEvent);   // generate after how many messages next random event will trigger
        StartCoroutine(TextMessageCreator());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator TextMessageCreator()
    {
        while (true)
        {
            // when side task is no longer in chat, reset
            if (sideTaskIndex >= 5)
            {
                currentSideTask = -1;
                sideTaskIndex = -1;
            }

            // if side task is not set, generate with probability
            if (currentSideTask < 0)
            {
                if (nextSideTaskIn == 0)
                {
                    currentSideTask = Random.Range(0, totalWeights);
                    int currWeight = 0;
                    for (int i = 0; i < sideTaskMessages.Count; i++)
                    {
                        currWeight += sideTaskMessages[i].weight;
                        if (currWeight >= currentSideTask)
                        {
                            messages.Insert(0, sideTaskMessages[i].message);
                            currentSideTask = i;
                            sideTaskIndex = 0;
                            break;
                        }
                    }
                    nextSideTaskIn = Random.Range(minNextSideTask, maxNextSideTask);
                }
                else
                {
                    messages.Insert(0, GetRandomFanWord());
                    nextSideTaskIn -= 1;
                }
            }
            // if side task is set, just do message
            else
            {
                sideTaskIndex += 1;
                messages.Insert(0, GetRandomFanWord());   
            }
            ViewersCount.AddViewers(Random.Range(-1, 3));
            
            // first 6 messages need to appear
            if (messages.Count < 7)
            {
                texts[messages.Count - 1].gameObject.SetActive(true);
            }
            // then we just add at bottom
            else
            {
                messages.RemoveAt(6);
            }
            
            // set texts and color of all message bubbles
            for (int i=0; i<messages.Count; i++)
            {
                texts[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = messages[i];
                if (sideTaskIndex >= 0 && i==sideTaskIndex)
                {
                    texts[i].color = Color.yellow;
                }
                else
                {
                    texts[i].color = Color.white;
                }
            }

            // if no random event is active
            if (currentRandomEvent < 0)
            {
                // if its time to trigger random event, generate it
                if (nextRandomEventIn == 0)
                {
                    currentRandomEvent = Random.Range(0, 1);
                    nextRandomEventIn = Random.Range(minNextRandomEvent, maxNextRandomEvent); 
                }
                else
                {
                    nextRandomEventIn -= 1;
                }
            }

            if (ViewersCount.viewersCount > 200)
            {
                random = Random.Range(1.0f, 1.5f); // next message time
            }
            else
            {
                random = Random.Range(1.0f, 2.5f); // next message time   
            }
            yield return new WaitForSeconds(random);
        }
    }
    
    string GetRandomFanWord()
    {
        // Get a random fan word from the list
        int randomIndex = Random.Range(0, fanMessages.Count);
        while (randomIndex == lastMessage)
        {
            randomIndex = Random.Range(0, fanMessages.Count);
        }
        lastMessage = randomIndex;
        return fanMessages[randomIndex];
    }
}
