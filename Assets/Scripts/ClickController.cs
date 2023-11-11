using System.Collections;
using UnityEngine;

public class ClickController : MonoBehaviour
{
    private int currTask = 0;
    public GameObject light;

    int spln = 0;
    int nowpreTommyho = 0;

    public bool leftTrig = true;
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
                    Debug.Log("Clicked on: " + hit.collider.gameObject.name);
                    if (hit.collider.gameObject.name == "Lamp")
                    {
                        light.SetActive(!light.active);
                        if (currTask == 10)
                        {
                            ViewersCount.AddViewers(Random.Range(5, 21) * spln);
                            DonationsCount.AddDonation(Random.Range(30, 51));

                            spln += 1;
                        }
                    }
                    if (hit.collider.gameObject.name == "Plant")
                    {
                        //Animation
                        //Change cursor
                        if (currTask == 6)
                        {
                            ViewersCount.AddViewers(Random.Range(5, 11) * spln);
                            DonationsCount.AddDonation(Random.Range(30, 51));
                            spln += 1;
                        }
                    }
                    else if (hit.collider.gameObject.name == "Drink")
                    {
                        //Change cursor
                        //Animation
                        if (currTask == 0)
                        {
                            spln += 1;
                            ViewersCount.AddViewers(Random.Range(5, 11) * spln);
                            DonationsCount.AddDonation(Random.Range(30, 51));
                        }
                    }
                    else if (hit.collider.gameObject.name == "Redbull")
                    {
                        //Change cursor
                        //Animation
                        if (currTask == 9)
                        {
                            spln += 1;
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
                        if (currTask == 4)
                        {
                            spln += 1;
                            ViewersCount.AddViewers(Random.Range(5, 11) * spln);
                            DonationsCount.AddDonation(Random.Range(30, 51));

                        }
                        if (currTask == 5)
                        {
                            spln += 1;
                            ViewersCount.AddViewers(Random.Range(5, 11) * spln);
                            DonationsCount.AddDonation(Random.Range(30, 51));

                        }
                    }
                    else if (hit.collider.gameObject.name == "Right")
                    {
                        SoundManager.instance.NextSong();
                        if (currTask == 3)
                        {
                            spln += 1;
                            ViewersCount.AddViewers(Random.Range(5, 11) * spln);
                            DonationsCount.AddDonation(Random.Range(30, 51));

                        }
                    }
                    else if (hit.collider.gameObject.name == "Up")
                    {
                        SoundManager.instance.HigherVolume();
                        if (currTask == 2)
                        {
                            spln += 1;
                            ViewersCount.AddViewers(Random.Range(5, 11) * spln);
                            DonationsCount.AddDonation(Random.Range(30, 51));

                        }
                    }
                    else if (hit.collider.gameObject.name == "Down")
                    {
                        SoundManager.instance.LowerVolume();
                        if (currTask == 3)
                        {
                            spln += 1;
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
