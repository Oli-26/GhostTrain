using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Refiner : TimeEffected
{
    bool refining = true;
    ResourceType refiningType = ResourceType.Stone;
    List<ResourceType> refinableTypes = new List<ResourceType>() {ResourceType.Stone};
    Inventory invent;
    public GameObject effect;

    float cooldown = 5f;
    float _activeCooldown = 5f;

    int inputAmount = 1;

    float stoneRefineSuccessChance = 50f;
    void Start()
    {
        invent = GameObject.Find("Controller").GetComponent<Inventory>();
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
            case ResourceType.Stone:
                if(invent.GetResourceAmount(ResourceType.Stone) >= inputAmount){
                    invent.LoseResource(ResourceType.Stone, inputAmount);
                    for(int i = 0; i < inputAmount; i++){
                        if(randomChance(stoneRefineSuccessChance)){
                            invent.GainResource(ResourceType.Metal, 1);
                        }
                    }
                }
                break;
            default:
                return;
        }
    }

    bool randomChance(float percent){
        float random = Random.Range(0,100f);
        Debug.Log(random);
        return  random < percent;
    }

    public void Toggle(){
        refining = !refining;

        effect.SetActive(refining);
    }
}
