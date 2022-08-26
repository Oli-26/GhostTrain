using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropPlot : TimeEffected
{

    Inventory invent;
    public GameObject bonusObject;
    CropPlotBonus bonus;
    
    public GameObject effect1;
    public GameObject effect2;
    public GameObject effect3;

    float cooldown = 5f;
    float _activeCooldown = 5f;

    int currentFoodCount = 0;
    int maxFoodCount = 10;
    int foodPerGrowth = 1;

    void Start()
    {
        invent = GameObject.Find("Controller").GetComponent<Inventory>();
        bonus = bonusObject.GetComponent<CropPlotBonus>();
    }

    void Update()
    {
        if(_activeCooldown <= 0){
            Grow();
            _activeCooldown = cooldown;
        }else{
            _activeCooldown -= getTimePassed();
        }
    }

    private void OnMouseUpAsButton()
    {
        invent.GainResource(ResourceType.Food, currentFoodCount, transform.position);
        currentFoodCount = 0;
        SetEffect();
    }

    void Grow(){
        currentFoodCount += foodPerGrowth;
        if(currentFoodCount > maxFoodCount){
            currentFoodCount = maxFoodCount;
        }
        SetEffect();
    }

    void SetEffect(){
        effect1.SetActive(false);
        effect2.SetActive(false);
        effect3.SetActive(false);
        if(currentFoodCount == maxFoodCount){
            bonusObject.GetComponent<CropPlotBonus>().SetUpBonus();
        }
        if(currentFoodCount > maxFoodCount * 0.8){
            effect3.SetActive(true);
        }
        if(currentFoodCount > maxFoodCount * 0.5){
            effect2.SetActive(true);
        }
        if(currentFoodCount > maxFoodCount * 0.2){
            effect1.SetActive(true);
        }
    }

    public void GrantBonus(){
        invent.GainResource(ResourceType.Food, (int)Mathf.Round(maxFoodCount*0.2f), transform.position + new Vector3(0.1f, 0f, 0f));
    }
}
