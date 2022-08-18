using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddOnOptionButton : UIElement
{
    public AddOnOption type;
    GameObject gameController;
    void Start()
    {
        gameController = GameObject.Find("Controller");
    }

    public void Interact(){
        UIController UI = gameController.GetComponent<UIController>();
        if(UI.selectedSlotId != -1 && UI.selectedExtentionId != -1){
            
            TrainCore train = GameObject.Find("Train").GetComponent<TrainCore>();
            GameObject slot = train.Extentions[UI.selectedExtentionId-1].GetComponent<Extention>().GetSlot(UI.selectedSlotId); 
            switch(type){
                case AddOnOption.refinerToggle:
                    slot.GetComponent<Slot>().GetAddOn().GetComponent<Refiner>().Toggle();
                    break;
                default:
                    break;
            }
        }
        
    }

    public void Select(){

    }

    public void DeSelect(){

    }
}

public enum AddOnOption {refinerToggle}