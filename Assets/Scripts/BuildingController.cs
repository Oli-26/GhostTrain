using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    public GameObject grabberPrefabTopSide;
    public GameObject grabberPrefabBotSide;
    public GameObject refinerPrefab;
    public TrainCore trainCore;
    public UIController uiController;

    void Start()
    {
    }
    
    

    void Update()
    {

    }

    public bool CheckBuildIsPossible(int extentionNumber, int slotNumber){
        GameObject slot = trainCore.Extensions[extentionNumber-1].GetComponent<Extension>().GetSlot(slotNumber);
        return slot.GetComponent<Slot>().GetAddOn() == null;
    }
    public void ConstructAddOn(PurchaseType type, int extentionNumber, int slotNumber){
        Extension extention = trainCore.Extensions[extentionNumber-1];
        GameObject slot = extention.GetComponent<Extension>().GetSlot(slotNumber);

        if(type == PurchaseType.Grabber){
            CreateGrabber(slot, slotNumber <= 1);
            uiController.selectedObject.GetComponent<SelectableTrainPart>().InUse();
        }

        if(type == PurchaseType.Refiner){
            CreateRefiner(slot, slotNumber <= 1);
            uiController.selectedObject.GetComponent<SelectableTrainPart>().InUse();
        }
        
        uiController.RefreshUiElements();

    }

    public void CreateGrabber(GameObject slot, bool isTop){
        GameObject grabber;
        if(isTop){
            grabber = Instantiate(grabberPrefabTopSide, slot.transform.position, Quaternion.identity);
        }else{
            grabber = Instantiate(grabberPrefabBotSide, slot.transform.position, Quaternion.identity);
        }
        
        slot.GetComponent<Slot>().CreateAddOn(grabber);
        grabber.transform.parent = slot.transform;
        grabber.GetComponent<Grabber>().isTopSide = isTop;
    }

    public void CreateRefiner(GameObject slot, bool isTop){
        GameObject refiner;
        refiner = Instantiate(refinerPrefab, slot.transform.position, Quaternion.identity);
        slot.GetComponent<Slot>().CreateAddOn(refiner);
        refiner.transform.parent = slot.transform;
    }

}