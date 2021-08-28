using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeLerp : MonoBehaviour
{
    public Transform target;

void Update()
{


        //check out https://docs.unity3d.com/ScriptReference/Quaternion.LookRotation.html
        // Rotate the camera every frame so it keeps looking at the target
        transform.LookAt(target);

    // Same as above, but setting the worldUp parameter to Vector3.left in this example turns the camera on its side
    transform.LookAt(target, Vector3.up);
}
}