using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropPlotBonus : MonoBehaviour
{
    CropPlot _cropPlot;
    void Start()
    {
        _cropPlot = transform.parent.gameObject.GetComponent<CropPlot>();
    }

    void Update()
    {
    }

    public void SetUpBonus(){
        gameObject.SetActive(true);
    }

    private void OnMouseUpAsButton()
    {
        _cropPlot.GrantBonus();
        gameObject.SetActive(false);
    }
}
