using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DonationController : MonoBehaviour
{

    public Image myImage;
    public TextMeshProUGUI myTextMeshPro;
    public bool alreadyInProgress = false;

    public float animationDuration = 2f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartC(string mine)
    {
        if (!alreadyInProgress)
        {
            alreadyInProgress = true;
            StartCoroutine(Ine(mine));

        }

    }

    IEnumerator Ine(string mine)
    {
        float alpha = 0;
        while (myImage.color.a < 1)
        {
            alpha += 0.05f;
            myImage.color = new Color(myImage.color.r, myImage.color.g, myImage.color.b, alpha);
            yield return null;
        }
        myTextMeshPro.text = mine;
        yield return new WaitForSeconds(2.5f);

        alpha = 1;
        while (myImage.color.a > 0)
        {
            alpha -= 0.05f;
            myImage.color = new Color(myImage.color.r, myImage.color.g, myImage.color.b, alpha);
            yield return null;
        }

        alreadyInProgress = false;
        myTextMeshPro.text = "";
    }

}
