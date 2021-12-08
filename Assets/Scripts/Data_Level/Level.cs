using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Level
{
    public int level;
    public int health_Boss;
    
    Level(int x)
    {
        level = x;
        health_Boss = 50;
    }

    void Minus(int health)
    {
        health_Boss -= health;
    }
}
