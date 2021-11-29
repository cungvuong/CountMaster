using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class List_Level
{
    public Level[] level;
    public int level_Current;

    public List_Level()
    {
        level = null;
        level_Current = 1;
    }

    public List_Level(Level[] levels, int level_curr)
    {
        level = levels;
        level_Current = level_curr;
    }
}
