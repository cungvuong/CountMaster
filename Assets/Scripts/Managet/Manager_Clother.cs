using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager_Clother : MonoBehaviour
{
    public Button[] list_Item;

    void Start()
    {
        list_Item = GetComponentsInChildren<Button>();
        foreach(Button x in list_Item)
        {
            //Debug.Log(x.gameObject.name);
        }
    }

    public void Get_Clother(GameObject x)
    {
        int num_Game_Object_Selected=0;
        for(int i=0; i<list_Item.Length; i++)
        {
            if (list_Item[i].gameObject == x)
            {
                // xu ly khi click vao item vua chon
                num_Game_Object_Selected = i;
            }
        }
        Player.instance.Load_Player_Start(num_Game_Object_Selected);
    }
}
