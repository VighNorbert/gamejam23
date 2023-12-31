using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{

    public Paper[] availablePapers;
    
    public Paper currentPaper;
    public int currentPaperIndex;
    
    public ChatController chatController;

    public Pen defaultPen;
    public Pen[] pens;
    public Pen[] markers;

    public Pen nextPen;

    public Pen selectedPen;
    public bool isPenAvailable = false;

    public Hand hand;
    public Vector3 originalHandPosition;

    public Animator handAnimator;

    public ViewersCount vc;

    public int paperIndex = 0;

    private Paper _previousPaper;

    public GameObject pointer;

    public DonationController dc;

    private bool shaking;
    
    
    void Start()
    {
        defaultPen.gm = this;
        foreach (Pen p in pens)
        {
            p.gm = this;
        }
        foreach (Pen p in markers)
        {
            p.gm = this;
        }
        
        handAnimator = GetComponent<Animator>();
        SelectPen(defaultPen);
        SelectNextPaper();
    }

    void Update()
    {
        if (isPenAvailable)
        {
            if (Input.GetKeyDown("1") && !selectedPen.Equals(markers[0]))
            {
                ChangePen(markers[0]);
            }
            if (Input.GetKeyDown("2") && !selectedPen.Equals(defaultPen))
            {
                ChangePen(defaultPen);
            }
            else if (Input.GetKeyDown("3") && !selectedPen.Equals(pens[0]))
            {
                ChangePen(pens[0]);
            }
            else if (Input.GetKeyDown("4") && pens[1].gameObject.activeSelf && !selectedPen.Equals(pens[1]))
            {
                ChangePen(pens[1]);
            }
            else if (Input.GetKeyDown("5") && pens[2].gameObject.activeSelf && !selectedPen.Equals(pens[2]))
            {
                ChangePen(pens[2]);
            }
        }
    }

    public void SelectNextPaper()
    {
        if (currentPaper == null)
        {
            currentPaperIndex = paperIndex % availablePapers.Length;
        
            currentPaper = Instantiate(availablePapers[currentPaperIndex], new Vector3(0, 5.01f, -3.08f), Quaternion.identity);
            currentPaper.gm = this;
            paperIndex++;
        }
        else
        {
            _previousPaper = currentPaper;
            _previousPaper.transform.position = new Vector3(0, 5.03f, -3.08f);
            
            currentPaperIndex = paperIndex % availablePapers.Length;
            currentPaper = Instantiate(availablePapers[currentPaperIndex], new Vector3(0, 5.01f, -3.08f),
                Quaternion.identity);
            currentPaper.gm = this;
            paperIndex++;
            
            int donated = Random.Range(50, 101);
            DonationsCount.AddDonation(donated);
            dc.StartC("Donation: You have received $" + donated + " for completing a page!");

            StartCoroutine(PutPenDown());
        }
    }

    public void ChangePen(Pen pen)
    {
        if (isPenAvailable)
            StartCoroutine(PenChanger(pen));
    }

    public void SelectPen(Pen pen)
    {
        if (hand.transform.position.Equals(new Vector3(0, 5, -3.47f)))
        {
            originalHandPosition = new Vector3(-0.872f, 5, -1.939f);
        }
        else
        {
            originalHandPosition = hand.transform.position;
        }

        isPenAvailable = false;
        
        selectedPen = pen;
        StartCoroutine(PenSelector(true));
    }
    
    IEnumerator PenSelector(bool sendPickup = false)
    {
        Debug.Log("Pickup");
        if (sendPickup) handAnimator.SetTrigger("PickUpPen");
        Vector3 startPos = hand.transform.position;
        float total = Vector3.Distance(startPos, selectedPen.transform.position) * .6f;
        float start = Time.time;
        while (true)
        {
            float t = (Time.time - start) / total;
            hand.transform.position = Vector3.Lerp(startPos, selectedPen.transform.position, t);
            if (Time.time - start >= total)
            {
                hand.transform.position = selectedPen.transform.position;
                break;
            }
            yield return new WaitForSeconds(0.001f);
        }
    }
    
    IEnumerator PenChanger(Pen pen)
    {
        isPenAvailable = false;
        originalHandPosition = hand.transform.position;
        handAnimator.SetTrigger("SwitchPen");
        nextPen = pen;
        Vector3 startPos = hand.transform.position;
        float total = Vector3.Distance(startPos, selectedPen.transform.position) * .6f;
        float start = Time.time;
        while (true)
        {
            float t = (Time.time - start) / total;
            hand.transform.position = Vector3.Lerp(startPos, selectedPen.transform.position, t);
            if (Time.time - start >= total)
            {
                hand.transform.position = selectedPen.transform.position;
                break;
            }
            yield return new WaitForSeconds(0.001f);
        }
    }

    public void OnPenPutDown()
    {
        selectedPen.GetComponentInChildren<MeshRenderer>().enabled = true;
        hand.blackMarker.SetActive(false);
        hand.blackPen.SetActive(false);
        hand.redPen.SetActive(false);
        hand.bluePen.SetActive(false);
        hand.greenPen.SetActive(false);
        selectedPen = nextPen;
        nextPen = null;
        StartCoroutine(PenSelector());
    }

    public void OnPenPickedUp()
    {
        if (selectedPen == null) return;
        selectedPen.GetComponentInChildren<MeshRenderer>().enabled = false;
        switch (selectedPen.id)
        {
            case 0:
                hand.blackMarker.SetActive(true);
                break;
            case 1:
                hand.blackPen.SetActive(true);
                break;
            case 2:
                hand.redPen.SetActive(true);
                break;
            case 3:
                hand.bluePen.SetActive(true);
                break;
            case 4:
                hand.greenPen.SetActive(true);
                break;
        }
        
        StartCoroutine(HandReturnToPosition());
    }

    public void OnPenFinished()
    {
        // isPenAvailable = true;
    }

    IEnumerator HandReturnToPosition()
    {
        Vector3 startPos = hand.transform.position;
        float total = Vector3.Distance(startPos, originalHandPosition) * .6f;
        float start = Time.time;
        while (true)
        {
            float t = (Time.time - start) / total;
            hand.transform.position = Vector3.Lerp(startPos, originalHandPosition, t);
            if (Time.time - start >= total)
            {
                hand.transform.position = originalHandPosition;
                Debug.Log("Pen available");
                isPenAvailable = true;
                break;
            }
            yield return new WaitForSeconds(0.001f);
        }
    }

    public IEnumerator AnimateWrite(Vector3 endPosition, bool writing)
    {
        Vector3 startPos = hand.transform.position;
        float total = writing ? Vector3.Distance(startPos, endPosition) * 1f : Vector3.Distance(startPos, endPosition) * .5f;

        float start = Time.time;
        while (true)
        {
            float t = (Time.time - start) / total;
            hand.transform.position = Vector3.Lerp(startPos, endPosition, t);
            if (Time.time - start >= total)
            {
                hand.transform.position = endPosition;
                if (writing) handAnimator.SetTrigger("EndWriting");
                Debug.Log("Pen available");
                isPenAvailable = true;
                break;
            }
            yield return new WaitForSeconds(0.001f);
        }
    }

    IEnumerator PutPenDown()
    {
        isPenAvailable = false;
        originalHandPosition = hand.transform.position;
        handAnimator.SetTrigger("PutDownPen");
        Vector3 startPos = hand.transform.position;
        float total = Vector3.Distance(startPos, selectedPen.transform.position) * .6f;
        float start = Time.time;
        while (true)
        {
            float t = (Time.time - start) / total;
            hand.transform.position = Vector3.Lerp(startPos, selectedPen.transform.position, t);
            if (Time.time - start >= total)
            {
                hand.transform.position = selectedPen.transform.position;
                break;
            }
            yield return new WaitForSeconds(0.001f);
        }
    }

    public void OnReadyToAnimateHandToPaper()
    {
        
        selectedPen.GetComponentInChildren<MeshRenderer>().enabled = true;
        hand.blackMarker.SetActive(false);
        hand.blackPen.SetActive(false);
        hand.redPen.SetActive(false);
        hand.bluePen.SetActive(false);
        hand.greenPen.SetActive(false);
        selectedPen = null;
        StartCoroutine(AnimateHandToPaper());
    }
    
    IEnumerator AnimateHandToPaper()
    {
        Vector3 startPos = hand.transform.position;
        Vector3 endPos = new Vector3(-0.85f, 4.5f, -3.5f);
        float total = Vector3.Distance(startPos, endPos) * .6f;
        float start = Time.time;
        while (true)
        {
            float t = (Time.time - start) / total;
            hand.transform.position = Vector3.Lerp(startPos, endPos, t);
            if (Time.time - start >= total)
            {
                hand.transform.position = endPos;
                StartCoroutine(AnimatePaperOut());
                break;
            }
            yield return new WaitForSeconds(0.001f);
        }
    }

    IEnumerator AnimatePaperOut()
    {
        Vector3 startPos = hand.transform.position;
        Vector3 paperOffset = _previousPaper.transform.position - startPos;
        Vector3 endPos = new Vector3(-0.85f, 5f, -6f);
        float total = Vector3.Distance(startPos, endPos) * .8f;
        float start = Time.time;
        while (true)
        {
            float t = (Time.time - start) / total;
            hand.transform.position = Vector3.Lerp(startPos, endPos, t);
            _previousPaper.transform.position = Vector3.Lerp(startPos, endPos, t) + paperOffset;
            if (Time.time - start >= total)
            {
                hand.transform.position = endPos;
                Destroy(_previousPaper.gameObject);
                _previousPaper = null;
                StartCoroutine(ReturnHand());
                break;
            }
            yield return new WaitForSeconds(0.001f);
        }
    }
    
    IEnumerator ReturnHand()
    {
        Vector3 startPos = hand.transform.position;
        Vector3 endPos = new Vector3(0, 5, -3.47f);
        float total = Vector3.Distance(startPos, endPos) * .8f;
        float start = Time.time;
        while (true)
        {
            float t = (Time.time - start) / total;
            hand.transform.position = Vector3.Lerp(startPos, endPos, t);
            if (Time.time - start >= total)
            {
                hand.transform.position = endPos;
                SelectPen(defaultPen);
                break;
            }
            yield return new WaitForSeconds(0.001f);
        }
    }
    
    // public IEnumerator Shake()
    // {
    //     Debug.Log("trying to shake");
    //     if (!shaking)
    //     {
    //         Debug.Log("shaking");
    //         shaking = true;
    //         Quaternion originalRotation = transform.rotation;
    //         Vector3 originalPosition = transform.position;
    //         float elapsed = 0.0f;
    //
    //         while (elapsed < .5f)
    //         {
    //             float x = (float) Math.Sin(Time.time * 50f);
    //
    //             transform.rotation = originalRotation * Quaternion.Euler(0, x / 10f, 0);
    //             
    //             // transform.position = new Vector3(originalPosition.x, originalPosition.y - x / 200f, originalPosition.z);
    //
    //             elapsed += Time.deltaTime;
    //             yield return null;
    //         }
    //
    //         transform.rotation = originalRotation;
    //         transform.position = originalPosition;
    //         shaking = false;
    //     }
    // }
}
