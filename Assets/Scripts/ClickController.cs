using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ClickController : MonoBehaviour
{
    private int currTask = 0;
    public GameObject light;

    public int spln = 0;
    int nowpreTommyho = 0;

    public bool leftTrig = true;
    public GameObject blackOut;

    public static ClickController instance;


    private void Awake()
    {
        instance = this;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null)
                {
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
                        SFXManager.instance.playWatering();
                        //Animation
                        //Change cursor
                        if (ChatController.instance.currentSideTask == 6)
                        {
                            CompleteTask();
                        }
                    }
                    else if (hit.collider.gameObject.name == "Drink")
                    {
                        SFXManager.instance.playDrink();
                        //Change cursor
                        //Animation
                        if (ChatController.instance.currentSideTask == 0)
                        {
                            CompleteTask();
                        }
                    }
                    else if (hit.collider.gameObject.name == "RedBull")
                    {
                        //Change cursor
                        //Animation
                        //blackOut.SetActive(false);
                       // RandomEventController.instance.setOff = true;
                        //blackOut.GetComponent<Image>().color = new Color(blackOut.GetComponent<Image>().color.r, blackOut.GetComponent<Image>().color.g, blackOut.GetComponent<Image>().color.b, 0);
                        SFXManager.instance.playDrink();
                        if (ChatController.instance.currentSideTask == 0)
                        {
                            CompleteTask();
                        }
                    }
                    else if (hit.collider.gameObject.name == "Left")
                    {
                        SFXManager.instance.playClick();
                        if (leftTrig)
                        {
                            SoundManager.instance.PreviousSong();
                            leftTrig = false;
                            if (ChatController.instance.currentSideTask == 4)
                            {
                                spln += 1;
                                ChatController.instance.currentSideTask = -1;
                                if (spln > 20)
                                {
                                    ViewersCount.AddViewers(Random.Range(5, 11));    
                                }
                                else
                                {
                                    ViewersCount.AddViewers(Random.Range(5, 11) * spln);   
                                }
                                DonationsCount.AddDonation(Random.Range(5, 30));

                            }
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
                        SFXManager.instance.playClick();
                        SoundManager.instance.NextSong();
                        if (ChatController.instance.currentSideTask == 3)
                        {
                            CompleteTask();

                        }
                    }
                    else if (hit.collider.gameObject.name == "Up")
                    {
                        SFXManager.instance.playClick();
                        SoundManager.instance.HigherVolume();
                        if (ChatController.instance.currentSideTask == 1)
                        {
                            CompleteTask();
                        }
                    }
                    else if (hit.collider.gameObject.name == "Down")
                    {
                        SFXManager.instance.playClick();
                        SoundManager.instance.LowerVolume();
                        if (ChatController.instance.currentSideTask == 2)
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
        if (spln > 20)
        {
            ViewersCount.AddViewers(Random.Range(5, 11));    
        }
        else
        {
            ViewersCount.AddViewers(Random.Range(5, 11) * spln);
        }
    }

    void CompleteRandomEvent()
    {
        ChatController.instance.currentRandomEvent = -1;
        ChatController.instance.setDonation();
        spln += 1;
        if (spln > 20)
        {
            ViewersCount.AddViewers(Random.Range(5, 11));    
        }
        else
        {
            ViewersCount.AddViewers(Random.Range(5, 11) * spln);   
        }
    }

    IEnumerator ChangeBoolAfterDelay()
    {
        leftTrig = true;
        yield return new WaitForSeconds(2f);
        leftTrig = false;
    }
}
