using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class NPC : TimeEffected
{
    Extension extension;
    Vector3 relativeTargetPoint;
    float _newDirectionTime = 5f;
    float speed = 2f;
    public List<Sprite> allBodySprites;
    Transform _transform;
    Transform _extensionBaseTransform;
    public GameObject actionMenu;
    UIController uiController;

    NPCActionType actionBeingPerformed;
    bool actionIsBeingPerformed = false;
    int actionPhase = 0;

    GameObject targetLoot;
    Transform targetLootTransform;

    
    private void OnMouseUpAsButton() {
        if(!uiController.buildUIActive){
            actionMenu.SetActive(true);
        }
    }

    void Start()
    {
        _transform = transform;
        uiController = FindObjectOfType<UIController>();
        extension = transform.parent.gameObject.GetComponent<Extension>();
        _extensionBaseTransform = extension.baseObject.transform;
        WalkToNewTrainLocation();
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = allBodySprites[Random.Range(0, allBodySprites.Count-1)];
    }

    void Update()
    {
        if(actionIsBeingPerformed){
            switch(actionBeingPerformed){
                case NPCActionType.Loot:
                    Loot();
                    break;
                default:
                    break;

            }
        }else{
            WanderAroundTrain();
        }
    }

    void WanderAroundTrain(){
        if(_newDirectionTime <= 0){
            WalkToNewTrainLocation();
            _newDirectionTime = Random.Range(3f, 6f);
        }else{
            _newDirectionTime -= getTimePassed();
        }

        _transform.position = Vector3.MoveTowards(_transform.position, _extensionBaseTransform.position + relativeTargetPoint, speed * getTimePassed());
    }

    void Loot(){
        if(actionPhase == 0){
            LocateLoot();
            return;
        }
        if(actionPhase == 1){
            MoveTowardLoot();
            return;
        }
        if(actionPhase == 2){
            OpenLoot();
            return;
        }
        if(actionPhase == 3){
            ReturnFromLoot();
            return;
        }
    }

    void LocateLoot(){
        List<GameObject> possibleLoot = GameObject.FindGameObjectsWithTag("Loot").Where(loot => Vector3.Distance(_transform.position, loot.transform.position) < 14f).ToList();
        List<GameObject> orderedLoot = possibleLoot.OrderBy(loot => loot.transform.position.x).ToList();

        if(orderedLoot.Count == 0){
            actionIsBeingPerformed = false;
            Debug.Log("loot not found");
        }else{
            targetLoot = orderedLoot[0];
            targetLootTransform = targetLoot.transform;
            actionPhase = 1;
            _transform.parent = null;
            Debug.Log("Loot found, continuing");
        }
    }

    void MoveTowardLoot(){
        if(Vector3.Distance(_transform.position, targetLootTransform.position) <= 0.2f){
            actionPhase = 2;
        }else{
            _transform.position = Vector3.MoveTowards(_transform.position, targetLootTransform.position, 2f * speed * getTimePassed());
        }
        
    }

    void OpenLoot(){
        Destroy(targetLoot);
        actionPhase = 3;
    }

    void ReturnFromLoot(){
        _transform.parent = extension.gameObject.transform;
        WalkToNewTrainLocation();
        actionIsBeingPerformed = false;
    }

    void WalkToNewTrainLocation(){
        Vector3 bounds = extension.baseObject.gameObject.GetComponent<SpriteRenderer>().bounds.size;
        relativeTargetPoint = new Vector3(bounds.x * Random.Range(-0.2f, 0.2f), bounds.y * Random.Range(-0.2f, 0.2f), 0f);
    } 

    public void SetAction(NPCActionType actionType){
        Debug.Log("Action set:" + actionType);
        actionBeingPerformed = actionType;
        actionIsBeingPerformed = true;
        actionPhase = 0;
        actionMenu.SetActive(false);
    }
}
