using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour
{

    public Image image;
    public void OnButtonClick()
    {
        if (ChatController.instance.currentSideTask == 7 && image.color == Color.yellow)
        {
            ClickController.instance.spln += 1;
            ChatController.instance.currentSideTask = -1;
            ViewersCount.AddViewers(Random.Range(5, 11) * ClickController.instance.spln);
            DonationsCount.AddDonation(Random.Range(30, 51));
            image.color = Color.white;
        }
    }
}
