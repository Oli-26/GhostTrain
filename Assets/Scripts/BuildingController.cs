using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    public GameObject grabberPrefabTopSide;
    public GameObject grabberPrefabBotSide;
    public GameObject refinerPrefab;
    void Start()
    {
        
    }

    void Update()
    {

    }

    public void ConstructAddOn(AddOnType type, int extentionNumber, int slotNumber){
        List<GameObject> extentions = GameObject.Find("Train").GetComponent<TrainCore>().Extentions;
        GameObject slot = extentions[extentionNumber-1].GetComponent<Extention>().GetSlot(slotNumber);

        if(type == AddOnType.Grabber){
            CreateGrabber(extentions[extentionNumber-1], slot, slotNumber <= 1);
            GetComponent<UIController>().selectedObject.GetComponent<SelectableTrainPart>().InUse();
        }

        if(type == AddOnType.Refiner){
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




}
