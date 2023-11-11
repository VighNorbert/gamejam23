using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DonationsCount : MonoBehaviour
{
    public static int donationsCount = 0;
    public TextMeshProUGUI donationText;
    
    public static void AddDonation(int addCount)
    {
        donationsCount += addCount;
    }

    public static void SubtractDonation(int substractCount)
    {
        donationsCount -= substractCount;
    }

    private void Update()
    {
        donationText.text = donationsCount.ToString() + " $";
    }
}
