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
    }

    private void Update()
    {
        viewText.text = viewersCount.ToString();
    }
}
