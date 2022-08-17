using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeEffected : MonoBehaviour
{
    protected float getTimePassed(){
        if(TimeController.Paused){
            return 0f;
        }else{
            return Time.deltaTime;
        }
    }
}
