using System.Collections;
using System.Collections.Generic;
using System.Text;
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

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        if (Money > 0)
        {
            sb.Append("Money: " + Money + "\n");
        }
        if (Wood > 0)
        {
            sb.Append("Wood: " + Wood + "\n");
        }
        if (Stone > 0)
        {
            sb.Append("Stone: " + Stone + "\n");
        }
        if (Metal > 0)
        {
            sb.Append("Metal: " + Metal + "\n");
        }
        if (Food > 0)
        {
            sb.Append("Food: " + Food + "\n");
        }

        return sb.ToString().TrimEnd('\n');


    }
}
