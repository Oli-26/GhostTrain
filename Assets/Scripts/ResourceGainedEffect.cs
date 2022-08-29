using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceGainedEffect : MonoBehaviour
{
    Transform _transform;
    Vector3 movementVector;

    public Sprite woodSprite;
    public Sprite stoneSprite;
    public Sprite metalSprite;
    public Sprite foodSprite;
    public Sprite moneySprite;
    public TextMesh amount;

    void Start()
    {
        movementVector = new Vector3(0f, 0.01f, 0f);
        Destroy(gameObject, 1f);
        _transform = transform;
    }

    void Update()
    {
        _transform.position += movementVector;
    }

    public void SetResource(ResourceType type){
        SpriteRenderer renderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        switch(type){
            case ResourceType.Wood:
                renderer.sprite = woodSprite;
                break;
            case ResourceType.Stone:
                renderer.sprite = stoneSprite;
                break;
            case ResourceType.Metal:
                renderer.sprite = metalSprite;
                break;
            case ResourceType.Food:
                renderer.sprite = foodSprite;
                break;
            case ResourceType.Money:
                renderer.sprite = moneySprite;
                break;

        }
    }

    public void SetAmount(int amount)
    {
        this.amount.text = "+" + amount;
        this.amount.gameObject.GetComponent<MeshRenderer>().sortingOrder = 15;
    }
}
