using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainCore : TimeEffected
{
    Transform _transform;
    public GameObject trainFront;
    float speed = 3f;
    bool boostActive = false;
    float boostAmount = 1.5f;

    public List<GameObject> Extentions = new List<GameObject>();
    void Start()
    {
        _transform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        moveForward();
    }

    public void boost(){
        boostActive = true;
    }

    public void moveForward(){
        _transform.position += new Vector3(getActiveSpeed() * getTimePassed(), 0, 0);
        boostActive = false;
    }

    public void moveBackward(){
        _transform.position += new Vector3(-getActiveSpeed() * getTimePassed(), 0, 0);
    }

    private float getActiveSpeed(){
        return speed + (boostActive ? boostAmount : 0f);
    }
}
