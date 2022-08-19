using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Grabber : TimeEffected{
    GameObject Controller;
    Environment environmentController;
    Inventory invent;
    public GameObject hand;
    public GameObject arm;
    public Vector3 armSize;
    public Transform touchPoint;
    public Transform basePoint;

    public float cooldown = 8f;
    float _activeCooldown;

    public float maxRange = 1.5f;
    public bool isTopSide = true;
    
    public Vector3 targetPoint;
    public GameObject target;
    bool isGrabbing = false;
    bool isReturning = false;

    void Start()
    {
        Controller = GameObject.Find("Controller");
        environmentController = Controller.GetComponent<Environment>();
        invent = Controller.GetComponent<Inventory>();
        armSize = arm.GetComponent<SpriteRenderer>().bounds.size;
    }

    void Update()
    {
        if(isGrabbing){
            if(target == null){
                disableGrabbing();
                return;
            }

            
            float xDiff = targetPoint.x - touchPoint.position.x;

            if(xDiff < 0){
                disableGrabbing();
                return;
            }

            float yDiff = targetPoint.y - touchPoint.position.y;

            if(Mathf.Abs(yDiff) > 0.1f){
                    MoveWithRespectToTarget();
            }else if(Mathf.Abs(xDiff) < 0.2f){
                consumeResource();
                disableGrabbing();
                return;
            }
        }else{
           if(isReturning){
                if(Mathf.Abs(touchPoint.position.y - basePoint.position.y) > 0.1f){
                    MoveWithRespectToTarget();
                }else{
                    isReturning = false;
                }
            }else{
                _activeCooldown -= getTimePassed();
            } 

            if(_activeCooldown <= 0){
                _activeCooldown = cooldown;
                FindObjectToGrab();
            }
        }
        
    }

    void MoveWithRespectToTarget(){
        int signMultiplier = isGrabbing ? 1 : -1;
        int orientationMultiplier = isTopSide ? 1 : -1;

        Vector3 movementVector = new Vector3(0f, getTimePassed()*signMultiplier*orientationMultiplier, 0f);

        arm.transform.localScale -= movementVector/armSize.y;
        arm.transform.position -= movementVector/2f;
        hand.transform.position -= movementVector;
        
    }

    void disableGrabbing(){
        hand.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        isGrabbing = false;
    }

    void enableGrabbing(){
        hand.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 0f, 1f);
        isGrabbing = true;
    }

    void consumeResource(){
        environmentController.DeTargetResource(target);
        Resource r = target.GetComponent<Resource>();
        invent.GainResource(r.type, r.amount);
        Destroy(target);
    }

    void FindObjectToGrab(){
        List<GameObject> resources = GameObject.FindGameObjectsWithTag("Resource").Where(resource => IsTargetable(resource)).OrderBy(resource => resource.transform.position.x).ToList();

        if(resources.Count >= 1){
            
            target = resources[0];
            
            targetPoint = target.transform.position;
            environmentController.TargetResource(target);
            enableGrabbing();
        }else{
            _activeCooldown = 0.5f;
        }
        
    }

    bool IsTargetable(GameObject resource){
        return resource.transform.position.x> basePoint.position.x + 4f 
            && !environmentController.IsResourceTargeted(resource)
            && isTopSide ? resource.transform.position.y - basePoint.position.y > 0 : resource.transform.position.y - basePoint.position.y < 0
            && isTopSide ? resource.transform.position.y - basePoint.position.y < maxRange : basePoint.position.y - resource.transform.position.y < maxRange;                               
    }
}
