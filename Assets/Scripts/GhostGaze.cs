using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GhostGaze : MonoBehaviour
{
    public Transform PlayerDown;
    public Transform PlayerHead;

    public bool GazeHead;
    public bool Gazing=false;
    public bool RotatingBackToNormal = false;
    private Quaternion normalRotation;
    private Quaternion latestRotation;



    void LateUpdate()
    {
        if (Gazing)
        {
            Vector3 relativePos;
            if (GazeHead)
            {
                 relativePos = PlayerHead.position - transform.position;
            }
            else
            {
                 relativePos = PlayerDown.position - transform.position;
            }
            

            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);

            Quaternion temprotation = rotation;


            if (rotation.y > -0.90 && rotation.y < -0.70)
            {
                //if(!(rotation.x > -0.085 && rotation.x < 0)){
                //    rotation.x = latestRotation.x;
                //}
               
                rotation.z = transform.rotation.z;
                transform.rotation = rotation;
                latestRotation = rotation;
            }
            else
            {
                transform.rotation = latestRotation;
            }
        }

        if (RotatingBackToNormal)
        {
            //Quaternion rotation = Quaternion.Lerp(normalRotation, normalRotation, Time.time * 1f);
            transform.rotation = Quaternion.Lerp(latestRotation, normalRotation, 0.5f * Time.deltaTime);
            RotatingBackToNormal = false;
        }

    }

    public void StartGazing(bool gazeHead)
    {
        GazeHead = gazeHead;
        normalRotation = transform.rotation;
        Gazing = true;
        RotatingBackToNormal = false;
    }

    public void StopGazing()
    {
        Gazing = false;
        RotatingBackToNormal = true;
    }


}
