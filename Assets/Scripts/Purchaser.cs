using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Purchaser : MonoBehaviour
{
    Inventory invent;
    
    void Start()
    {
        invent = FindObjectOfType<Inventory>();
    }

    void Update()
    {
        
    }

    public bool AttemptPurchase(PurchaseType type){
        switch(type){
            case PurchaseType.Grabber:
                return HasResourceForPurchase(grabberCost);
            case PurchaseType.Refiner:
                return HasResourceForPurchase(refinerCost);
            case PurchaseType.Extension:
                return HasResourceForPurchase(extentionCost);
            case PurchaseType.StorageExtension:
                return HasResourceForPurchase(storageExtentionCost);
            case PurchaseType.LivingExtension:
                return HasResourceForPurchase(livingExtentionCost);
            case PurchaseType.ResearchExtension:
                return HasResourceForPurchase(researchExtentionCost);
            case PurchaseType.CropPlot:
                return HasResourceForPurchase(cropPlotCost);
            case PurchaseType.Worker:
                return HasResourceForPurchase(workerCost);
            default:
                return false;
        }
    }


    public bool HasResourceForPurchase(Price price){
        if(!invent.HasResource(ResourceType.Wood, price.Wood)){
            return false;
        }
        if(!invent.HasResource(ResourceType.Stone, price.Stone)){
            return false;
        }
        if(!invent.HasResource(ResourceType.Metal, price.Metal)){
            return false;
        }
        if(!invent.HasResource(ResourceType.Food, price.Food)){
            return false;
        }
        if(!invent.HasResource(ResourceType.Money, price.Money)){
            return false;
        }

        invent.LoseResource(ResourceType.Wood, price.Wood);
        invent.LoseResource(ResourceType.Stone, price.Stone);
        invent.LoseResource(ResourceType.Metal, price.Metal);
        invent.LoseResource(ResourceType.Wood, price.Food);
        invent.LoseResource(ResourceType.Stone, price.Money);
        return true;
    }



    // Price set ups /////////////////////
    // Money, Wood, Stone, Metal, Food ///
    Price grabberCost = new Price(0, 25, 0, 15, 0);
    Price refinerCost = new Price(0, 20, 20, 0, 0);
    Price cropPlotCost = new Price(0, 25, 20, 0, 0);

    Price extentionCost = new Price(0, 50, 25, 20, 0);
    Price storageExtentionCost = new Price(0, 50, 25, 20, 0);
    Price livingExtentionCost = new Price(0, 50, 25, 20, 0);
    Price researchExtentionCost = new Price(0, 50, 25, 20, 0);

    Price workerCost = new Price(1, 0, 0, 0, 20);

}
