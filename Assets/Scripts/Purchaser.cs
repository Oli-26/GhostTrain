using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Purchaser : MonoBehaviour
{
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public bool AttemptPurchase(PurchaseType type){
        switch(type){
            case PurchaseType.Grabber:
                return AttemptGrabberPurchase();
            case PurchaseType.Refiner:
                return AttemptRefinerPurchase();
            case PurchaseType.Extension:
                return AttemptExtensionPurchase();
            case PurchaseType.StorageExtension:
                return AttemptStorageExtensionPurchase();
            case PurchaseType.LivingExtension:
                return AttemptLivingExtensionPurchase();
            case PurchaseType.ResearchExtension:
                return AttemptResearchExtensionPurchase();
            default:
                return false;
        }
    }

    public bool AttemptGrabberPurchase(){
        Inventory invent = GetComponent<Inventory>();

        if(!invent.HasResource(ResourceType.Wood, 25)){
            return false;
        }
        if(!invent.HasResource(ResourceType.Metal, 15)){
            return false;
        }

        invent.LoseResource(ResourceType.Wood, 25);
        invent.LoseResource(ResourceType.Metal, 15);
        return true;
    }

    public bool AttemptRefinerPurchase(){
        Inventory invent = GetComponent<Inventory>();

        if(!invent.HasResource(ResourceType.Wood, 20)){
            return false;
        }
        if(!invent.HasResource(ResourceType.Stone, 20)){
            return false;
        }

        invent.LoseResource(ResourceType.Wood, 20);
        invent.LoseResource(ResourceType.Stone, 20);
        return true;
    }

    public bool AttemptExtensionPurchase(){
        Inventory invent = GetComponent<Inventory>();

        if(!invent.HasResource(ResourceType.Wood, 50)){
            return false;
        }
        if(!invent.HasResource(ResourceType.Stone, 25)){
            return false;
        }
        if(!invent.HasResource(ResourceType.Metal, 20)){
            return false;
        }

        invent.LoseResource(ResourceType.Wood, 50);
        invent.LoseResource(ResourceType.Stone, 25);
        invent.LoseResource(ResourceType.Metal, 20);
        return true;
    }

    public bool AttemptStorageExtensionPurchase(){
        Inventory invent = GetComponent<Inventory>();

        if(!invent.HasResource(ResourceType.Wood, 50)){
            return false;
        }
        if(!invent.HasResource(ResourceType.Stone, 25)){
            return false;
        }
        if(!invent.HasResource(ResourceType.Metal, 20)){
            return false;
        }

        invent.LoseResource(ResourceType.Wood, 50);
        invent.LoseResource(ResourceType.Stone, 25);
        invent.LoseResource(ResourceType.Metal, 20);
        return true;
    }

    public bool AttemptLivingExtensionPurchase(){
        Inventory invent = GetComponent<Inventory>();

        if(!invent.HasResource(ResourceType.Wood, 50)){
            return false;
        }
        if(!invent.HasResource(ResourceType.Stone, 25)){
            return false;
        }
        if(!invent.HasResource(ResourceType.Metal, 20)){
            return false;
        }

        invent.LoseResource(ResourceType.Wood, 50);
        invent.LoseResource(ResourceType.Stone, 25);
        invent.LoseResource(ResourceType.Metal, 20);
        return true;
    }

    public bool AttemptResearchExtensionPurchase(){
        Inventory invent = GetComponent<Inventory>();

        if(!invent.HasResource(ResourceType.Wood, 50)){
            return false;
        }
        if(!invent.HasResource(ResourceType.Stone, 25)){
            return false;
        }
        if(!invent.HasResource(ResourceType.Metal, 20)){
            return false;
        }

        invent.LoseResource(ResourceType.Wood, 50);
        invent.LoseResource(ResourceType.Stone, 25);
        invent.LoseResource(ResourceType.Metal, 20);
        return true;
    }
}
