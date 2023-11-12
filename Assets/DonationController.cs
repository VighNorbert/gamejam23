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
            Debug.Log(alpha);
            alpha += 0.05f;
            myImage.color = new Color(myImage.color.r, myImage.color.g, myImage.color.b, alpha);
            yield return null;
        }
        myTextMeshPro.text = mine;
        yield return new WaitForSeconds(2.5f);

        alpha = 1;
        while (myImage.color.a > 0)
        {
            Debug.Log(alpha);
            alpha -= 0.05f;
            myImage.color = new Color(myImage.color.r, myImage.color.g, myImage.color.b, alpha);
            yield return null;
        }

        alreadyInProgress = false;
    }

    IEnumerator Ine2()
    {
        float alpha = 0;
        while (myImage.color.a > 0)
        {
            Debug.Log(alpha);
            alpha -= 0.001f;
            myImage.color = new Color(myImage.color.r, myImage.color.g, myImage.color.b, alpha);
            yield return null;
        }
    }

    IEnumerator AlphaAnimation()
    {
        while (true)
        {
            float timer = 0f;

            // Fade In (0 to 1)
            while (timer < animationDuration / 2f)
            {
                float alpha = Mathf.Lerp(0f, 1f, timer / (animationDuration / 2f));
                myImage.color = new Color(myImage.color.r, myImage.color.g, myImage.color.b, alpha);

                timer += Time.deltaTime;
                yield return null;
            }

            // Wait for 2 seconds
            yield return new WaitForSeconds(2f);

            timer = 0f;

            // Fade Out (1 to 0)
            while (timer < animationDuration / 2f)
            {
                float alpha = Mathf.Lerp(1f, 0f, timer / (animationDuration / 2f));
                myImage.color = new Color(myImage.color.r, myImage.color.g, myImage.color.b, alpha);

                timer += Time.deltaTime;
                yield return null;
            }

            // Wait for 2 seconds before repeating
            yield return new WaitForSeconds(2f);
        }
    }
}
