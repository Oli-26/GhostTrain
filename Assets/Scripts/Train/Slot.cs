using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public GameObject addOn;
    public int id;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void CreateAddOn(GameObject addon){
        addOn = addon;
    }

    public GameObject GetAddOn(){
        return addOn;
    }
}
