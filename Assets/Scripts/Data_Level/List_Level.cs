using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class List_Level
{
    public Level[] level;
    public int level_Current; 
    public int mount_Player; // so luong player hien tai
    public int index_Curr; // player dang duoc chon

    public List_Level()
    {
        level = null;
        level_Current = 1;
    }

    public List_Level(int level_curr, int mount_Pl, int index_curr)
    {
        Level[] levels = new Level[1];
        level = levels;
        level_Current = level_curr;
        mount_Player = mount_Pl;
        index_Curr = index_curr;
    }
}
