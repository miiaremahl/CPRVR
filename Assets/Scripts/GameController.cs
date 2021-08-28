using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;
public class GameController : MonoBehaviour
{
    //private XRController xrController;

    public GameObject Ghost;
    public Animator victimAnimator;
    public Animator ghostAnimator;
    public AudioSource audioPlayer;

    [Header("CPR Functionality")]
    public GameObject mainJosh;
    public GameObject deadJosh;
    public GameObject HandPoseEss;
    public GameObject ValidationUI;
    public GameObject FinalUI;

    public GameObject mobile, callButton;
   
    public GameObject[] dialogues;
    public AudioClip[] audioDialogue;
    public GameObject[] UIhints;

    public Gaze DoctorGaze;
    public GhostGaze GhostGaze;

    public GameObject shakeHand1;
    public GameObject shakeHand2;

    public GameplayHand CPRHands;

    public ParticleSystem light,stars;

   // public AudioSource KeepPushing;
    int currentId = 0;
    public static bool armCheck = false;
    public static bool firstBreathCheck = false, secondBreathCheck = false, emerCall = false, rHand = false, lHand = false, saved=false;
    public static bool pushesDone = false;
    private void Start()
    {
        Invoke("InvokeAnim", 3);
        //xrController = (XRController)GameObject.FindObjectOfType(typeof(XRController));

    }

    public static void InvokePushingDialog()
    {
        //KeepPushing.Play();
    }
    public void UpdateId(int id)
    {
        currentId = id;
    }

    public void InvokeAnim()
    {
        Debug.Log(currentId);
          switch (currentId)
        {
            case 0:
                Invoke("StartGaze", 0);
                victimAnimator.SetTrigger("walk");
                StartCoroutine(CallDialogue("Hey", 0));
                Invoke("StartChestAnimation", 10);
                Invoke("EndChestAnimation", 5);
                break;

            case 1:
                victimAnimator.SetTrigger("dying");
                Invoke("StopGaze", 10);
                Invoke("MobileActivate", 2);
                StartCoroutine("GamePlayControl", 5);
                break;

            case 2:
                light.Play();
                
                //yield return new WaitForSeconds(1);
              //  stars.Play();
               // yield return new WaitForSeconds(2);
               
                Ghost.SetActive(true);
                ReplaceGuy(0);
                // ghostAnimator.SetTrigger("standUp");
                // Invoke("ReplaceGuy", 0);
                //Invoke("GhostGazeStartGaze", 13);
                //Invoke("GhostGazeStopGaze", 18);
                Invoke("GhostGazeStartGazeCPR", 23);
                StartCoroutine(CallDialogue("Dead", 12));
                //Invoke("GhostGazeStartGaze", 20);
                break;

            case 3:
              //  ghostAnimator.SetTrigger("breathe");
                StartCoroutine(CallDialogue("Tap", 3));
                Invoke("DisplayShakeHands", 3);
                break;

            case 4:
               // ghostAnimator.SetTrigger("breathe");
                StartCoroutine(CallDialogue("Hear", 0));
                break;

            case 5:
                StartCoroutine(CallDialogue("CheckAgain", 0));
                break;

            case 6:
                StartCoroutine(CallDialogue("Call911", 0));
                break;

            case 7:
                //Invoke("GhostGazeStopGaze", 0);
                StartCoroutine(CallDialogue("StartCPR", 1));
                break;

            case 8:
                StartCoroutine(CallDialogue("RightHand", 0));
                
                break;

            case 9:
                // StartCoroutine(CallDialogue("Lefthand", 0));
                StartCoroutine("GamePlayControl", 0);
                break;

            case 10:
                StartCoroutine(CallDialogue("StartPush", 0.5f));
                Invoke("GhostGazeStopGaze", 0);
                ghostAnimator.SetTrigger("DoCPR");
                ValidationUI.SetActive(true);
                break;

            case 11:
                StartCoroutine(CallDialogue("Finally", 2)); 
                ghostAnimator.SetTrigger("stand");
                Invoke("StartGaze", 8);
                break;

            case 12: 
                ghostAnimator.SetTrigger("move");
                StartCoroutine("GamePlayControl", 12);
                break;

            case 13:
                victimAnimator.SetTrigger("wakeUp");
                //Invoke("StartGaze", 3);
                StartCoroutine(CallDialogue("Thanks", 5));
                break;
               
         


                //case 2:

                //    victimAnimator.SetTrigger("CPR");
                //    break;
                //case 3:
                //    victimAnimator.SetTrigger("wakeUp");
                //    break;
        }

    
    }

