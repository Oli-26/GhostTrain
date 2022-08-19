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

            Vector3 differences = targetPoint - touchPoint.transform.position;

            if(differences.x < 0){
                disableGrabbing();
                return;
            }

            if(Mathf.Abs(differences.y) > 0.1f){
                    MoveWithRespectToTarget();
            }else if(Mathf.Abs(differences.x) < 0.2f){
                grabResource();
                disableGrabbing();
                return;
            }
        }else{
           if(isReturning){
                if(Mathf.Abs(touchPoint.position.y - basePoint.position.y) > 0.1f){
                    MoveWithRespectToTarget();
                }else{
                    consumeResource();
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

        arm.transform.localScale += movementVector/armSize.y;
        arm.transform.position += movementVector/2f;
        hand.transform.position += movementVector;
        
    }

    void disableGrabbing(){
        hand.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        isGrabbing = false;
        isReturning = true;
    }

    void enableGrabbing(){
        hand.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 0f, 1f);
        isGrabbing = true;
        isReturning = false;
    }

    void consumeResource(){
        environmentController.DeTargetResource(target);
        Resource r = target.GetComponent<Resource>();
        invent.GainResource(r.type, r.amount);
        Destroy(target);
    }

    void grabResource(){
        target.transform.parent = touchPoint;
    }

    void FindObjectToGrab(){
        List<GameObject> resources = GameObject.FindGameObjectsWithTag("Resource").Where(resource => IsTargetable(resource)).ToList();
        List<GameObject> orderedResources = resources.OrderBy(resource => resource.transform.position.x).ToList();

        if(orderedResources.Count >= 1){
            Debug.Log("Targeting resource");
            target = orderedResources[0];

            Debug.Log("Target " + target.transform.position);
            Debug.Log("Grabber " + basePoint.position);
            
            targetPoint = target.transform.position;
            environmentController.TargetResource(target);
            enableGrabbing();
        }else{
            _activeCooldown = 0.5f;
        }
        
    }

    bool IsTargetable(GameObject resource){
        bool result = (resource.transform.position.x > basePoint.position.x + 4f) 
            && (!environmentController.IsResourceTargeted(resource));
            
            if(isTopSide){
                result &= resource.transform.position.y - basePoint.position.y > 0;
                result &= resource.transform.position.y - basePoint.position.y < maxRange;  
            }else{
                result &= resource.transform.position.y - basePoint.position.y < 0;
                result &= basePoint.position.y - resource.transform.position.y < maxRange; 
            }
       
            return result;                           
    }
}
