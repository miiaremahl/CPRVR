using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gaze : MonoBehaviour
{
    public Transform Player;
    public bool Gazing=false;
    public bool RotatingBackToNormal = false;
    private Quaternion normalRotation;
    private Quaternion latestRotation;


    void LateUpdate()
    {
        if (Gazing)
        {
            Vector3 relativePos = Player.position - transform.position;

            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);

            Quaternion temprotation = rotation;

            if (rotation.y > -0.50 && rotation.y < -0.13)
            {
                rotation.x = transform.rotation.x;
                rotation.z = transform.rotation.z;
                transform.rotation = rotation;
                latestRotation = rotation;
            }
            else if (rotation.y < 0.75)
            {
                if (Mathf.Abs(rotation.y) > 0.50)
                {
                    rotation.x = transform.rotation.x;
                    rotation.z = transform.rotation.z;
                    rotation.y = -0.50f;
                    transform.rotation = rotation;
                }
                else if (rotation.y > -0.13)
                {
                    rotation.x = transform.rotation.x;
                    rotation.z = transform.rotation.z;
                    rotation.y = -0.13f;
                    transform.rotation = rotation;
                }
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
            transform.rotation = Quaternion.Lerp(latestRotation, normalRotation, 0.5f  * Time.deltaTime);
            RotatingBackToNormal = false;
        }

    }

    public void StartGazing()
    {
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
