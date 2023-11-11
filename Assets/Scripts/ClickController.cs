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
                            ViewersCount.AddViewers(Random.Range(5, 21) * spln);
                            DonationsCount.AddDonation(Random.Range(30, 51));
                            ChatController.instance.currentRandomEvent = -1;
                            spln += 1;
                        }
                    }
                    if (hit.collider.gameObject.name == "Plant")
                    {
                        //Animation
                        //Change cursor
                        if (ChatController.instance.currentSideTask == 6)
                        {
                            ChatController.instance.currentSideTask = -1;
                            ViewersCount.AddViewers(Random.Range(5, 11) * spln);
                            DonationsCount.AddDonation(Random.Range(30, 51));
                            spln += 1;
                        }
                    }
                    else if (hit.collider.gameObject.name == "Drink")
                    {
                        //Change cursor
                        //Animation
                        if (ChatController.instance.currentSideTask == 0)
                        {
                            spln += 1;
                            ChatController.instance.currentSideTask = -1;
                            ViewersCount.AddViewers(Random.Range(5, 11) * spln);
                            DonationsCount.AddDonation(Random.Range(30, 51));
                        }
                    }
                    else if (hit.collider.gameObject.name == "Redbull")
                    {
                        //Change cursor
                        //Animation
                        if (ChatController.instance.currentRandomEvent == 1)
                        {
                            spln += 1;
                            ChatController.instance.currentRandomEvent = -1;
                            ViewersCount.AddViewers(Random.Range(5, 11) * spln);
                            DonationsCount.AddDonation(Random.Range(30, 51));

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
                            spln += 1;
                            ChatController.instance.currentSideTask = -1;
                            ViewersCount.AddViewers(Random.Range(5, 11) * spln);
                            DonationsCount.AddDonation(Random.Range(30, 51));

                        }
                        if (ChatController.instance.currentSideTask == 5)
                        {
                            spln += 1;
                            ChatController.instance.currentSideTask = -1;
                            ViewersCount.AddViewers(Random.Range(5, 11) * spln);
                            DonationsCount.AddDonation(Random.Range(30, 51));

                        }
                    }
                    else if (hit.collider.gameObject.name == "Right")
                    {
                        SoundManager.instance.NextSong();
                        if (ChatController.instance.currentSideTask == 3)
                        {
                            spln += 1;
                            ChatController.instance.currentSideTask = -1;
                            ViewersCount.AddViewers(Random.Range(5, 11) * spln);
                            DonationsCount.AddDonation(Random.Range(30, 51));

                        }
                    }
                    else if (hit.collider.gameObject.name == "Up")
                    {
                        SoundManager.instance.HigherVolume();
                        if (ChatController.instance.currentSideTask == 2)
                        {
                            spln += 1;
                            ChatController.instance.currentSideTask = -1;
                            ViewersCount.AddViewers(Random.Range(5, 11) * spln);
                            DonationsCount.AddDonation(Random.Range(30, 51));

                        }
                    }
                    else if (hit.collider.gameObject.name == "Down")
                    {
                        SoundManager.instance.LowerVolume();
                        if (ChatController.instance.currentSideTask == 3)
                        {
                            spln += 1;
                            ChatController.instance.currentSideTask = -1;
                            ViewersCount.AddViewers(Random.Range(5, 11) * spln);
                            DonationsCount.AddDonation(Random.Range(30, 51));

                        }
                    }
                }
            }
        }
    }

    IEnumerator ChangeBoolAfterDelay()
    {
        leftTrig = true;
        yield return new WaitForSeconds(2f);
        leftTrig = false;
    }
}
