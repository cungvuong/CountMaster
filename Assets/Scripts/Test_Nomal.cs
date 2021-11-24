using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Nomal : MonoBehaviour
{
    public Camera cam;
    Vector3 pos;
    Vector3 pos_Get;
    float get_Curr_X1;
    float get_Curr_X2;
    static int move_Arrow = -10;

    private void Update()
    {
        Debug.Log(move_Arrow);    
    }

    public void StopDrag_Check()
    {
        pos = Input.mousePosition;
        pos_Get = cam.ScreenToWorldPoint(pos + new Vector3(0f, -18f, 6f));
        get_Curr_X2 = pos_Get.x;

        if (get_Curr_X1 > get_Curr_X2)
        {
            Debug.Log(-1);
            move_Arrow = - 1;
        }
        else if (get_Curr_X1 < get_Curr_X2)
        {
            Debug.Log(1);
            move_Arrow =  1;
        }
        else
        {
            Debug.Log(0);
            move_Arrow =  0;
        }
    }

    public void BeginDrag_Check()
    {
        pos = Input.mousePosition;
        pos_Get = cam.ScreenToWorldPoint(pos + new Vector3(0f, -18f, 6f));
        get_Curr_X1 = pos_Get.x;
    }
}
