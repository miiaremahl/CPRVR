using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;
public class TriggerGreen : MonoBehaviour
{
    public Color defaultCol, greenCol;
    public Image arrow;
    //InputDevice device = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
    private XRController xrController;

    void Start()
    {
        xrController = (XRController)GameObject.FindObjectOfType(typeof(XRController));
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("arrow"))
        {
            arrow.color = greenCol;
            //uint channel = 1;
            float amplitude = 0.5f;
            float duration = 0.2f;
            xrController.SendHapticImpulse(amplitude, duration);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("arrow"))
        {
            arrow.color = defaultCol;
        }
    }
}