    public void InvokeDialogue(string phrase)
    {
        switch (phrase)
        {
            case "Hey":
               // dialogues[0].SetActive(true);               
                break;

            case "Dead":
                light.Stop(); stars.Stop();
                //  dialogues[1].SetActive(true);
                break;

            case "Tap":
                //dialogues[2].SetActive(true);
                break;

            case "Hear":
               // dialogues[3].SetActive(true);
                break;

            case "CheckAgain":
               // dialogues[4].SetActive(true);
                break;

            case "Call911":
              //  dialogues[5].SetActive(true);
                callButton.SetActive(true);
                break;

            case "StartCPR":
                //HandPoseEss.SetActive(true);
                break;

            case "RightHand":
                break;

            case "LeftHand":
                break;

            case "StartPush":
                break;

            case "Finally": 
                break;

            case "Thanks":
                break;

        }
    }

    public void InvokeAudio(int id)
    {
        audioPlayer.clip = audioDialogue[id];
        audioPlayer.Play();

        if (currentId != 0)
            StartCoroutine("GamePlayControl", audioDialogue[id].length);
        else
            StartCoroutine("GamePlayControl", audioDialogue[id].length - 4);

    }


    IEnumerator CallDialogue(string stringId,float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        InvokeDialogue(stringId);
        InvokeAudio(currentId);
    }

    void GhostGazeStopGaze()
    {
        GhostGaze.StopGazing();
    }

    void GhostGazeStartGaze()
    {
        GhostGaze.StartGazing(true);
    }

    void GhostGazeStartGazeCPR()
    {
        GhostGaze.StartGazing(false);
    }

    void DisableShakeHands()
    {
        shakeHand1.SetActive(false);
        shakeHand2.SetActive(false);
    }

    void StartChestAnimation()
    {
        victimAnimator.SetTrigger("ChestHurt");
    }
    void EndChestAnimation()
    {
        victimAnimator.SetTrigger("HandRelease");
    }

    void DisplayShakeHands()
    {
        shakeHand1.SetActive(true);
        shakeHand2.SetActive(true);
    }



    void StopGaze()
    {
        DoctorGaze.StopGazing();
    }

    void StartGaze()
    {
        DoctorGaze.StartGazing();
    }

    //Call this when cpr pushing is over and want to return the normal pose
    void ReturnNormalHandsPose()
    {
        CPRHands.EndCPR();
    }


    void MobileActivate()
    {
        mobile.SetActive(true);
    }

    void ReplaceGuy(int id)
    {
        if (id == 0)
        {
            mainJosh.SetActive(false);
            deadJosh.SetActive(true);
        }
        else if (id == 1)
        {
            Ghost.SetActive(false);
            deadJosh.SetActive(false);
            mainJosh.SetActive(true);
        }
    }
    public IEnumerator GamePlayControl(int waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        currentId += 1;
        
        switch(currentId-1)
        {
            case 0:
              //  dialogues[currentId-1].SetActive(false); 
                InvokeAnim();
                
                break;

            case 1: InvokeAnim();
                break;

            case 2:
                // dialogues[currentId-2].SetActive(false);
                // UIhints[0].SetActive(true);
                //light.Play();
                //yield return new WaitForSeconds(1);
                //stars.Play();
                //yield return new WaitForSeconds(2);
                //light.Stop(); stars.Stop();
                InvokeAnim();
                break;

            case 3:
                yield return new WaitUntil(()=> armCheck == true);
                DisableShakeHands();
                // dialogues[currentId - 2].SetActive(false);
                // UIhints[0].SetActive(false);
                // UIhints[1].SetActive(true);
                InvokeAnim();
                break;

            case 4:
                yield return new WaitUntil(() => firstBreathCheck == true);
               // dialogues[currentId - 2].SetActive(false);
                InvokeAnim();
                break;

            case 5:
                yield return new WaitUntil(() => secondBreathCheck == true);
               // dialogues[currentId - 2].SetActive(false);
               // UIhints[1].SetActive(false);
               // UIhints[2].SetActive(true);
                InvokeAnim();
                break;

            case 6:
                yield return new WaitUntil(() => emerCall == true);
               // dialogues[currentId - 2].SetActive(false);
              //  UIhints[2].SetActive(false);
               // UIhints[3].SetActive(true);

                //xrController.SendHapticImpulse(1.0f, 0.1f);

                InvokeAnim();
                break;

            case 7:
                HandPoseEss.SetActive(true);
                InvokeAnim();
                break;

            case 8:
                yield return new WaitUntil(() => rHand == true);
                InvokeAnim();
                break;

            case 9:
                yield return new WaitUntil(() => lHand == true);
                InvokeAnim();
                break;

            case 10:
                yield return new WaitUntil(() => saved == true);
                ValidationUI.SetActive(false);
                ReturnNormalHandsPose();
                HandPoseEss.SetActive(false);
                InvokeAnim();
                break;

            case 11:
                InvokeAnim();
                break;

            case 12:
                light.Play();
                yield return new WaitForSeconds(2);
                ReplaceGuy(1);
                InvokeAnim();
                break;

            case 13:
                yield return new WaitForSeconds(2);
                FinalUI.SetActive(true);
                break;


        }


    }
}

    
    