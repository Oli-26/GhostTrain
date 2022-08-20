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
    
    private void OnMouseUpAsButton()
    {
        Interact();
    }

    public void Interact(){
        UIController UI = gameController.GetComponent<UIController>();
        if(UI.selectedSlotId != -1 && UI.selectedExtentionId != -1){
            
            TrainCore train = GameObject.Find("Train").GetComponent<TrainCore>();
            GameObject slot = train.Extensions[UI.selectedExtentionId-1].GetComponent<Extension>().GetSlot(UI.selectedSlotId); 
            Refiner refiner;
            Grabber grabber;

            switch(type){
                case AddOnOption.refinerToggle:
                    refiner = slot.GetComponent<Slot>().GetAddOn().GetComponent<Refiner>();
                    refiner.Toggle();
                    gameController.GetComponent<UIController>().SetUpRefinerOptions(refiner);
                    break;
                case AddOnOption.refinerMakeMetal:
                    refiner = slot.GetComponent<Slot>().GetAddOn().GetComponent<Refiner>();
                    refiner.SetRefiningType(ResourceType.Metal);
                    gameController.GetComponent<UIController>().SetUpRefinerOptions(refiner);
                    break;
                case AddOnOption.grabberFocusStone:
                    grabber = slot.GetComponent<Slot>().GetAddOn().GetComponent<Grabber>();
                    grabber.FocusResource(ResourceType.Stone);
                    gameController.GetComponent<UIController>().SetUpGrabberOptions(grabber);
                    break;
                case AddOnOption.grabberFocusWood:
                    grabber = slot.GetComponent<Slot>().GetAddOn().GetComponent<Grabber>();
                    grabber.FocusResource(ResourceType.Wood);
                    gameController.GetComponent<UIController>().SetUpGrabberOptions(grabber);
                    break;
                case AddOnOption.grabberFocusNone:
                    grabber = slot.GetComponent<Slot>().GetAddOn().GetComponent<Grabber>();
                    grabber.SetFocusActive(false);
                    gameController.GetComponent<UIController>().SetUpGrabberOptions(grabber);
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

public enum AddOnOption {refinerToggle, grabberFocusWood, grabberFocusStone, grabberFocusNone, refinerMakeMetal};