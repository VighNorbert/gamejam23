using System.Collections;
using UnityEngine;

public class ClickController : MonoBehaviour
{
    private int currTask = 0;
    public GameObject light;

    public int spln = 0;
    int nowpreTommyho = 0;

    public bool leftTrig = true;

    public static ClickController instance;


    private void Awake()
    {
        instance = this;
    }
    void Update()
    {
        //Debug.Log(ChatController.instance.currentRandomEvent + " T " + ChatController.instance.currentSideTask);

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null)
                {
                    Debug.Log("Clicked on: " + hit.collider.gameObject.name);
                    if (hit.collider.gameObject.name == "Lamp")
                    {
                        light.SetActive(!light.active);
                        if (ChatController.instance.currentRandomEvent == 0)
                        {
                            CompleteRandomEvent();
                        }
                    }
                    if (hit.collider.gameObject.name == "Plant")
                    {
                        //Animation
                        //Change cursor
                        if (ChatController.instance.currentSideTask == 6)
                        {
                            CompleteTask();
                        }
                    }
                    else if (hit.collider.gameObject.name == "Drink")
                    {
                        //Change cursor
                        //Animation
                        if (ChatController.instance.currentSideTask == 0)
                        {
                            CompleteTask();
                        }
                    }
                    else if (hit.collider.gameObject.name == "Redbull")
                    {
                        //Change cursor
                        //Animation
                        if (ChatController.instance.currentRandomEvent == 1)
                        {
                            CompleteRandomEvent();

                        }
                    }
                    else if (hit.collider.gameObject.name == "Left")
                    {
                        if (leftTrig)
                        {
                            SoundManager.instance.PreviousSong();
                            leftTrig = false;
                        }
                        else
                        {
                            StartCoroutine(ChangeBoolAfterDelay());
                            spln += 1;
                            SoundManager.instance.ResetSong();
                        }
                        if (ChatController.instance.currentSideTask == 4)
                        {
                            CompleteTask();

                        }
                        if (ChatController.instance.currentSideTask == 5)
                        {
                            CompleteTask();

                        }
                    }
                    else if (hit.collider.gameObject.name == "Right")
                    {
                        SoundManager.instance.NextSong();
                        if (ChatController.instance.currentSideTask == 3)
                        {
                            CompleteTask();

                        }
                    }
                    else if (hit.collider.gameObject.name == "Up")
                    {
                        SoundManager.instance.HigherVolume();
                        if (ChatController.instance.currentSideTask == 2)
                        {
                            CompleteTask();

                        }
                    }
                    else if (hit.collider.gameObject.name == "Down")
                    {
                        SoundManager.instance.LowerVolume();
                        if (ChatController.instance.currentSideTask == 3)
                        {
                            CompleteTask();
                        }
                    }
                }
            }
        }
    }

    public void CompleteTask()
    {
        ChatController.instance.CompleteTask();
        ChatController.instance.currentSideTask = -1;
        ChatController.instance.setDonation();
        spln += 1;
        ViewersCount.AddViewers(Random.Range(5, 21) * spln);
    }

    void CompleteRandomEvent()
    {
        ChatController.instance.currentRandomEvent = -1;
        ChatController.instance.setDonation();
        spln += 1;
        ViewersCount.AddViewers(Random.Range(5, 21) * spln);
    }

    IEnumerator ChangeBoolAfterDelay()
    {
        leftTrig = true;
        yield return new WaitForSeconds(2f);
        leftTrig = false;
    }
}
