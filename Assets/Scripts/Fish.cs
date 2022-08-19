using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : TimeEffected
{
    Vector3 target;
    float redirectTime = 0.1f;

    void Start()
    {
        target = transform.position;
    }

    void Update()
    {
        float timePassed = getTimePassed();
        transform.position = Vector3.MoveTowards(transform.position, target, timePassed);
        redirectTime -= timePassed;
        if(redirectTime <= 0){
            ChooseRandomPoint();
            redirectTime = 5f;
        }
    }

    void ChooseRandomPoint(){
        target = transform.position + new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0f);

        Vector3 vectorToTarget = target - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, getTimePassed());
    }
}
