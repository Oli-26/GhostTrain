using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostExtension : Extention
{

    private TrainCore _trainCore;
    private Purchaser _purchaser;
    void Start()
    {
        _purchaser = FindObjectOfType<Purchaser>();
        _trainCore = FindObjectOfType<TrainCore>();
    }

    void Update()
    {
        
    }

    private void OnMouseUpAsButton()
    {
        Debug.Log("Clicked");
        if (_purchaser.AttemptPurchase(PurchaseType.Extension))
        {
            _trainCore.AddExtension();
        }
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
