using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : TimeEffected
{
    Extension extension;
    Vector3 relativeTargetPoint;
    float _newDirectionTime = 5f;
    float speed = 1f;
    public List<Sprite> allBodySprites;

    void Start()
    {
        extension = transform.parent.gameObject.GetComponent<Extension>();
        WalkToNewTrainLocation();
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = allBodySprites[Random.Range(0, allBodySprites.Count-1)];
    }

    void Update()
    {
        if(_newDirectionTime <= 0){
            WalkToNewTrainLocation();
            _newDirectionTime = Random.Range(3f, 6f);
        }else{
            _newDirectionTime -= getTimePassed();
        }

        transform.position = Vector3.MoveTowards(transform.position, extension.baseObject.transform.position + relativeTargetPoint, speed * getTimePassed());
    }

    void WalkToNewTrainLocation(){
        Vector3 bounds = extension.baseObject.gameObject.GetComponent<SpriteRenderer>().bounds.size;
        relativeTargetPoint = new Vector3(bounds.x * Random.Range(-0.2f, 0.2f), bounds.y * Random.Range(-0.2f, 0.2f), 0f);
    } 
}
