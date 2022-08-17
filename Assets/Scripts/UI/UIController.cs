﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{

    public List<GameObject> selectableTrainParts = new List<GameObject>();
    public List<GameObject> nonInteractableUIParts = new List<GameObject>();

    public List<GameObject> allText = new List<GameObject>();

    bool buildUIActive = false;

    public GameObject selectedObject;

    public GameObject clickDecal;


    void Start()
    {
         foreach(GameObject uiElement in allText){
            uiElement.GetComponent<MeshRenderer>().sortingOrder = 15;
        }
    }

    void Update()
    {
        TryClick();
        TryHighlight();
    }

    void TryClick(){
        if (buildUIActive && Input.GetMouseButtonDown(0)){
            foreach(GameObject uiElement in selectableTrainParts){
                if(mouseInsideObject(uiElement)){
                    //Instantiate(clickDecal, mouseToWorld(), Quaternion.identity);
                    SelectableTrainPart selectableTrainPart = uiElement.GetComponent<SelectableTrainPart>();
                    if(selectableTrainPart != null){
                        selectableTrainPart.Interact();
                    }
                }
            }
        }
    }
    
    // Probably want to reduce the time complexity from iterating through these lists twice
    void TryHighlight(){
        if (buildUIActive && !Input.GetMouseButtonDown(0)){
            foreach(GameObject uiElement in selectableTrainParts){
                if(mouseInsideObject(uiElement)){
                    SelectableTrainPart selectableTrainPart = uiElement.GetComponent<SelectableTrainPart>();
                    if(selectableTrainPart != null){
                        selectableTrainPart.HighLight();
                    }
                }
            }
        }
    }

    
    bool mouseInsideObject(GameObject obj){
        Vector3 objectPosition = obj.transform.position;
        Vector3 localScale = obj.transform.localScale;

        Vector3 bottomLeftCorner = new Vector3(objectPosition.x - localScale.x, objectPosition.y - localScale.y, 0f);
        Vector3 topRightCorder = new Vector3(objectPosition.x + localScale.x, objectPosition.y + localScale.y, 0f);
        

        return pointInsideRectangle(mouseToWorld(), bottomLeftCorner, topRightCorder);
    }

    Vector3 mouseToWorld(){
        Vector3 mousePositionWithZ = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return new Vector3(mousePositionWithZ.x, mousePositionWithZ.y, 0f);
    }

    bool pointInsideRectangle(Vector3 point, Vector3 lowCorner, Vector3 highCorner){
        if(point.x > lowCorner.x && point.x < highCorner.x && point.y > lowCorner.y && point.y < highCorner.y){
            return true;
        }
        return false;
    }

    public void toggleBuildingUI(){
        buildUIActive = !buildUIActive;
        
        if(buildUIActive){
            TimeController.Paused = true;
        }else{
            TimeController.Paused = false;
        }

        foreach(GameObject uiElement in selectableTrainParts){
            uiElement.SetActive(buildUIActive);
        }

        foreach(GameObject uiElement in nonInteractableUIParts){
            uiElement.SetActive(buildUIActive);
        }
    }

    public void trySelectObject(GameObject selected){
        if(buildUIActive){
            if(selectedObject != null){
                SelectableTrainPart selectedObjectSelectableTrainPart = selectedObject.GetComponent<SelectableTrainPart>();
                if(selectedObjectSelectableTrainPart != null){
                    selectedObjectSelectableTrainPart.DeSelect();
                }
            }

            selectedObject = selected;
            SelectableTrainPart selectableTrainPart = selectedObject.GetComponent<SelectableTrainPart>();
            if(selectableTrainPart != null){
                selectableTrainPart.Select();
            }
        }
    }
}