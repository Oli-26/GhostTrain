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
    public GameObject spinner;
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
    bool hasGrabbedResource = false;

    ResourceType focus;
    bool focusSet = false;

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

            if(Mathf.Abs(differences.y) > 0.07f){
                    MoveWithRespectToTarget();
            }else if(Mathf.Abs(differences.x) < 0.07f){
                grabResource();
                disableGrabbing();
                return;
            }
        }else{
           if(isReturning){
                if(Mathf.Abs(touchPoint.position.y - basePoint.position.y) > 0.07f){
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

    void Spin(int direction){
        spinner.transform.Rotate (direction * Vector3.forward * -90 * getTimePassed());
    }

    void MoveWithRespectToTarget(){
        int signMultiplier = isGrabbing ? 1 : -1;
        int orientationMultiplier = isTopSide ? 1 : -1;

        Spin(signMultiplier);

        Vector3 movementVector = new Vector3(0f, getTimePassed()*signMultiplier*orientationMultiplier, 0f);

        arm.transform.localScale += orientationMultiplier*movementVector/armSize.y;
        arm.transform.position += movementVector/2f;
        hand.transform.position += movementVector;
        
    }

    void disableGrabbing(){
        isGrabbing = false;
        isReturning = true;
    }

    void enableGrabbing(){
        isGrabbing = true;
        isReturning = false;
    }

    void consumeResource(){
        environmentController.DeTargetResource(target);

        if(hasGrabbedResource == false){
            return;
        }
        
        Resource r = target.GetComponent<Resource>();
        invent.GainResource(r.type, r.amount);
        Destroy(target);
    }

    void grabResource(){
        hasGrabbedResource = true;
        target.transform.parent = touchPoint;
    }

    void FindObjectToGrab(){
        List<GameObject> resources = GameObject.FindGameObjectsWithTag("Resource").Where(resource => IsTargetable(resource)).ToList();
        List<GameObject> orderedResources = resources.OrderBy(resource => resource.transform.position.x).ToList();

        if(orderedResources.Count >= 1){
            target = orderedResources[0];
            
            Vector3 size = target.GetComponent<SpriteRenderer>().bounds.size;
            int orientationMultiplier = isTopSide ? 1 : -1;

            targetPoint = target.transform.position ;//+ new Vector3(0f, size.y/2f, 0f)*orientationMultiplier;
            environmentController.TargetResource(target);
            enableGrabbing();
            hasGrabbedResource = false;
        }else{
            _activeCooldown = 0.5f;
        }
        
    }

    bool IsTargetable(GameObject resource){
        if(resource.transform.position.x < basePoint.position.x + 4f){
            return false;
        } 
        if(environmentController.IsResourceTargeted(resource)){
            return false;
        }
            
            if(isTopSide){
                if(resource.transform.position.y - basePoint.position.y < 0){
                    return false;
                }
                
                if(resource.transform.position.y - basePoint.position.y > maxRange){
                    return false;
                };  
            }else{
                if(resource.transform.position.y - basePoint.position.y > 0){
                    return false;
                };
                if(basePoint.position.y - resource.transform.position.y > maxRange){
                    return false;
                }; 
            }

            if(focusSet && focus != null){
                if(resource.GetComponent<Resource>().type == focus){
                    return true;
                }
            }else{
                return true;
            }
       
            return false;                         
    }

    public void FocusResource(ResourceType type){
        focus = type;
        SetFocusActive(true);
    }

    public ResourceType GetFocus(){
        return focus;
    }

    public bool GetFocusActive(){
        return focusSet;
    }

    public void SetFocusActive(bool active){
        focusSet = active;
    }
}
