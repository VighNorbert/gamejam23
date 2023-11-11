using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomEventController : MonoBehaviour
{
    public GameObject blackOut;
    public GameObject light;
    public bool isSetOff = false;

    public bool setOff = false;

    private Image image;
    public float fadeDuration = 1f;

    public static RandomEventController instance;
    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        image = blackOut.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ChatController.instance.currentRandomEvent != -1)
        {
            if (ChatController.instance.currentRandomEvent == 0)
            {
                if (light.active == false && isSetOff == true)
                {
                    ChatController.instance.currentRandomEvent = -1;
                }
                else
                {
                    light.SetActive(false);
                    isSetOff = false;
                }
            }
            else if (ChatController.instance.currentRandomEvent == 1)
            {
                blackOut.SetActive(true);
                Debug.Log("PICA" + setOff + " " + ChatController.instance.currentRandomEvent);
                
                StartCoroutine(FadeInOut());

            }
        } 
    }

    IEnumerator FadeInOut()
    {
        Debug.Log(setOff);
        while (!setOff)
        {
            // Fade Out
            if (image.color.a == 0)
            {
                yield return FadeImage(0f, 1f);

            }
            else if (image.color.a == 1)
            {
                yield return FadeImage(1f, 0f);

            }
            yield return new WaitForSeconds(2f);
        }
        setOff = false;
    }

    IEnumerator FadeImage(float startAlpha, float targetAlpha)
    {
        float timer = 0f;

        while (timer < fadeDuration && !setOff)
        {
            // Calculate the alpha value based on the lerp factor
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, timer / fadeDuration);

            // Update the image color with the new alpha
            image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);

            // Increment the timer
            timer += Time.deltaTime;

            // Wait for the next frame
            yield return null;
        }

        // Ensure the final alpha value is set
        image.color = new Color(image.color.r, image.color.g, image.color.b, targetAlpha);
    }
}
