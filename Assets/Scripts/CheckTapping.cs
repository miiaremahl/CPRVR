using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;

public class CheckTapping : MonoBehaviour
{

    private XRController xrController;
    private int num = 0;

    void Start()
    {
        xrController = (XRController)GameObject.FindObjectOfType(typeof(XRController));
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "arm")
        {
            num++;
            if(num == 3)
                GameController.armCheck = true;
        }

        if (other.gameObject.tag == "911")
        {
            other.gameObject.GetComponent<MeshCollider>().enabled = false;
            GameController.emerCall = true;
            float amplitude = 0.5f;
            float duration = 0.1f;
            xrController.SendHapticImpulse(amplitude, duration);
        }
    }
}
