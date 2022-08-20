using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseButton : UIElement
{
    public PurchaseType type;
    GameObject gameController;
    int metalCost = 0;
    int woodCost = 10;
    int stoneCost = 0;
    Purchaser _purchaser;
    TrainCore _trainCore;
    void Start()
    {
        gameController = GameObject.Find("Controller");
        _purchaser = gameController.GetComponent<Purchaser>();
        _trainCore = GameObject.Find("Train").GetComponent<TrainCore>();
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
        if(type == PurchaseType.Extension){
            if (_purchaser.AttemptPurchase(type))
            {
                _trainCore.AddExtension(type);
            }
            
            return;
        }

        if(type == PurchaseType.StorageExtension){
            if (_purchaser.AttemptPurchase(type))
            {
                _trainCore.AddExtension(type);
            }
            return;
        }
        
        //Debug.Log("selectedSlot: " + UI.selectedSlotId + " selectedExtensionId: " + UI.selectedExtentionId);

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

public enum PurchaseType {Grabber, Refiner, Extension, StorageExtension}
