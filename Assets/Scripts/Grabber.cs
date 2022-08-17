﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Grabber : TimeEffected{
    public GameObject hand;
    public GameObject arm;
    public Transform touchPoint;
    public Transform basePoint;

    public float cooldown = 8f;
    float _activeCooldown;

    public float maxRange = 1.5f;
    public bool isTopSide = true;
    
    public Vector3 targetPoint;
    public GameObject target;
    bool isGrabbing = false;
    Transform _transform;

    void Start()
    {
        _transform = transform;
    }

    void Update()
    {
        if(isGrabbing){
            if(target == null){
                disableGrabbing();
                return;
            }

            float yDiff = targetPoint.y - touchPoint.position.y;
            float xDiff = targetPoint.x - touchPoint.position.x;
            if(xDiff < 0){
                disableGrabbing();
                return;
            }

            if(Mathf.Abs(yDiff) > 0.1f){
                    float extendBy = getTimePassed();
                    arm.transform.localScale = arm.transform.localScale + new Vector3(0f, extendBy/4f, 0f);
                    if(isTopSide){
                        arm.transform.position += new Vector3(0f, extendBy/2f, 0f);
                        hand.transform.position += new Vector3(0f, extendBy, 0f);
                    }else{
                        arm.transform.position -= new Vector3(0f, extendBy/2f, 0f);
                        hand.transform.position -= new Vector3(0f, extendBy, 0f);
                    }
            }else if(Mathf.Abs(xDiff) < 0.2f){
                Destroy(target);
                disableGrabbing();
                return;
            }
        }else{
           if(Mathf.Abs(touchPoint.position.y - basePoint.position.y) > 0.55f){
                float extendBy = getTimePassed();
                arm.transform.localScale = arm.transform.localScale - new Vector3(0f, extendBy/4f, 0f);
                if(isTopSide){
                    arm.transform.position -= new Vector3(0f, extendBy/2f, 0f);
                    hand.transform.position -= new Vector3(0f, extendBy, 0f);
                }else{
                    arm.transform.position += new Vector3(0f, extendBy/2f, 0f);
                    hand.transform.position += new Vector3(0f, extendBy, 0f);
                }
            }else{
                _activeCooldown -= getTimePassed();
            } 

            if(_activeCooldown <= 0){
                _activeCooldown = cooldown;
                target = FindObjectToGrab();

                if(target != null){
                    targetPoint = target.transform.position;
                    enableGrabbing();
                }else{
                    _activeCooldown = 0.5f;
                }
            }
        }
        
    }

    void disableGrabbing(){
        hand.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        isGrabbing = false;
    }

    void enableGrabbing(){
        hand.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 0f, 1f);
        isGrabbing = true;
    }

    public GameObject FindObjectToGrab(){
        List<GameObject> resources = GameObject.FindGameObjectsWithTag("Resource").Where(resource => resource.transform.position.x > basePoint.position.x).ToList();
        List<GameObject> reachableResources;
        if(isTopSide){
            reachableResources = resources.Where(
                resource => 
                    resource.transform.position.y - basePoint.position.y > 0 
                    && resource.transform.position.y - basePoint.position.y < maxRange)
                    .ToList();
        }else{
            reachableResources = resources.Where(
                resource => 
                    resource.transform.position.y - basePoint.position.y < 0 
                    && basePoint.position.y - resource.transform.position.y < maxRange)
                    .ToList();
        }
        List<GameObject> orderedReachableList = reachableResources.OrderBy(resource => resource.transform.position.x).ToList();

        if(orderedReachableList.Count >= 1){
            return orderedReachableList[0];
        }else{
            return null;
        }
        
    }
}
