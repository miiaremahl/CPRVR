using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class BothHandsController : MonoBehaviour
{
    [SerializeField] private HandController leftHand;
    [SerializeField] private HandController rightHand;

    public event Action OnBothHandsReady;
    public event Action OnBothHandsNotReady;
    private int readyHandOne;

    //CPR animations
    public event Action OnBothHandsCPREnter;
    public event Action OnBothHandsCPRExit;
    private int CPRHand=0;



    private void Awake()
    {
        // Registering an event for the actions 

        leftHand.OnHandCorrectLoc += HandReady;
        rightHand.OnHandCorrectLoc += HandReady;

        leftHand.OnHandIncorrectLoc += HandNotReady;
        rightHand.OnHandIncorrectLoc += HandNotReady;


        //Hand animation registers
        leftHand.OnHandEnterCPR += HandEnterCPR;
        rightHand.OnHandEnterCPR += HandEnterCPR;

        leftHand.OnHandExitCPR += HandExitCPR;
        rightHand.OnHandExitCPR += HandExitCPR;
    }

    //enters cpr area (animation)
    private void HandEnterCPR()
    {
        CPRHand += 1;

        if (CPRHand == 2)
        {
            OnBothHandsCPREnter?.Invoke();
        }
    }

    //exits cpr area (animation)
    private void HandExitCPR()
    {
        CPRHand -= 1;
        if (CPRHand == 1)
        {
            OnBothHandsCPRExit?.Invoke();
        }
    }


    private void HandReady()
    {
        readyHandOne += 1;
        if(readyHandOne == 2)
        {
            OnBothHandsReady?.Invoke();
        }

        //Debug.Log("-HAND READY-");
    }

    private void HandNotReady()
    {
        readyHandOne -= 1;
        if(readyHandOne == 1)
        {
            OnBothHandsNotReady?.Invoke();
        }

        //Debug.Log("-HAND NOT READY-");
    }



}
