using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int woodCount = 25;
    public int stoneCount = 15;
    public int metalCount = 15;
    public int foodCount = 0;

    public GameObject ResourceGainedEffect;

    void Start(){
        
    }

    public void GainResource(ResourceType type, int amount, Vector3 positionForEffect){
        NewMethod(type, positionForEffect, amount);
        
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
            default:
                Debug.Log("Resource " + type + " is not recognised");
                break;
        }

        GetComponent<UIController>().UpdateResourceValues(woodCount, stoneCount, metalCount, foodCount);
    }

    private void NewMethod(ResourceType type, Vector3 positionForEffect, int amount)
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
            default:
                Debug.Log("Resource " + type + " is not recognised");
                break;
        }

        GetComponent<UIController>().UpdateResourceValues(woodCount, stoneCount, metalCount, foodCount);
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
            default:
                return false;
        }
    }
}
public enum ResourceType {Wood, Stone, Metal, Food}
