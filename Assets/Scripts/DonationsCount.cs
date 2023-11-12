using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DonationsCount : MonoBehaviour
{
    public static float donationsCount = 200;
    public TextMeshProUGUI donationText;

    public GameManager gm;
    
    public GameObject image;

    public float donationGoal = 250;
    
    public static void AddDonation(float addCount)
    {
        donationsCount += addCount;
    }

    public static void SubtractDonation(float substractCount)
    {
        donationsCount -= substractCount;
    }

    private void Update()
    {
        if (donationsCount > donationGoal)
        {
            donationsCount = donationsCount - donationGoal;
            foreach (Pen pen in gm.pens)
            {
                if (!pen.gameObject.activeSelf)
                {
                    pen.gameObject.SetActive(true);
                    break;
                }
            }
        }
        image.transform.localScale = new Vector3((float)(donationsCount / donationGoal), image.transform.localScale.y, image.transform.localScale.z);
        donationText.text = donationsCount.ToString() + " $";
    }
}
