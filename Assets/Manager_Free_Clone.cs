using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_Free_Clone : MonoBehaviour
{
    private void OnEnable()
    {
        Touch_Free[] list_Clone = GetComponentsInChildren<Touch_Free>(true);
        for (int i = 0; i < list_Clone.Length; i++)
        {
            list_Clone[i].gameObject.GetComponent<BoxCollider>().enabled = true;
            list_Clone[i].gameObject.SetActive(true);
        }
    }
}
