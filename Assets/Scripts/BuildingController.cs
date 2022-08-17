using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    bool buildUIActive = false;
    public List<GameObject> allBuildUIObjects = new List<GameObject>();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toggleBuildingUI(){
        buildUIActive = !buildUIActive;
        
        foreach(GameObject uiElemeent in allBuildUIObjects){
            uiElemeent.SetActive(buildUIActive);
        }
    }
}
