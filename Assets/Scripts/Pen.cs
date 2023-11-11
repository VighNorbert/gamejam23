using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Pen : MonoBehaviour
{
    public string color;
    public string fadedColor;
    public bool isForWriting;
    public bool isMarker;
    public bool isForHighlighting;
    
    void Start()
    {
       
    }

    void Update()
    {
        
    }

    public string GetDefinitionString()
    {
        if (isMarker)
        {
            return "<b><color=" + color + ">";
        } else
        {
            return "<color=" + color + ">";
        }
    }

    public string GetTerminationString()
    {
        if (isMarker)
        {
            return "</color></b>";
        }
        else
        {
            return "</color>";
        }
    }
    
}
