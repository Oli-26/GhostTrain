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
                return HasResourceForPurchase(extensionCost);
            case PurchaseType.StorageExtension:
                return HasResourceForPurchase(storageExtensionCost);
            case PurchaseType.LivingExtension:
                return HasResourceForPurchase(livingExtensionCost);
            case PurchaseType.ResearchExtension:
                return HasResourceForPurchase(researchExtensionCost);
            case PurchaseType.CropPlot:
                return HasResourceForPurchase(cropPlotCost);
            case PurchaseType.Worker:
                return HasResourceForPurchase(workerCost);
            default:
                return false;
        }
    }


    public bool HasResourceForPurchase(Price price, bool performPurchase = true){
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

        if(performPurchase){
            invent.LoseResource(ResourceType.Wood, price.Wood);
            invent.LoseResource(ResourceType.Stone, price.Stone);
            invent.LoseResource(ResourceType.Metal, price.Metal);
            invent.LoseResource(ResourceType.Wood, price.Food);
            invent.LoseResource(ResourceType.Stone, price.Money);
        }

        return true;
    }



    // Price set ups /////////////////////
    // Money, Wood, Stone, Metal, Food ///
    public Price grabberCost = new Price(0, 25, 0, 15, 0);
    public Price refinerCost = new Price(0, 20, 20, 0, 0);
    public Price cropPlotCost = new Price(0, 25, 20, 0, 0);

    public Price extensionCost = new Price(0, 50, 25, 20, 0);
    public Price storageExtensionCost = new Price(0, 50, 25, 20, 0);
    public Price livingExtensionCost = new Price(0, 50, 25, 20, 0);
    public Price researchExtensionCost = new Price(0, 50, 25, 20, 0);

    public Price workerCost = new Price(1, 0, 0, 0, 20);

}
