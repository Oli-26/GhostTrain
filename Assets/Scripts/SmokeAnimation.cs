using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeAnimation : MonoBehaviour
{
    public List<Sprite> spriteList;
    int frameDuration = 10;
    int _frameTick = 0;
    int spriteIndex = 0;

    SpriteRenderer renderer;

    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        if(_frameTick == frameDuration){
            spriteIndex++;
            if(spriteIndex == spriteList.Count){
                spriteIndex = 0;
            }
            renderer.sprite = spriteList[spriteIndex];
            renderer.color = new Color(1f, 1f, 1f, 1f - spriteIndex * 0.07f);
            _frameTick = 0;
        }else{
            _frameTick++;
        }
    }
}
