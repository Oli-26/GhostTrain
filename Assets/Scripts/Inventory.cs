using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    int woodCount = 0;
    int stoneCount = 0;
    int metalCount = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GainResource(ResourceType type, int amount){
        switch(type){
            case ResourceType.Wood:
                woodCount+=amount;
                break;
            case ResourceType.Stone:
                stoneCount+=amount;
                break;
            case ResourceType.Metal:
                metalCount=+amount;
                break;
            default:
                Debug.Log("Resource " + type + " is not recognised");
                break;
        }

        GetComponent<UIController>().UpdateResourceValues(woodCount, stoneCount, metalCount);
    }
}

public enum ResourceType {Wood, Stone, Metal}
