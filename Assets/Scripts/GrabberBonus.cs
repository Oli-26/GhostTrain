using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabberBonus : TimeEffected
{
    Grabber _grabber;
    float _lifeTime;
    void Start()
    {
        _grabber = transform.parent.gameObject.GetComponent<Grabber>();
    }

    void Update()
    {
        _lifeTime -= getTimePassed();
        if(_lifeTime <= 0){
            gameObject.SetActive(false);
        }
    }

    public void SetUpBonus(float lifeTime){
        gameObject.SetActive(true);
        _lifeTime = lifeTime;
    }

    private void OnMouseUpAsButton()
    {
        _grabber.GrantBonus();
        gameObject.SetActive(false);
    }
}
