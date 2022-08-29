using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableTrainPart : UIElement
{
    GameObject gameController;
    Color baseColor;
    Color inUseColor = new Color(0f, 0f, 0f, 0.6f);
    Color selectedColor = new Color(1f, 1f, 0f, 1f);
    bool selected = false;
    bool highLighted = false;
    int highLightTick = 0;
    bool beingUsed = false;

    public int slotId = -1;
    public int extentionId = -1;
    private UIController uiController;
    public SelectableType type;

    void Start()
    {
        gameController = GameObject.Find("Controller");
        uiController = gameController.GetComponent<UIController>();
        baseColor = GetComponent<SpriteRenderer>().color;
    }

    void Update()
    {
        if(highLighted && highLightTick > 0){
            highLightTick--;
        }
        if(highLighted && highLightTick == 0){
            highLighted = false;
            if(selected){
                GetComponent<SpriteRenderer>().color = selectedColor;
                return;
            }
            
            GetComponent<SpriteRenderer>().color = beingUsed ? inUseColor : baseColor;
        }
        
    }

    public void Select(){
        GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 0f, 1f);
        selected = true;
        highLighted = false;
        uiController.SelectObject(this);
    }

    public void Deselect(){
        GetComponent<SpriteRenderer>().color = beingUsed ? inUseColor : baseColor;
        selected = false;
    }

    public void HighLight(){
        if(selected == false){
            GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.6f, 0.75f, 1f);
            highLightTick = 15;
            highLighted = true;
        } 
    }
    
    void OnMouseOver()
    {
        HighLight();
    }

    private void OnMouseUpAsButton()
    {
        Select();
    }

    public void InUse(){
        GetComponent<SpriteRenderer>().color = inUseColor;
        beingUsed = true;
    }
}

public enum SelectableType {AddOnSlot, LivingExtensionMenu, ResearchExtensionMenu}