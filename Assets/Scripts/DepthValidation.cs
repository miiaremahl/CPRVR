using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class DepthValidation : MonoBehaviour
{
    public Image bar;
    int totalCount = 0; // Total compressions count
    // perfectCount = 0;

    public AudioSource perfect, tooMuch;

   
    public GameObject ind, chestObj;
    float indN, chestN;

    /*
    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.name)
        {
            case "L1": bar.fillAmount = 0.3f;
                TotalCompressionsInc();
                //StartCoroutine(Decrease(0.3f)); 
                break;
            case "L2": bar.fillAmount = 0.6f;
                TotalCompressionsInc();
                //StartCoroutine(Decrease(0.6f));
                break;
            case "L3": bar.fillAmount = 1;
                TotalCompressionsInc();
                tooMuch.Play();
                //  StartCoroutine(Decrease(1f));
                break;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        switch (other.gameObject.name)
        {
            case "L1":
                bar.fillAmount = 0;
                // StartCoroutine(Decrease(bar.fillAmount));
                break;
                //case "L2":
                //case "L3":


        }

    }

    public void PerfectPush()
    {
        perfectCount++;
        if(perfectCount==3)
        {
            // display Perfect Push 
        }
    }

    public void TotalCompressionsInc()
    {
        totalCount++;
        if(totalCount == 30)
        {
            GameController.pushesDone = true;
        }
    }

    IEnumerator Decrease(float maxVal)
    {
        for(float i= maxVal; i > 0f; i -=0.01f)
        {
            bar.fillAmount = i;
            yield return new WaitForSeconds(0.02f);
        }
    }

   */

    private void Start()
    {
     
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "PushObject")
        {   

            totalCount++;
            Debug.Log(totalCount);
            if (totalCount == 50)
            {
                GameController.saved = true;
            }
        }
    }
    private void Update()
    {   
        indN = (ind.transform.localPosition.y  - (-348)) / (127 - (-348));
        chestN = (chestObj.transform.localPosition.y - (1.5f)/((16.4f) - (1.5f)));

     //   ind.transform.
        ind.transform.localPosition = new Vector3(ind.transform.localPosition.x, ( chestN * 30), ind.transform.localPosition.z);
      
    }
}
