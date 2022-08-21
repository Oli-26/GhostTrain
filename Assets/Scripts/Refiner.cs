using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Refiner : TimeEffected
{
    bool refining = true;
    ResourceType refiningType = ResourceType.Metal;
    List<ResourceType> refinableTypes = new List<ResourceType>() {ResourceType.Metal};
    Inventory invent;
    public GameObject effect;
    public GameObject bonusObject;
    RefinerBonus bonus;

    float cooldown = 5f;
    float _activeCooldown = 5f;

    int inputAmount = 1;

    float metalRefineSuccessChance = 50f;
    float bonusChance = 20f;

    void Start()
    {
        invent = GameObject.Find("Controller").GetComponent<Inventory>();
        bonus = bonusObject.GetComponent<RefinerBonus>();
    }

    void Update()
    {
        if(refining && _activeCooldown <= 0){
            Refine();
            _activeCooldown = cooldown;
        }else{
            _activeCooldown -= getTimePassed();
        }
    }

    void Refine(){
        switch(refiningType){
            case ResourceType.Metal:
                if(invent.GetResourceAmount(ResourceType.Stone) >= inputAmount){
                    invent.LoseResource(ResourceType.Stone, inputAmount);
                    for(int i = 0; i < inputAmount; i++){
                        if(randomChance(metalRefineSuccessChance)){
                            invent.GainResource(ResourceType.Metal, 1, transform.position);
                            if(randomChance(bonusChance)){
                                bonus.SetUpBonus(2.5f);
                            }
                        }
                        
                    }
                }
                break;
            default:
                return;
        }
    }

    public void Toggle(){
        refining = !refining;

        effect.SetActive(refining);
    }

    public bool IsOn(){
        return refining;
    }

    public void SetRefiningType(ResourceType type){
        refiningType = type;
    }

    public ResourceType GetRefineType(){
        return refiningType;
    }

    public void GrantBonus(){
        switch(refiningType){
            case ResourceType.Metal:
                invent.GainResource(ResourceType.Metal, inputAmount, transform.position);
                break;
            default:
                return;
        }
    }
}
