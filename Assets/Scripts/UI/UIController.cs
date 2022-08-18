using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UIController : MonoBehaviour
{

    public List<GameObject> selectableTrainParts = new List<GameObject>();
    public List<GameObject> nonInteractableUIParts = new List<GameObject>();
    public List<GameObject> purchaseButtons = new List<GameObject>();

    public List<GameObject> addOnOptionButtons = new List<GameObject>();

    public List<GameObject> allText = new List<GameObject>();

    bool buildUIActive = false;

    public GameObject selectedObject;
    public int selectedSlotId = -1;
    public int selectedExtentionId = -1;

    public GameObject RefinerOptions;
    public GameObject AddOnShop;

    // TEXT
    GameObject woodCountText;
    GameObject stoneCountText;
    GameObject metalCountText;


    void Start()
    {
         foreach(GameObject uiElement in allText){
            uiElement.GetComponent<MeshRenderer>().sortingOrder = 15;
        }

        woodCountText = allText.Where(text => text.GetComponent<UIText>().UITag == "WoodCount").ToList().FirstOrDefault();
        stoneCountText = allText.Where(text => text.GetComponent<UIText>().UITag == "StoneCount").ToList().First();
        metalCountText = allText.Where(text => text.GetComponent<UIText>().UITag == "MetalCount").ToList().First();

        Inventory invent = GetComponent<Inventory>();
        UpdateResourceValues(invent.woodCount, invent.stoneCount, invent.metalCount);
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
                    uiElement.GetComponent<SelectableTrainPart>().Interact();
                    return;
                }
            }

            foreach(GameObject uiElement in purchaseButtons){
                if(mouseInsideObject(uiElement)){
                    uiElement.GetComponent<PurchaseAddOnButton>().Interact();
                    return;
                }
            }

            foreach(GameObject uiElement in addOnOptionButtons){
                if(mouseInsideObject(uiElement)){
                    uiElement.GetComponent<AddOnOptionButton>().Interact();
                    return;
                }
            }
        }
    }
    
    // Probably want to reduce the time complexity from iterating through these lists twice
    void TryHighlight(){
        if (buildUIActive && !Input.GetMouseButtonDown(0)){
            foreach(GameObject uiElement in selectableTrainParts){
                if(mouseInsideObject(uiElement)){
                    uiElement.GetComponent<SelectableTrainPart>().HighLight();
                }
            }
        }
    }

    
    bool mouseInsideObject(GameObject obj){
        Vector3 objectPosition = obj.transform.position;
        Vector3 localScale = obj.GetComponent<SpriteRenderer>().bounds.size;

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
                Deselect();
            }

            selectedObject = selected;
            SelectableTrainPart selectableTrainPart = selectedObject.GetComponent<SelectableTrainPart>();
            if(selectableTrainPart != null){
                selectableTrainPart.Select();
                selectedSlotId = selectableTrainPart.slotId;
                selectedExtentionId = selectableTrainPart.extentionId;

                LoadCorrectGUI();
            }
        }
    }

    public void LoadCorrectGUI(){
        List<GameObject> extentions = GameObject.Find("Train").GetComponent<TrainCore>().Extentions;
                GameObject slot = extentions[selectedExtentionId-1].GetComponent<Extention>().GetSlot(selectedSlotId);
                GameObject addOn = slot.GetComponent<Slot>().GetAddOn();

                if(addOn != null){
                    if(addOn.GetComponent<Grabber>() != null){
                        RefinerOptions.SetActive(false);
                        AddOnShop.SetActive(true);
                    }

                    if(addOn.GetComponent<Refiner>() != null){
                        RefinerOptions.SetActive(true);
                        AddOnShop.SetActive(false);
                    }
                }else{
                    RefinerOptions.SetActive(false);
                    AddOnShop.SetActive(true);
                }
    }

    public void Deselect(){
         SelectableTrainPart selectedObjectSelectableTrainPart = selectedObject.GetComponent<SelectableTrainPart>();
         if(selectedObjectSelectableTrainPart != null){
            selectedObjectSelectableTrainPart.DeSelect();
         }
    }

    public void UpdateResourceValues(int wood, int stone, int metal){
        woodCountText.GetComponent<TextMesh>().text = wood.ToString();
        stoneCountText.GetComponent<TextMesh>().text = stone.ToString();
        metalCountText.GetComponent<TextMesh>().text = metal.ToString();
    }
}
