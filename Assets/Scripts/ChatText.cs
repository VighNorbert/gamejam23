using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChatText : MonoBehaviour
{
    public TextMeshProUGUI text;

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
    // Start is called before the first frame update
    void Start()
    {
        text.text = GetRandomFanWord();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    string GetRandomFanWord()
    {
        // Get a random fan word from the list
        int randomIndex = Random.Range(0, fanMessages.Count);
        return fanMessages[randomIndex];
    }
}
