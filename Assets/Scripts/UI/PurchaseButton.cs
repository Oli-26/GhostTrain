using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class PurchaseButton : UIElement
{
    public PurchaseType Type;
    public KeyCode Shortcut { get; set; } = KeyCode.None;

    GameObject gameController;
    Purchaser _purchaser;
    TrainCore _trainCore;

    void Start()
    {
        gameController = GameObject.Find("Controller");
        _purchaser = gameController.GetComponent<Purchaser>();
        _trainCore = GameObject.Find("Train").GetComponent<TrainCore>();
    }

    private void OnGUI()
    {
        Event e = Event.current;
        if (e.isKey && e.keyCode!= KeyCode.None && Shortcut == e.keyCode)
        {
            Debug.Log("KeyDown:" + e.keyCode + " Shortcut:" + Shortcut);
            Interact();
        }
    }

    void Update()
    {
    }

    private void OnMouseUpAsButton()
    {
        Interact();
    }

    public void Interact()
    {
        Debug.Log("Called:" + Type);
        UIController UI = gameController.GetComponent<UIController>();
        if (Type == PurchaseType.Extension || Type == PurchaseType.StorageExtension ||
            Type == PurchaseType.LivingExtension || Type == PurchaseType.ResearchExtension)
        {
            if (_purchaser.AttemptPurchase(Type))
            {
                _trainCore.AddExtension(Type);
            }

            return;
        }

        if (Type == PurchaseType.Worker)
        {
            if (!gameController.GetComponent<BuildingController>().CheckNewWorkerPossible(UI.selectedExtentionId))
            {
                return;
            }

            if (gameController.GetComponent<Purchaser>().AttemptPurchase(Type))
            {
                gameController.GetComponent<BuildingController>().CreateWorker(UI.selectedExtentionId);
                gameController.GetComponent<UIController>().LoadCorrectGUI();
            }
        }

        if (UI.selectedSlotId != -1 && UI.selectedExtentionId != -1)
        {
            if (!gameController.GetComponent<BuildingController>()
                .CheckBuildIsPossible(UI.selectedExtentionId, UI.selectedSlotId))
            {
                return;
            }

            if (gameController.GetComponent<Purchaser>().AttemptPurchase(Type))
            {
                gameController.GetComponent<BuildingController>()
                    .ConstructAddOn(Type, UI.selectedExtentionId, UI.selectedSlotId);
                gameController.GetComponent<UIController>().LoadCorrectGUI();
            }
        }
    }

    public void Select()
    {
    }

    public void DeSelect()
    {
    }
}