using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseAddOnButton : UIElement
{
    public AddOnType type;
    public int extentionNumber = 1;
    public int slotNumber = 0;
    GameObject gameController;
    int metalCost = 0;
    int woodCost = 10;
    int stoneCost = 0;
    void Start()
    {
        gameController = GameObject.Find("Controller");
    }

    void Update()
    {
        
    }

    public void Interact(){
        Debug.Log("purchase addon button clicked: " + type);
        UIController UI = gameController.GetComponent<UIController>();
        if(UI.selectedSlotId != -1 && UI.selectedExtentionId != -1){
            gameController.GetComponent<BuildingController>().ConstructAddOn(type, UI.selectedExtentionId, UI.selectedSlotId);
        }
        
    }

    public void Select(){

    }

    public void DeSelect(){

    }
}

public enum AddOnType {Grabber, Refiner}
