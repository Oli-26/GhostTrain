using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    GameObject addOn;
    public int id;

    void Start()
    {
        
    }

    // Update is called once per frame
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
