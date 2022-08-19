using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UIController : MonoBehaviour
{
    public List<GameObject> selectableTrainParts = new List<GameObject>();
    public List<GameObject> baseUIParts = new List<GameObject>();
    public List<GameObject> allText = new List<GameObject>();
    
    bool buildUIActive;
    
    public GameObject RefinerOptions;
    public GameObject AddOnShop;
    public TrainCore trainCore;

    public SelectableTrainPart selectedObject;
    public int selectedSlotId = -1;
    public int selectedExtentionId = -1;
    
    // TEXT
    GameObject woodCountText;
    GameObject stoneCountText;
    GameObject metalCountText;


    void Start()
    {
        CollectAllSceneText();
        allText.ForEach(uiElement => uiElement.GetComponent<MeshRenderer>().sortingOrder = 15);
        
        woodCountText = allText.Where(text => text.GetComponent<UIText>().UITag == "WoodCount").ToList()
            .FirstOrDefault();
        stoneCountText = allText.Where(text => text.GetComponent<UIText>().UITag == "StoneCount").ToList().First();
        metalCountText = allText.Where(text => text.GetComponent<UIText>().UITag == "MetalCount").ToList().First();

        Inventory invent = GetComponent<Inventory>();
        UpdateResourceValues(invent.woodCount, invent.stoneCount, invent.metalCount);
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

        RefreshUiElements();
    }

    public void RefreshUiElements()
    {
        trainCore.Extentions
            .SelectMany(ext => ext.interactableUISlots).ToList()
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

    public void LoadCorrectGUI()
    {
        if (selectedExtentionId == -1 || selectedSlotId == -1)
        {
            RefinerOptions.SetActive(false);
            AddOnShop.SetActive(true);
            return;
        }

        GameObject slot = trainCore.Extentions[selectedExtentionId - 1].GetComponent<Extention>().GetSlot(selectedSlotId);
        GameObject addOn = slot.GetComponent<Slot>().GetAddOn();

        if (addOn != null)
        {
            if (addOn.GetComponent<Grabber>() != null)
            {
                RefinerOptions.SetActive(false);
                AddOnShop.SetActive(true);
            }

            if (addOn.GetComponent<Refiner>() != null)
            {
                RefinerOptions.SetActive(true);
                AddOnShop.SetActive(false);
            }
        }
        else
        {
            RefinerOptions.SetActive(false);
            AddOnShop.SetActive(true);
        }
    }

    public void UpdateResourceValues(int wood, int stone, int metal)
    {
        woodCountText.GetComponent<TextMesh>().text = wood.ToString();
        stoneCountText.GetComponent<TextMesh>().text = stone.ToString();
        metalCountText.GetComponent<TextMesh>().text = metal.ToString();
    }
}