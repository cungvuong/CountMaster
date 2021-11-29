using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipe_Touch : MonoBehaviour
{
    public Camera cam;
    Vector3 pos;
    Vector3 pos_Player;
    Vector3 pos_Now_Player;
    Vector3 begin_Pos_Player;
    Vector3 pos_Delta;
    bool can_Touch = true;
    bool right;
    bool left;
    [Range(1,5f)]
    public float ran;

    public void Drag_Mouse()
    {
        pos = Input.mousePosition;
        pos = cam.ScreenToWorldPoint(pos + new Vector3(0f, -11f, 7.6f));
        pos_Now_Player = Player.instance.gameObject.transform.position;
        pos_Delta = (pos - begin_Pos_Player);
        if (Player.instance.can_Move && can_Touch || right || left)
        {
            if(pos_Delta.x > 0)
            {
                Player.instance.gameObject.transform.position = new Vector3(pos_Player.x, pos_Now_Player.y, pos_Now_Player.z) + new Vector3(pos_Delta.x/ran, 0f, 0f);
            }
            else if(pos_Delta.x <0)
            {
                Player.instance.gameObject.transform.position = new Vector3(pos_Player.x, pos_Now_Player.y, pos_Now_Player.z) + new Vector3(pos_Delta.x/ran, 0f, 0f);
            }
            else
            {
            }
        }
        else if(can_Touch == false)
        {
            if(pos_Player.x > 0)
            {
                if (pos_Delta.x < 0)
                {
                    can_Touch = true;
                    left = true;
                }
            } else if(pos_Player.x < 0)
            {
                if (pos_Delta.x > 0)
                {
                    can_Touch = true;
                    right = true;
                }
            }
        }
        if (pos_Player.x > 3.5f || pos_Player.x < -3.5f)
        {
            can_Touch = false;
        }
        else
        {
            can_Touch = true;
            right = left = false;
        }
        //Player.instance.gameObject.transform.Translate(Vector3.right * (pos_Delta.x/ran));// = new Vector3(pos.x/ran, pos_Player.y, pos_Player.z);
    }

    public void Begin_Drag()
    {
        begin_Pos_Player = cam.ScreenToWorldPoint(Input.mousePosition + new Vector3(0f, -11f, 7.6f));
        pos_Player = Player.instance.gameObject.transform.position;
    }
}
