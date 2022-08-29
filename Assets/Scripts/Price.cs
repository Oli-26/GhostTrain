using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Price
{
    public int Money;
    public int Wood;
    public int Stone;
    public int Metal;
    public int Food;
    
    public Price(int money, int wood, int stone, int metal, int food){
        Money = money;
        Wood = wood;
        Stone = stone;
        Metal = metal;
        Food = food;
    }
}
