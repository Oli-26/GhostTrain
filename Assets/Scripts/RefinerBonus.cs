using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefinerBonus : TimeEffected
{
    Refiner _refiner;
    float _lifeTime;
    void Start()
    {
        _refiner = transform.parent.gameObject.GetComponent<Refiner>();
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
        _refiner.GrantBonus();
        gameObject.SetActive(false);
    }
}
