using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Paper : MonoBehaviour
{
    
    public GameObject textLinePrefab;
    
    public int numLines = 10;
    
    private int maxNumLines = 19;

    // Start is called before the first frame update
    void Start()
    {
        if (numLines > maxNumLines)
        {
            numLines = maxNumLines;
        }
        
        for (int i = 0; i < numLines; i++)
        {
            GameObject textLine = Instantiate(textLinePrefab, transform);
            textLine.transform.localPosition = new Vector3(0, 0.001f, 4.45f - 0.5f * i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
