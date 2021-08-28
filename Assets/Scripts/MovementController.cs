using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
public class MovementController : MonoBehaviour
{
    [SerializeField] private List<XRController> controllers;
    [SerializeField] private GameObject head;
    [SerializeField] private float speed = 3f;

    void Update()
    {
        foreach (XRController xRController in controllers)
        {
            if (xRController.inputDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 positionVector))
            {
                if (positionVector.magnitude > 0.15f)
                {
                    Move(positionVector);
                }
            }
        }
    }

    private void Move(Vector2 positionVector)
    {
        Vector3 direction = new Vector3(positionVector.x, 0, positionVector.y);
        Vector3 headRotation = new Vector3(0, head.transform.eulerAngles.y, 0);

        direction = Quaternion.Euler(headRotation) * direction;

        Vector3 movement = direction * speed;
        transform.position = transform.position + (Vector3.ProjectOnPlane(Time.deltaTime * movement, Vector3.up));
    }
}
