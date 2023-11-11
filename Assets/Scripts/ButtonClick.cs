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
            ClickController.instance.CompleteTask();
            ChatController.instance.CompleteTask();
        }
    }
}
