﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrainCore : TimeEffected
{
    private const int MaxExtensions = 7;
    private const float Speed = 3f;
    private const float BoostAmount = 1.5f;
    
    private bool _boostActive;

    public GameObject extensionPrefab;
    public GameObject ghostExtensionPrefab;
    public GameObject storageExtensionPrefab;
    public GameObject livingExtensionPrefab;
    public GameObject researchExtensionPrefab;

    Transform _transform;
    public GameObject trainFront;
    private Extension _ghostExtension;

    private Vector3 walkAreaBottomLeft;
    private Vector3 walkAreaTopRight;

    public List<Extension> Extensions { get; } = new List<Extension>();

    void Start()
    {
        _transform = transform;
        walkAreaTopRight = new Vector3(0.6f, 0.15f, 0f);
        walkAreaBottomLeft = new Vector3(-0.6f, -0.15f, 0f);
    }

    void Update()
    {
        moveForward();
    }

    public void boost()
    {
        _boostActive = true;
    }

    public void moveForward()
    {
        _transform.position += new Vector3(getActiveSpeed() * getTimePassed(), 0, 0);
        _boostActive = false;
    }

    public void moveBackward()
    {
        _transform.position += new Vector3(-getActiveSpeed() * getTimePassed(), 0, 0);
    }

    private float getActiveSpeed()
    {
        return Speed + (_boostActive ? BoostAmount : 0f);
    }

    public void AddExtension(PurchaseType extensionType)
    {
        if (CanAddExtension())
        {
            var extension = CreateExtension(GetExtensionPrefab(extensionType));

            Extensions.Add(extension);
            extension.SetSlotExtensionId(Extensions.Count);
            
            extension.interactableUISlots
                .ForEach(slot => slot.SetActive(true));

            extension.otherInteractables
                .ForEach(interactable => interactable.SetActive(true));
            
            if (CanAddExtension()) ShowGhostExtension(true);
        }
    }

    public void ShowGhostExtension(bool show)
    {
        if (_ghostExtension != null)
        {
            Destroy(_ghostExtension.gameObject);
        }

        if (show && CanAddExtension())
        {
            _ghostExtension = CreateExtension(ghostExtensionPrefab);
        }
    }

    private Extension CreateExtension(GameObject prefab)
    {
        Extension extension = Instantiate(prefab, trainFront.transform.position, Quaternion.identity)
            .GetComponent<Extension>();

        walkAreaBottomLeft -= new Vector3(extension.GetComponent<Extension>().baseObject.GetComponent<SpriteRenderer>().bounds.size.x/2f, 0f, 0f);

        Vector3 extensionPosition = new Vector3(0f, 0f, 0f);
        foreach(Extension ext in Extensions){
            extensionPosition += new Vector3(ext.GetComponent<Extension>().baseObject.GetComponent<SpriteRenderer>().bounds.size.x, 0f, 0f);
        }


        extension.gameObject.transform.position -= (extensionPosition + new Vector3(1.88f, 0f, 0f));
        extension.gameObject.transform.parent = gameObject.transform;
        
        return extension;
    }
    
    private bool CanAddExtension()
    {
        return Extensions.Count < MaxExtensions;
    }

    public GameObject GetExtensionPrefab(PurchaseType type){
        switch(type){
            case PurchaseType.Extension:
                return extensionPrefab;
            case PurchaseType.StorageExtension:
                return storageExtensionPrefab;
            case PurchaseType.LivingExtension:
                return livingExtensionPrefab;
            case PurchaseType.ResearchExtension:
                return researchExtensionPrefab;
            default:
                return null;
        }
    }

    public Vector3 GetTopPoint(){
        return walkAreaTopRight;
    }

    public Vector3 GetBottomPoint(){
        return walkAreaBottomLeft;
    }

    public Vector3 GetWorldTopPoint(){
        return _transform.position + walkAreaTopRight;
    }

}