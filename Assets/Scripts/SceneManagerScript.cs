using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    //public Animator animator;

    public void FadeToNextScene()
    {
        //ADD FADE
        //    animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
       SceneManager.LoadScene(1);
    }

    public void Replay()
    {
        SceneManager.LoadScene(0);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
