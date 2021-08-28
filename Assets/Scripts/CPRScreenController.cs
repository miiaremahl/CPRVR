using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPRScreenController : MonoBehaviour
{
    [SerializeField] private BothHandsController bothHandsCont;

    [SerializeField] private GameObject screen;

    private void Awake()
    {
        bothHandsCont.OnBothHandsReady += OpenCPRScreen;
        bothHandsCont.OnBothHandsNotReady += CloseCPRScreen;
    }

    private void OpenCPRScreen()
    {
        screen.SetActive(true);    
    }

    private void CloseCPRScreen()
    {
        screen.SetActive(false);
    }

}
