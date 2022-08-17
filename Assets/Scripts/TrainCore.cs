using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainCore : MonoBehaviour
{
    Transform _transform;
    float speed = 3f;
    bool boostActive = false;
    float boostAmount = 1.5f;

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
        _transform.position += new Vector3(getActiveSpeed() * Time.deltaTime, 0, 0);
        boostActive = false;
    }

    public void moveBackward(){
        _transform.position += new Vector3(-getActiveSpeed() * Time.deltaTime, 0, 0);
    }

    private float getActiveSpeed(){
        return speed + (boostActive ? boostAmount : 0f);
    }
}
