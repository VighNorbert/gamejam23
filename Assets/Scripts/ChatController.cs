using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatController : MonoBehaviour
{
    public GameObject chatMessagePrefab;

    // Start is called before the first frame update
    void Start()
    {
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
            if (transform.childCount > 5)
            {
                Destroy(transform.GetChild(0).gameObject, 0f);
            }
            GameObject chatMessage = Instantiate(chatMessagePrefab, transform.position, Quaternion.identity);
            chatMessage.transform.parent = transform;

            ViewersCount.AddViewers(20);

            yield return new WaitForSeconds(1f);
        }
    }
}
