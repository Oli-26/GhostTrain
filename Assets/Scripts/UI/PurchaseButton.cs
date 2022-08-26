using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class PurchaseButton : UIElement
{
    public PurchaseType type;
    GameObject gameController;
    int metalCost = 0;
    int woodCost = 10;
    int stoneCost = 0;
    Purchaser _purchaser;
    TrainCore _trainCore;

    private static Dictionary<KeyCode, PurchaseType> shortcuts = new Dictionary<KeyCode, PurchaseType>()
    {
        { KeyCode.G, PurchaseType.Grabber },
        { KeyCode.R, PurchaseType.Refiner }
        // { PurchaseType.Extension, KeyCode. },
        // { PurchaseType.StorageExtension, KeyCode.G },
        // { PurchaseType.ResearchExtension, KeyCode.G },
        // { PurchaseType.LivingExtension, KeyCode.G },
 
    };

    void Start()
    {
        gameController = GameObject.Find("Controller");
        _purchaser = gameController.GetComponent<Purchaser>();
        _trainCore = GameObject.Find("Train").GetComponent<TrainCore>();
    }

    private void OnGUI()
    {
        Event e = Event.current;
        if (e.isKey)
        {
            Debug.Log("KeyDown:" + e.keyCode);
            if (shortcuts[e.keyCode] == type)
            {
                Interact();
            }
        }
    }

    void Update()
    {
        
    }
    
    private void OnMouseUpAsButton()
    {
        Interact();
    }

    public void Interact(){
        UIController UI = gameController.GetComponent<UIController>();
        if(type == PurchaseType.Extension || type == PurchaseType.StorageExtension || type == PurchaseType.LivingExtension || type == PurchaseType.ResearchExtension){
            if (_purchaser.AttemptPurchase(type))
            {
                _trainCore.AddExtension(type);
            }
            
            return;
        }

        if(UI.selectedSlotId != -1 && UI.selectedExtentionId != -1){
            if(!gameController.GetComponent<BuildingController>().CheckBuildIsPossible(UI.selectedExtentionId, UI.selectedSlotId)){
                return;
            }
            if(gameController.GetComponent<Purchaser>().AttemptPurchase(type)){
                gameController.GetComponent<BuildingController>().ConstructAddOn(type, UI.selectedExtentionId, UI.selectedSlotId);
                gameController.GetComponent<UIController>().LoadCorrectGUI();
            } 
        }
    }

    public void Select(){

    }

    public void DeSelect(){

    }
    }

public enum PurchaseType {Grabber, Refiner, Extension, StorageExtension, LivingExtension, ResearchExtension}
