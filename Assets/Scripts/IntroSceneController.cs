using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroSceneController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play()
    {
        SceneManager.LoadScene(sceneBuildIndex:1);
    }
    public void Tutorial()
    {
        SceneManager.LoadScene(sceneBuildIndex: 2);
    }
}
