﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrainCore : TimeEffected
{
    private const int MaxExtensions = 5;
    private const float Speed = 3f;
    private const float BoostAmount = 1.5f;
    
    private bool _boostActive;

    public GameObject extensionPrefab;
    public GameObject ghostExtensionPrefab;

    Transform _transform;
    public GameObject trainFront;
    private Extention _ghostExtension;


    public List<Extention> Extentions { get; } = new List<Extention>();

    void Start()
    {
        _transform = transform;
    }

    // Update is called once per frame
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

    public void AddExtension()
    {
        if (CanAddExtension())
        {
            var extension = CreateExtension(extensionPrefab);

            Extentions.Add(extension);
            extension.SetSlotExtensionId(Extentions.Count);
            
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

    private Extention CreateExtension(GameObject prefab)
    {
        Extention extension = Instantiate(prefab, trainFront.transform.position, Quaternion.identity)
            .GetComponent<Extention>();
        extension.transform.position -=
            new Vector3(
                (extension.GetComponent<Extention>().baseObject.GetComponent<SpriteRenderer>().bounds.size.x - 0.2f) *
                Extentions.Count, 0f, 0f);
        extension.transform.position -= new Vector3(1.88f, 0.82f, 0f);
        extension.transform.parent = gameObject.transform;
        
        return extension;
    }
    
    private bool CanAddExtension()
    {
        return Extentions.Count < MaxExtensions;
    }

}