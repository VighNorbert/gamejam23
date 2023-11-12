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
    public Image donation;
    
    public int currentSideTask = -1;
    public int nextSideTaskIn;
    
    public int currentRandomEvent = -1;
    public int nextRandomEventIn;

    public int nextDonationIn = -1;
    private int donationIndex = -1;
    private int donated;
    
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
        "Hello, I am new to the stream!",
        "I love this content!",
        "Great relax gameplay!",
        "Can you play my favorite game? This game!",
        "Greetings from DevTeam!",
        "This stream is awesome!",
        "Keep up the good work and stay relaxed!",
        "You're my favorite streamer!",
        "Hello from Slovakia!",
        "I just subscribed!",
        "Remember to stay relaxed",
        "Happy to be here",
        "Will you come to the next GameJam?",
        "Ahhh, good times",
        "I am watching from the day one !!!",
        ":):)",
        "Planning a game night, any recommendations?",
        "Like this game ? Tell the devs",
        "I don't know what to write here",
        "Just joined the notification squad!",
        "Curious, what's your all-time favorite game?",
        "Quick break to say hi, back to work now!",
        "Sending positive vibes to the chat!",
        "Just caught the stream – what did I miss?",
        "Love the details !",
        "Secret message from progammer who wrote this",
        "I am very tired, but need to program this game",
        "Having Fun!",
        "Chill out",
        "Just spamming here",
        "So tired, I don't know what am I writing",
        "NIiiiiiice!",
        "Woaaaah, cool !",
        "How long did you sleep ?",
        "What are you writing ?",
        "Where are the markers from ?",
        "I am going! Bye now !",
        "I have a school tommorow, must do presentation:/",
        "Nice Desk!",
        "You have a really nice setup!",
        "If you notice this! Tell the creators!",
    };

    private List<(List<string> messages, int weight)> sideTaskMessages = new List<(List<string>, int)>
    {
        (new List<string> {"Aren't you thirsty?", "Don't forget to hydrate!", "Remember to drink some water!"}, 1),
        (new List<string> {"Can you turn the volume up?", "Turn the music up!"}, 1),
        (new List<string> {"Can you turn the volume down?", "Could you lower the volume?"}, 1),
        (new List<string> {"This song is bad, give us another one", "Not feeling it—new song, please.", "This song? Nah. Another one?", "Not vibing with this. New one?"}, 1),
        (new List<string> {"Can you play that last song again?", "Could you give us that last song again?", "Replay the last track, if you can."}, 1),
        (new List<string> {"Can you start the song from the beginning?", "Give us that song from the start!", "Play it from the beginning again!"}, 1),
        (new List<string> {"That flower looks sad, give it some water!", "Notice the sad flower? Time to hydrate it!", "Spotting a sad flower; show it some water care!"}, 1),
        (new List<string> {"Notice me senpai!", "Could you give my message a thumbs up? It's my BDay!", "Do you even read the chat?"}, 1),
        (new List<string> {"Pet the cat!", "Show your cat some love!"}, 1)
    };

    private int totalWeights = 9;

    public static ChatController instance;

    private void Awake()
    {
        instance = this;
    }


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
            if (sideTaskIndex >= 5 || currentSideTask == -1)
            {
                currentSideTask = -1;
                sideTaskIndex = -1;
            }

            // if side task is not set, generate with probability
            if (currentSideTask < 0)
            {
                if (nextSideTaskIn == 0 && donationIndex == -1)
                {
                    currentSideTask = Random.Range(0, totalWeights);
                    int currWeight = 0;
                    for (int i = 0; i < sideTaskMessages.Count; i++)
                    {
                        currWeight += sideTaskMessages[i].weight;
                        if (currWeight >= currentSideTask)
                        {
                            int message = Random.Range(0, sideTaskMessages[i].messages.Count);
                            messages.Insert(0, sideTaskMessages[i].messages[message]);
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
                    if (donationIndex == -1)
                    {
                        nextSideTaskIn -= 1;   
                    }
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
            
            // donations handling
            if (nextDonationIn == 0)
            {
                donated = Random.Range(30, 51);
                DonationsCount.AddDonation(donated);
                donationIndex = 0;
                nextDonationIn = -1;
            }
            else if (nextDonationIn > 0)
            {
                nextDonationIn -= 1;
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
            
            if (donationIndex >= 0 && donationIndex < 6)
            {
                texts[donationIndex].color = Color.magenta;
                texts[donationIndex].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text =
                    "Donation: You have received $" + donated;
                donationIndex += 1;
            }
            else
            {
                donationIndex = -1;
            }

            // if no random event is active
            if (currentRandomEvent < 0)
            {
                // if its time to trigger random event, generate it
                if (nextRandomEventIn == 0)
                {
                    currentRandomEvent = Random.Range(4, 7);
                    nextRandomEventIn = Random.Range(minNextRandomEvent, maxNextRandomEvent); 
                }
                else
                {
                    nextRandomEventIn -= 1;
                }
            }

            if (ViewersCount.viewersCount > 1000)
            {
                random = Random.Range(3.5f, 10.0f); // next message time
            }
            else
            {
                random = Random.Range(4.0f, 10.0f); // next message time   
            }
            yield return new WaitForSeconds(random);
        }
    }

    public void CompleteTask()
    {
        texts[sideTaskIndex].color = Color.white;
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

    public void setDonation()
    {
        nextDonationIn = Random.Range(2, 4);
    }
}
