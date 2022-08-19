using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    public GameObject grabberPrefabTopSide;
    public GameObject grabberPrefabBotSide;
    public GameObject refinerPrefab;
    public GameObject extensionPrefab;
    public TrainCore trainCore;
    public UIController uiController;

    void Start()
    {
    }
    
    

    void Update()
    {

    }

    public bool CheckBuildIsPossible(int extentionNumber, int slotNumber){
        GameObject slot = trainCore.Extentions[extentionNumber-1].GetComponent<Extention>().GetSlot(slotNumber);
        return slot.GetComponent<Slot>().GetAddOn() == null;
    }
    public void ConstructAddOn(PurchaseType type, int extentionNumber, int slotNumber){
        
        if(type == PurchaseType.Extension){
            CreateExtension();
            uiController.RefreshUiElements();
            return;
        }

        Extention extention = trainCore.Extentions[extentionNumber-1];
        GameObject slot = extention.GetComponent<Extention>().GetSlot(slotNumber);

        if(type == PurchaseType.Grabber){
            CreateGrabber(extention, slot, slotNumber <= 1);
            uiController.selectedObject.GetComponent<SelectableTrainPart>().InUse();
        }

        if(type == PurchaseType.Refiner){
            CreateRefiner(extention, slot, slotNumber <= 1);
            uiController.selectedObject.GetComponent<SelectableTrainPart>().InUse();
        }
        
        uiController.RefreshUiElements();

    }

    public void CreateGrabber(Extention extention, GameObject slot, bool isTop){
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

    public void CreateRefiner(Extention extention, GameObject slot, bool isTop){
        GameObject refiner;
        refiner = Instantiate(refinerPrefab, slot.transform.position, Quaternion.identity);
        slot.GetComponent<Slot>().CreateAddOn(refiner);
        refiner.transform.parent = slot.transform;
    }


    public void CreateExtension(){
        Extention extension = Instantiate(extensionPrefab, trainCore.trainFront.transform.position, Quaternion.identity).GetComponent<Extention>();
        extension.transform.position -= new Vector3((extension.GetComponent<Extention>().baseObject.GetComponent<SpriteRenderer>().bounds.size.x -0.2f) * trainCore.Extentions.Count, 0f, 0f);
        extension.transform.position -= new Vector3(1.88f, 0.82f, 0f);
        extension.transform.parent = trainCore.gameObject.transform;
        trainCore.Extentions.Add(extension);

        uiController.selectableTrainParts.AddRange(extension.GetComponent<Extention>().interactableUISlots);
        extension.GetComponent<Extention>().SetSlotExtensionId(trainCore.Extentions.Count);

    }




}