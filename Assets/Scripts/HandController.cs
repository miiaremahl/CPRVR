using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System;
using TMPro;

public class HandController : MonoBehaviour
{

    public event Action OnHandCorrectLoc;

    public event Action OnHandIncorrectLoc;

    //For the hand animation (CPR)
    public event Action OnHandEnterCPR;
    public event Action OnHandExitCPR;

    [SerializeField] private GameObject Test;
    public TMP_Text abc;

    Animator animator;

   // public GameController GC;

    //Triggers for the hand animations
    public void OnTriggerEnter(Collider target)
    {
        if (target.tag == "HandCollider")
        {
            Test.SetActive(true);
            //Animator animator = GameObject.FindGameObjectWithTag("Hand").GetComponent<Animator>();
            //animator.SetBool("CPRExit", false);
            //animator.SetBool("CPREnter", true);
            OnHandExitCPR?.Invoke();
        }
    }

    public void OnTriggerExit(Collider target)
    {
        if (target.tag == "HandCollider")
        {
            //Animator animator = GameObject.FindGameObjectWithTag("Hand").GetComponent<Animator>();
            Test.SetActive(false);
            OnHandEnterCPR?.Invoke();
        }
    }


    public void OnTriggerEnterVR(XRBaseInteractable other)
    {
        if (other.transform.tag == "CPR Hand Area")
        {
            OnHandCorrectLoc?.Invoke();
        }

        if(other.transform.tag == "arm")
        {
           // GC.GamePlayControl(1);
        }
        
        //Debug.Log("HAND ON AREA");
    }

    public void OnTriggerExitVR(XRBaseInteractable other)
    {
        if(other.transform.tag == "CPR Hand Area")
        {
            OnHandIncorrectLoc?.Invoke();
        }
        
        //Debug.Log("HAND OFF AREA");
    }


}
