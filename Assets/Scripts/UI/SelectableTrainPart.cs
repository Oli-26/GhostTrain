using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableTrainPart : UIElement
{
    GameObject gameController;
    Color baseColor;
    bool selected = false;
    bool highLighted = false;
    int highLightTick = 0;
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
            GetComponent<SpriteRenderer>().color = baseColor;
        }
        
    }

    public void Interact(){
        Debug.Log("ui clicked: " + gameObject);
        gameController.GetComponent<UIController>().trySelectObject(gameObject);
    }

    public void Select(){
        GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 0f, 1f);
        selected = true;
        highLighted = false;
    }

    public void DeSelect(){
        GetComponent<SpriteRenderer>().color = baseColor;
        selected = false;
    }

    public void HighLight(){
        if(selected == false){
            GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.6f, 0.75f, 1f);
            highLightTick = 15;
            highLighted = true;
        } 
    }
}
