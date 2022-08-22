using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAction : MonoBehaviour
{
    public NPCActionType type;
    UIController uiController;

    private void OnMouseUpAsButton() {
        if(!uiController.buildUIActive){
            PerformAction();
        }
    }

    void Start()
    {
        uiController = FindObjectOfType<UIController>();
    }

    void Update()
    {
        
    }

    void PerformAction(){
        switch(type){
            case NPCActionType.Cancel:
                transform.parent.gameObject.SetActive(false);
                break;
            case NPCActionType.Loot:
            default:
                transform.parent.parent.gameObject.GetComponent<NPC>().SetAction(type);
                break;
        }
    }

    
}

public enum NPCActionType {Loot, Cancel};
