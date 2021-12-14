using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Get_Child_Component
{
    public static GameObject Get_Child(Transform parent, string child_Name)
    {
        RectTransform[] list_Child = parent.GetComponentsInChildren<RectTransform>();

        foreach(RectTransform x in list_Child)
        {
            if (x.name == child_Name)
            {
                return x.gameObject;
            }
        }
        return null;
    }
}
