using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPRHandHandler : MonoBehaviour
{
    [SerializeField] private BothHandsController BothHandsController;

    [SerializeField] private GameObject Test;


    private void Awake()
    {
        BothHandsController.OnBothHandsCPREnter += Open;
        BothHandsController.OnBothHandsCPRExit += Close;
    }

    private void Open()
    {
        Test.SetActive(true);
    }


    private void Close()
    {
        Test.SetActive(false);
    }

}
