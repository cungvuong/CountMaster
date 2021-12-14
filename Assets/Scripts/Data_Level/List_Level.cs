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
    public int mount_Gold;
    public int earn_Gold_Level;
    
    public List_Level()
    {
        level = null;
        level_Current = 1;
        mount_Player = 1;
        mount_Gold = 0;
    }

    public List_Level(int level_curr, int mount_Pl, int index_curr)
    {
        Level[] levels = new Level[1];
        level = levels;
        level_Current = level_curr;
        mount_Player = mount_Pl;
        index_Curr = index_curr;
    }

    public void Get_Gold(int gold)
    {
        mount_Gold += gold;
    }
    public List_Level Next_Level()
    {
        level_Current++;
        return this;
    }
}
