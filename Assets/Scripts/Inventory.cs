using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int woodCount = 25;
    public int stoneCount = 15;
    public int metalCount = 15;
    public int fishCount = 0;

    public GameObject ResourceGainedEffect;

    void Start(){
        
    }

    public void GainResource(ResourceType type, int amount, Vector3 positionForEffect){
        Instantiate(ResourceGainedEffect, positionForEffect, Quaternion.identity).GetComponent<ResourceGainedEffect>().SetResource(type);
        
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
            case ResourceType.Fish:
                fishCount+=amount;
                break;
            default:
                Debug.Log("Resource " + type + " is not recognised");
                break;
        }

        GetComponent<UIController>().UpdateResourceValues(woodCount, stoneCount, metalCount);
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
             case ResourceType.Fish:
                fishCount-=amount;
                break;
            default:
                Debug.Log("Resource " + type + " is not recognised");
                break;
        }

        GetComponent<UIController>().UpdateResourceValues(woodCount, stoneCount, metalCount);
    }

    public int GetResourceAmount(ResourceType type){
        switch(type){
            case ResourceType.Wood:
                return woodCount;
            case ResourceType.Stone:
                return stoneCount;
            case ResourceType.Metal:
                return metalCount;
            case ResourceType.Fish:
                return fishCount;
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
            case ResourceType.Fish:
                return fishCount >= amount;
            default:
                return false;
        }
    }
}
public enum ResourceType {Wood, Stone, Metal, Fish}
