using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainControl : MonoBehaviour
{
    TrainCore trainCore;
    BuildingController buildingController;
    void Start()
    {
        trainCore = GameObject.Find("Train").GetComponent<TrainCore>();
        buildingController = GetComponent<BuildingController>();
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
            buildingController.toggleBuildingUI();
        }
    }
}
