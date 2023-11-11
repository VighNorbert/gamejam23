using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ViewersCount : MonoBehaviour
{
    public static int viewersCount = 0;
    public TextMeshProUGUI viewText;
    
    public static void AddViewers(int addCount)
    {
        viewersCount += addCount;
        if (viewersCount < 0)
        {
            viewersCount = 0;
        }
    }

    private void Update()
    {
        viewText.text = viewersCount.ToString();
    }
}
