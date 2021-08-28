using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GameplayHand : BaseHand
{
    // The interactor we react to
    [SerializeField] private XRBaseInteractor targetInteractor = null;

    //interactables
    public GameObject SecondInteractable;
    public GameObject FirstInteractable;

    //ghost hands
    public GameObject GhostHandOne;
    public GameObject GhostHandTwo;

    //righ hands meshes
    public GameObject righHandMesh;
    public GameObject righHandCPRMesh; //under left hand

    //other hand
    public GameplayHand otherHand;

    //UI texts
    public GameObject leftHandText;
    public GameObject rightHandText;
    public GameObject PushText;

    private bool PerformingCPR = false;

    public AudioSource KeepPushingAudio;

    //for the other hand to call
    public void SetDefaultPose()
    {
        PerformingCPR = false;
        // apply default pose
        ApplyDefaultPose();
    }

    private void LeaveMobileArea()
    {
        ApplyDefaultPose();
    }


    private void TryApplyDefaultPose()
    {
        PerformingCPR = false;

        // apply default pose
        ApplyDefaultPose();

        //the other hand
        otherHand.SetDefaultPose();

        //other hand visible again
        righHandCPRMesh.SetActive(false);
        righHandMesh.SetActive(true);

        //hide second interactable
        SecondInteractable.SetActive(false);

        //set ghost hands visible
        GhostHandOne.SetActive(true);
        GhostHandTwo.SetActive(true);

        //set UI text
       // PushText.SetActive(false);
       // rightHandText.SetActive(true);
    }

    public override void ApplyOffset(Vector3 position, Quaternion rotation)
    {
        Vector3 finalPosition = position * -1.0f;
        Quaternion finalRotation = Quaternion.Inverse(rotation);

        finalPosition = finalPosition.RotatePointAroundPivot(Vector3.zero, finalRotation.eulerAngles);

        targetInteractor.attachTransform.localPosition = finalPosition;
        targetInteractor.attachTransform.localRotation = finalRotation;
    }

    public void EndCPR()
    {
        PerformingCPR = false;

        // apply default pose
        ApplyDefaultPose();

        //the other hand
        otherHand.SetDefaultPose();

        //other hand visible again
        righHandCPRMesh.SetActive(false);
        righHandMesh.SetActive(true);

        FirstInteractable.SetActive(false);
    }


    private void OnValidate()
    {
        if (!targetInteractor)
        {
           targetInteractor = GetComponent<XRBaseInteractor>();
        }
    }


    //Hand trigger detection
    public void OnTriggerEnter(Collider target)
    {

        if (gameObject.tag == "RightHand" && target.tag == "GhostRight" && !PerformingCPR)
        {
            // rightHandText.SetActive(false);
            // leftHandText.SetActive(true);
            GameController.rHand = true;
            GhostHandOne.SetActive(false);
            SecondInteractable.SetActive(true);
            ApplyCPRPose();
            PerformingCPR = true;
            StopCoroutine(CheckForLongPause());
        }

        if (gameObject.tag == "LeftHand" && target.tag == "GhostLeft" && !PerformingCPR)
        {
            //  leftHandText.SetActive(false);
            // PushText.SetActive(true);
            GameController.lHand = true;
            GhostHandTwo.SetActive(false);
            righHandMesh.SetActive(false);
            righHandCPRMesh.SetActive(true);
            ApplyCPRPose();
            PerformingCPR = true;
            StopCoroutine(CheckForLongPause());
        }


        if (target.tag == "MobileHand")
        {
            ApplyMobilePose();
        }
    }

    public void OnTriggerExit(Collider target)
    {
        if (target.tag == "CprHand" && PerformingCPR)
        {
            TryApplyDefaultPose();
        }


        if (target.tag == "MobileHand")
        {
            LeaveMobileArea();
        }
    }

    IEnumerator CheckForLongPause()
    {
        yield return new WaitForSeconds(3);
        KeepPushingAudio.Play();
      //  GameController.InvokePushingDialog();
    }
}