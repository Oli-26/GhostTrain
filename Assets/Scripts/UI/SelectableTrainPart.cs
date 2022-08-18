using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableTrainPart : UIElement
{
    GameObject gameController;
    Color baseColor;
    Color inUseColor = new Color(0f, 0f, 0f, 0.6f);
    bool selected = false;
    bool highLighted = false;
    int highLightTick = 0;
    bool beingUsed = false;

    public int slotId = -1;
    public int extentionId = -1;
    void Start()
    {
        gameController = GameObject.Find("Controller");
        baseColor = GetComponent<SpriteRenderer>().color;
    }

    void Update()
    {
        if(highLighted && highLightTick > 0){
            highLightTick--;
        }
        if(highLighted && highLightTick == 0){
            highLighted = false;
            GetComponent<SpriteRenderer>().color = beingUsed ? inUseColor : baseColor;;
        }
        
    }

    public void Interact(){
        gameController.GetComponent<UIController>().trySelectObject(gameObject);
    }

    public void Select(){
        GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 0f, 1f);
        selected = true;
        highLighted = false;
    }

    public void DeSelect(){
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

    public void InUse(){
        GetComponent<SpriteRenderer>().color = inUseColor;
        beingUsed = true;
    }
}
