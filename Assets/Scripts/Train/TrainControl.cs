using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainControl : MonoBehaviour
{
    TrainCore trainCore;
    UIController UIController;
    void Start()
    {
        trainCore = GameObject.Find("Train").GetComponent<TrainCore>();
        UIController = GetComponent<UIController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Space)){
            trainCore.moveForward();
        }

        if(Input.GetKey(KeyCode.A)){
            trainCore.moveBackward();
        }

        if(Input.GetKeyDown(KeyCode.B)){
            UIController.toggleBuildingUI();
        }
    }
}
