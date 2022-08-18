using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    public GameObject grabberPrefabTopSide;
    public GameObject grabberPrefabBotSide;
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

    }

    public void CreateGrabber(GameObject extention, GameObject slot, bool isTop){
        //Debug.Log(slot.transform.position);
        GameObject grabber;
        if(isTop){
            grabber = Instantiate(grabberPrefabTopSide, slot.transform.position, Quaternion.identity);
        }else{
            grabber = Instantiate(grabberPrefabBotSide, slot.transform.position, Quaternion.identity);
        }
        
        slot.GetComponent<Slot>().CreateAddOn(grabber);
        grabber.transform.parent = slot.transform;
        //grabber.GetComponent<Grabber>().basePoint = extention.GetComponent<Extention>().baseObject.transform;
        grabber.GetComponent<Grabber>().isTopSide = isTop;
    }




}
