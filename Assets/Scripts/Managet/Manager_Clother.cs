using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager_Clother : MonoBehaviour
{
    public Button[] list_Item;

    void Start()
    {
        Get_Child_Component.Get_Child(transform, "Item").GetComponent<Button>().onClick.AddListener( () => Load_NV(0)); // Item nv1
        for (int i = 1; i < transform.GetComponentsInChildren<Button>().Length; i++)
        {
            int temp_i = i;
            Get_Child_Component.Get_Child(transform, string.Format("Item ({0})", i)).GetComponent<Button>().onClick.AddListener( () => Load_NV(temp_i)); // Item nv2
        }
    }
    void Load_NV(int index)
    {
        if(Save_Data.Load().index_Curr != index)
            Player.instance.Load_Player_Start(index);
    }
} 
