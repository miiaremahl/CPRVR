using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBreathing : MonoBehaviour
{
    private int num = 0;
    private void OnTriggerEnter(Collider other)
    {
        

        if (other.gameObject.tag == "head")
        {
            num++;
            if(num ==1 )
                 GameController.firstBreathCheck= true;
            else if( num ==2)
                GameController.secondBreathCheck = true;
        }
    }
}
