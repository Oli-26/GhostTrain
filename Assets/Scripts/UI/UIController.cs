using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UIController : MonoBehaviour
{
    public List<GameObject> baseUIParts = new List<GameObject>();
    public List<GameObject> allText = new List<GameObject>();
    
    public bool buildUIActive;
    
    public GameObject RefinerOptions;
    public GameObject GrabberOptions;
    public GameObject AddOnShop;
    public GameObject ExtensionShop;
    public TrainCore trainCore;

    public SelectableTrainPart selectedObject;
    public int selectedSlotId = -1;
    public int selectedExtentionId = -1;
    
    // TEXT
    GameObject woodCountText;
    GameObject stoneCountText;
    GameObject metalCountText;
    GameObject foodCountText;

    void Start()
    {
        CollectAllSceneText();
        allText.ForEach(uiElement => uiElement.GetComponent<MeshRenderer>().sortingOrder = 15);
        
        woodCountText = allText.Where(text => text.GetComponent<UIText>().UITag == "WoodCount").ToList().First();
        stoneCountText = allText.Where(text => text.GetComponent<UIText>().UITag == "StoneCount").ToList().First();
        metalCountText = allText.Where(text => text.GetComponent<UIText>().UITag == "MetalCount").ToList().First();
        foodCountText =  allText.Where(text => text.GetComponent<UIText>().UITag == "FoodCount").ToList().First();

        Inventory invent = GetComponent<Inventory>();
        UpdateResourceValues(invent.woodCount, invent.stoneCount, invent.metalCount, invent.foodCount);
    }

    void Update()
    {
    }
    
    private void CollectAllSceneText(){
        allText = Resources.FindObjectsOfTypeAll<GameObject>().Where(text => text.tag == "UIText").ToList();
    }
    
    public void toggleBuildingUI()
    {
        buildUIActive = !buildUIActive;

        if (buildUIActive)
        {
            TimeController.Paused = true;
        }
        else
        {
            TimeController.Paused = false;
        }
        
        trainCore.ShowGhostExtension(buildUIActive);

        RefreshUiElements();
    }

    public void RefreshUiElements()
    {
        trainCore.Extensions
            .SelectMany(ext => ext.interactableUISlots).ToList()
            .ForEach(slot => slot.SetActive(buildUIActive));
        trainCore.Extensions
            .SelectMany(ext => ext.otherInteractables).ToList()
            .ForEach(slot => slot.SetActive(buildUIActive));
        baseUIParts.ForEach(part => part.SetActive(buildUIActive));
    }

    public void SelectObject(SelectableTrainPart selected)
    {
        if (buildUIActive)
        {
            if (selectedObject != null)
            {
                selectedObject.Deselect();
            }

            selectedObject = selected;
            selectedSlotId = selectedObject.slotId;
            selectedExtentionId = selectedObject.extentionId;
            LoadCorrectGUI();
        }
    }

    public void LoadCorrectGUI(bool loadExtensionShop = false)
    {
        if(loadExtensionShop){
            SetUpExtensionShop();
            return;
        }

        if (selectedExtentionId == -1 || selectedSlotId == -1)
        {
            RefinerOptions.SetActive(false);
            AddOnShop.SetActive(true);
            ExtensionShop.SetActive(false);
            GrabberOptions.SetActive(false);
            return;
        }

        GameObject slot = trainCore.Extensions[selectedExtentionId - 1].GetComponent<Extension>().GetSlot(selectedSlotId);
        GameObject addOn = slot.GetComponent<Slot>().GetAddOn();

        if (addOn != null)
        {
            Grabber grabber = addOn.GetComponent<Grabber>();
            if (grabber != null)
            {
                SetUpGrabberOptions(grabber);
            }

            Refiner refiner = addOn.GetComponent<Refiner>();
            if (refiner != null)
            {
                SetUpRefinerOptions(refiner);
            }
        }
        else
        {
            RefinerOptions.SetActive(false);
            AddOnShop.SetActive(true);
            GrabberOptions.SetActive(false);
            ExtensionShop.SetActive(false);
        }
    }

    public void UpdateResourceValues(int wood, int stone, int metal, int food)
    {
        woodCountText.GetComponent<TextMesh>().text = wood.ToString();
        stoneCountText.GetComponent<TextMesh>().text = stone.ToString();
        metalCountText.GetComponent<TextMesh>().text = metal.ToString();
        
        foodCountText.GetComponent<TextMesh>().text = food.ToString();
    }

    public void SetUpRefinerOptions(Refiner refiner){
        RefinerOptions.SetActive(true);
        AddOnShop.SetActive(false);
        GrabberOptions.SetActive(false);
        ExtensionShop.SetActive(false);

        SpriteRenderer metalOption = GetNestedChild(RefinerOptions, new string[]{"Metal option", "Option"}).GetComponent<SpriteRenderer>();
        SpriteRenderer toggleOption = GetNestedChild(RefinerOptions, new string[]{"ToggleRefiner"}).GetComponent<SpriteRenderer>();

        metalOption.color = new Color(1f, 1f, 1f, 1f);
        toggleOption.color = new Color(1f, 1f, 1f, 1f);

        if(!refiner.IsOn()){ 
            toggleOption.color = new Color(0f, 0f, 0f, 1f);
        }

        if(refiner.GetRefineType() == ResourceType.Metal){
            metalOption.color = new Color(0f, 0f, 0f, 1f);
        }
    }

    public void SetUpGrabberOptions(Grabber grabber){
        RefinerOptions.SetActive(false);
        AddOnShop.SetActive(false);
        GrabberOptions.SetActive(true);
        ExtensionShop.SetActive(false);

        SpriteRenderer woodOption = GetNestedChild(GrabberOptions, new string[]{"Wood option", "Option"}).GetComponent<SpriteRenderer>();
        SpriteRenderer stoneOption = GetNestedChild(GrabberOptions, new string[]{"Stone option", "Option"}).GetComponent<SpriteRenderer>();
        SpriteRenderer noneOption = GetNestedChild(GrabberOptions, new string[]{"None option", "Option"}).GetComponent<SpriteRenderer>();

        woodOption.color = new Color(1f, 1f, 1f, 1f);
        stoneOption.color = new Color(1f, 1f, 1f, 1f);
        noneOption.color = new Color(1f, 1f, 1f, 1f);

        if(grabber.GetFocusActive()){
            switch(grabber.GetFocus()){
                case ResourceType.Wood:
                    woodOption.color = new Color(0f, 0f, 0f, 1f);
                    break;
                case ResourceType.Stone:
                    stoneOption.color = new Color(0f, 0f, 0f, 1f);
                    break;
                default:
                    break;
            }
        }else{
            noneOption.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, 1f);
        }
    }

    public void SetUpExtensionShop(){
        RefinerOptions.SetActive(false);
        AddOnShop.SetActive(false);
        GrabberOptions.SetActive(false);
        ExtensionShop.SetActive(true);
    }


    GameObject GetNestedChild(GameObject baseObject, string[] names){
        Transform latest = baseObject.transform.Find(names[0]);
        for(int i = 1; i < names.Length; i++){
            latest = latest.Find(names[i]);
        }

        return latest.gameObject;
    }
}