using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Pen : MonoBehaviour
{
    public GameManager gm;
    
    public string color;
    public string fadedColor;
    public bool isForWriting;
    public bool isMarker;
    public bool isForHighlighting;
    public Material mat;
    
    void Start()
    {
        mat = GetComponentInChildren<MeshRenderer>().material;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
            {
                gm.ChangePen(this);
            }
        }
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
