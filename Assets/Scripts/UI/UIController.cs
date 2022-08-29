using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UIController : MonoBehaviour
{
    public List<GameObject> baseUIParts = new List<GameObject>();
    public GameObject middleMenuBase;
    public Dictionary<string, TextMesh> allText;
    
    public bool buildUIActive;
    public Dictionary<string, GameObject> menus;

    public TrainCore trainCore;
    Inventory invent;
    Purchaser purchaser;

    public SelectableTrainPart selectedObject;
    public int selectedSlotId = -1;
    public int selectedExtentionId = -1;

    Color canAffordColor = new Color(1f, 1f, 1f, 1f);
    Color canNotAffordColor = new Color(1f, 0.5f, 0.5f, 1f);
    
    // TEXT
    TextMesh woodCountText;
    TextMesh stoneCountText;
    TextMesh metalCountText;
    TextMesh foodCountText;
    TextMesh weightText;
    TextMesh moneyText;

    void Start()
    {
        SetUpText();
        SetUpMenus();
        invent = GetComponent<Inventory>();
        purchaser = GetComponent<Purchaser>();
        UpdateResourceValues();
    }

    void SetUpText(){
        allText = new Dictionary<string, TextMesh>();
        List<GameObject> texts = Resources.FindObjectsOfTypeAll<GameObject>().Where(text => text.tag == "UIText").ToList();

        foreach(GameObject text in texts){
            text.GetComponent<MeshRenderer>().sortingOrder = 15;
            allText[text.GetComponent<UIText>().UITag] = text.GetComponent<TextMesh>();
        }
    }

    void SetUpMenus(){
        menus = new Dictionary<string, GameObject>();
        List<GameObject> allMenus = Resources.FindObjectsOfTypeAll<GameObject>().Where(menu => menu.tag == "Menu").ToList();

        foreach(GameObject menu in allMenus){
            menus.Add(menu.GetComponent<Menu>().UITag, menu);
        }
    }

    void Update()
    {
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

        SetAllMenusInactive();

        if(loadExtensionShop){
            SetUpExtensionShop();
            return;
        }

        if(selectedObject == null){
            return;
        }

        if(selectedObject.type == SelectableType.LivingExtensionMenu){
            SetUpLivingShop();
            return;
        }

        if(selectedObject.type == SelectableType.ResearchExtensionMenu){
            SetUpResearchShop();
            return;
        }

        if (selectedExtentionId == -1 || selectedSlotId == -1)
        {
            SetUpAddOnsShop();
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
            SetUpAddOnsShop();
        }
    }

    public void UpdateResourceValues()
    {
        allText["WoodCount"].text = invent.woodCount.ToString();
        allText["StoneCount"].text = invent.stoneCount.ToString();
        allText["MetalCount"].text = invent.metalCount.ToString();
        allText["FoodCount"].text = invent.foodCount.ToString();
        allText["Money"].text = invent.Money.ToString();
        allText["Weight"].text = invent.GetCurrentWeight().ToString();
        float nonRedColors = invent.AtMaxWeight() ? 0f : 1f;
        allText["Weight"].color = new Color(1f, nonRedColors, nonRedColors, 1f);
    }

    public void SetUpAddOnsShop(){
        GameObject menu = menus["AddOns"];
        menu.SetActive(true);

        GameObject buyGrabberButton = GetNestedChild(menu, new string[]{"Card-Grabber", "BuyButton"});
        GameObject buyRefinerButton = GetNestedChild(menu, new string[]{"Card-Refiner", "BuyButton"});
        GameObject buyCropPlotButton = GetNestedChild(menu, new string[]{"Card-CropPlot", "BuyButton"});

        if(purchaser.HasResourceForPurchase(purchaser.grabberCost, false)){
            buyGrabberButton.GetComponent<SpriteRenderer>().color = canAffordColor; 
        }else{
            buyGrabberButton.GetComponent<SpriteRenderer>().color = canNotAffordColor; 
        }

        if(purchaser.HasResourceForPurchase(purchaser.refinerCost, false)){
            buyRefinerButton.GetComponent<SpriteRenderer>().color = canAffordColor; 
        }else{
            buyRefinerButton.GetComponent<SpriteRenderer>().color = canNotAffordColor; 
        }

        if(purchaser.HasResourceForPurchase(purchaser.grabberCost, false)){
            buyCropPlotButton.GetComponent<SpriteRenderer>().color = canAffordColor; 
        }else{
            buyCropPlotButton.GetComponent<SpriteRenderer>().color = canNotAffordColor; 
        }
    }

    public void SetUpRefinerOptions(Refiner refiner){
        GameObject menu = menus["Refiner"];
        menu.SetActive(true);

        SpriteRenderer metalOption = GetNestedChild(menu, new string[]{"Metal option", "Option"}).GetComponent<SpriteRenderer>();
        SpriteRenderer toggleOption = GetNestedChild(menu, new string[]{"ToggleRefiner"}).GetComponent<SpriteRenderer>();

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
        SetAllMenusInactive();
        GameObject menu = menus["Grabber"];
        menu.SetActive(true);

        SpriteRenderer woodOption = GetNestedChild(menu, new string[]{"Wood option", "Option"}).GetComponent<SpriteRenderer>();
        SpriteRenderer stoneOption = GetNestedChild(menu, new string[]{"Stone option", "Option"}).GetComponent<SpriteRenderer>();
        SpriteRenderer noneOption = GetNestedChild(menu, new string[]{"None option", "Option"}).GetComponent<SpriteRenderer>();

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
        GameObject menu = menus["Extensions"]; 
        menu.SetActive(true);

        GameObject buyExtensionButton = GetNestedChild(menu, new string[]{"Card-AddOnExtension", "BuyButton"});
        GameObject buyStorageButton = GetNestedChild(menu, new string[]{"Card-StorageExtension", "BuyButton"});
        GameObject buyLivingButton = GetNestedChild(menu, new string[]{"Card-Living", "BuyButton"});
        GameObject buyResearchButton = GetNestedChild(menu, new string[]{"Card-Research", "BuyButton"});
        
        if(purchaser.HasResourceForPurchase(purchaser.extensionCost, false)){
            buyExtensionButton.GetComponent<SpriteRenderer>().color = canAffordColor; 
        }else{
            buyExtensionButton.GetComponent<SpriteRenderer>().color = canNotAffordColor; 
        }

        if(purchaser.HasResourceForPurchase(purchaser.storageExtensionCost, false)){
            buyStorageButton.GetComponent<SpriteRenderer>().color = canAffordColor; 
        }else{
            buyStorageButton.GetComponent<SpriteRenderer>().color = canNotAffordColor; 
        }

        if(purchaser.HasResourceForPurchase(purchaser.livingExtensionCost, false)){
            buyLivingButton.GetComponent<SpriteRenderer>().color = canAffordColor; 
        }else{
            buyLivingButton.GetComponent<SpriteRenderer>().color = canNotAffordColor; 
        }

        if(purchaser.HasResourceForPurchase(purchaser.researchExtensionCost, false)){
            buyResearchButton.GetComponent<SpriteRenderer>().color = canAffordColor; 
        }else{
            buyResearchButton.GetComponent<SpriteRenderer>().color = canNotAffordColor; 
        }
    }

    public void SetUpLivingShop(){
        GameObject menu = menus["Living"];
        menu.SetActive(true);

        
        Extension extension = trainCore.Extensions[selectedExtentionId - 1].GetComponent<Extension>();
        
        GameObject buyNPC1 = menu.transform.Find("Card-NPC1-buy").gameObject;
        GameObject buyNPC2 = menu.transform.Find("Card-NPC2-buy").gameObject;
        GameObject viewNPC1 = menu.transform.Find("Card-NPC1").gameObject;
        GameObject viewNPC2 = menu.transform.Find("Card-NPC2").gameObject;

        int count = extension.NPCS.Count;

        if(count >= 1){
            buyNPC1.SetActive(false); 
            viewNPC1.SetActive(true);
        }else{
            buyNPC1.SetActive(true); 
            viewNPC1.SetActive(false);
        }

        if(count== 2){
            buyNPC2.SetActive(false); 
            viewNPC2.SetActive(true);
        }else{
            buyNPC2.SetActive(true); 
            viewNPC2.SetActive(false);
        }

        
    }

    public void SetUpResearchShop(){
        menus["Research"].SetActive(true);
    }


    GameObject GetNestedChild(GameObject baseObject, string[] names){
        Transform latest = baseObject.transform.Find(names[0]);
        for(int i = 1; i < names.Length; i++){
            latest = latest.Find(names[i]);
        }

        return latest.gameObject;
    }

    public void SetAllMenusInactive(){
        foreach(GameObject menu in menus.Values.ToList()){
            menu.SetActive(false);
        }
    }
}