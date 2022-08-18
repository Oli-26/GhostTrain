using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    public GameObject grabberPrefabTopSide;
    public GameObject grabberPrefabBotSide;
    public GameObject refinerPrefab;
    public GameObject extensionPrefab;

    void Start()
    {
        
    }

    void Update()
    {

    }

    public bool CheckBuildIsPossible(int extentionNumber, int slotNumber){
        List<GameObject> extentions = GameObject.Find("Train").GetComponent<TrainCore>().Extentions;
        GameObject slot = extentions[extentionNumber-1].GetComponent<Extention>().GetSlot(slotNumber);
        return slot.GetComponent<Slot>().GetAddOn() == null;
    }
    public void ConstructAddOn(PurchaseType type, int extentionNumber, int slotNumber){
        if(type == PurchaseType.Extension){
            CreateExtension();
            return;
        }
        
        List<GameObject> extentions = GameObject.Find("Train").GetComponent<TrainCore>().Extentions;
        GameObject slot = extentions[extentionNumber-1].GetComponent<Extention>().GetSlot(slotNumber);

        if(type == PurchaseType.Grabber){
            CreateGrabber(extentions[extentionNumber-1], slot, slotNumber <= 1);
            GetComponent<UIController>().selectedObject.GetComponent<SelectableTrainPart>().InUse();
        }

        if(type == PurchaseType.Refiner){
            CreateRefiner(extentions[extentionNumber-1], slot, slotNumber <= 1);
            GetComponent<UIController>().selectedObject.GetComponent<SelectableTrainPart>().InUse();
        }

    }

    public void CreateGrabber(GameObject extention, GameObject slot, bool isTop){
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

    public void CreateRefiner(GameObject extention, GameObject slot, bool isTop){
        GameObject refiner;
        refiner = Instantiate(refinerPrefab, slot.transform.position, Quaternion.identity);
        slot.GetComponent<Slot>().CreateAddOn(refiner);
        refiner.transform.parent = slot.transform;
    }


    public void CreateExtension(){
        GameObject train = GameObject.Find("Train");
        TrainCore trainScript = train.GetComponent<TrainCore>();
        GameObject extension = Instantiate(extensionPrefab, trainScript.trainFront.transform.position, Quaternion.identity);
        extension.transform.position -= new Vector3((extension.GetComponent<Extention>().baseObject.GetComponent<SpriteRenderer>().bounds.size.x -0.2f) * trainScript.Extentions.Count, 0f, 0f);
        extension.transform.position -= new Vector3(1.88f, 0.82f, 0f);
        extension.transform.parent = train.transform;
        trainScript.Extentions.Add(extension);

        GetComponent<UIController>().selectableTrainParts.AddRange(extension.GetComponent<Extention>().interactableUISlots);
        extension.GetComponent<Extention>().SetSlotExtensionId(trainScript.Extentions.Count);

    }




}