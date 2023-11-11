using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Paper : MonoBehaviour
{
    public GameObject wordsContainer;
    public GameObject[] _words;
    
    void Start()
    {
        _words = new GameObject[wordsContainer.transform.childCount];
        
        for (int i = 0; i < wordsContainer.transform.childCount; i++)
        {
            GameObject childObject = wordsContainer.transform.GetChild(i).gameObject;
            _words[i] = childObject;
        }

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit))
            {
                if (_words.Contains(hit.collider.gameObject))
                {
                    GameObject word = hit.collider.gameObject;
                    word.GetComponent<MeshRenderer>().enabled = true;
                }
            }
        }   
    }
}
