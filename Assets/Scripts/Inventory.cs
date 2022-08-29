using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int woodCount = 25;
    public int stoneCount = 15;
    public int metalCount = 15;
    public int foodCount = 0;
    public int Money = 0;

    public GameObject ResourceGainedEffect;
    private TrainCore train;

    void Start(){
        train = FindObjectOfType<TrainCore>();
    }

    public void GainResource(ResourceType type, int amount, Vector3 positionForEffect){
        int currentWeight = GetCurrentWeight();
        int maxWeight = train.GetMaxWeight();

        if(currentWeight >= maxWeight){
            return;
        }
        if(currentWeight + amount > maxWeight){
            amount = maxWeight - currentWeight;
        }

        CreateResourceGainedEffect(type, positionForEffect, amount);

        switch(type){
            case ResourceType.Wood:
                woodCount+=amount;
                break;
            case ResourceType.Stone:
                stoneCount+=amount;
                break;
            case ResourceType.Metal:
                metalCount+=amount;
                break;
            case ResourceType.Food:
                foodCount+=amount;
                break;
            case ResourceType.Money:
                Money += amount;
                break;
            default:
                Debug.Log("Resource " + type + " is not recognised");
                break;
        }

        GetComponent<UIController>().UpdateResourceValues();
    }

    private void CreateResourceGainedEffect(ResourceType type, Vector3 positionForEffect, int amount)
    {
        var resourceGainedEffect = Instantiate(ResourceGainedEffect, positionForEffect, Quaternion.identity).GetComponent<ResourceGainedEffect>();
        resourceGainedEffect.SetResource(type);
        resourceGainedEffect.SetAmount(amount);
    }

    public void LoseResource(ResourceType type, int amount){
        switch(type){
            case ResourceType.Wood:
                woodCount-=amount;
                break;
            case ResourceType.Stone:
                stoneCount-=amount;
                break;
            case ResourceType.Metal:
                metalCount-=amount;
                break;
             case ResourceType.Food:
                foodCount-=amount;
                break;
            case ResourceType.Money:
                Money -= amount;
                break;
            default:
                Debug.Log("Resource " + type + " is not recognised");
                break;
        }

        GetComponent<UIController>().UpdateResourceValues();
    }

    public int GetResourceAmount(ResourceType type){
        switch(type){
            case ResourceType.Wood:
                return woodCount;
            case ResourceType.Stone:
                return stoneCount;
            case ResourceType.Metal:
                return metalCount;
            case ResourceType.Food:
                return foodCount;
            case ResourceType.Money:
                return Money;
                break;
            default:
                return 0;
        }
    }

    public bool HasResource(ResourceType type, int amount){
        switch(type){
            case ResourceType.Wood:
                return woodCount >= amount;
            case ResourceType.Stone:
                return stoneCount >= amount;
            case ResourceType.Metal:
                return metalCount >= amount;
            case ResourceType.Food:
                return foodCount >= amount;
            case ResourceType.Money:
                return Money >= amount;
                break;
            default:
                return false;
        }
    }

    public int GetCurrentWeight(){
        return woodCount + stoneCount + metalCount + foodCount/2;
    }

    public bool AtMaxWeight(){
        try{
            return GetCurrentWeight() >= train.GetMaxWeight();
        }catch{
            return true;
        }
        
    }
}
public enum ResourceType {Wood, Stone, Metal, Food, Money}
