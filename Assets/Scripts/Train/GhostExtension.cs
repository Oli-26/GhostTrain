using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostExtension : Extension
{

    private TrainCore _trainCore;
    private Purchaser _purchaser;
    private UIController _uiController;
    void Start()
    {
        _purchaser = FindObjectOfType<Purchaser>();
        _uiController = FindObjectOfType<UIController>();
        _trainCore = FindObjectOfType<TrainCore>();
    }

    void Update()
    {
        
    }

    private void OnMouseUpAsButton()
    {
        Debug.Log("Ghost train clicked");
        _uiController.LoadCorrectGUI(true);
    }

    public GameObject GetSlot(int index){
        return null;
    }

    public GameObject GetAddOn(int index){
        return null;
    }

    public void CreateAddon(int index, GameObject addon){}

    public void SetSlotExtensionId(int id){}
}
