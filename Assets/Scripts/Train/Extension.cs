using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extension : MonoBehaviour
{
    public List<GameObject> slots = new List<GameObject>();
    public List<GameObject> interactableUISlots = new List<GameObject>();
    public List<GameObject> otherInteractables = new List<GameObject>();
    public GameObject baseObject;
    public List<GameObject> NPCS;
    public GameObject NPCPrefab;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public GameObject GetSlot(int index){
        return slots[index];
    }

    public GameObject GetAddOn(int index){
        return slots[index].GetComponent<Slot>().GetAddOn();
    }

    public void CreateAddon(int index, GameObject addon){
        slots[index].GetComponent<Slot>().CreateAddOn(addon);
    }

    public void SetSlotExtensionId(int id){
        foreach(GameObject UIElement in interactableUISlots){
            UIElement.GetComponent<SelectableTrainPart>().extentionId = id;
        }
        foreach(GameObject UIElement in otherInteractables){
            UIElement.GetComponent<SelectableTrainPart>().extentionId = id;
        }

    }

    public void AddNPC(){
        GameObject npc = Instantiate(NPCPrefab, baseObject.transform.position, Quaternion.identity);
        NPCS.Add(npc);
        npc.transform.parent = baseObject.transform;
        npc.GetComponent<NPC>().SetParentExtension(this);
    }
}

public enum ExtensionType {Base, Storage, Living, Research}
