using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extention : MonoBehaviour
{
    public List<GameObject> slots = new List<GameObject>();
    public GameObject baseObject;
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
}
