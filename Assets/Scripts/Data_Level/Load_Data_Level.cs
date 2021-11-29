using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Load_Data_Level : MonoBehaviour
{
    public static Load_Data_Level instance;
    public Text text_lv;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }   
        list = Save_Data.Load();
        text_lv.text = "Level " + list.level_Current.ToString();
    }

    public List_Level list = new List_Level();

}
