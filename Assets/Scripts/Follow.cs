using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public GameObject target;
    //public GameObject point;
    //// Update is called once per frame
    [Range(0f,10f)]
    public float z_Cam = 10f;

    private void LateUpdate()
    {
        if (Player.instance.can_Move)
        {
            if(Player.instance.player_Oj_List != null)
            if (!Player_Manager.instance.boss_Attack)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z-z_Cam), Time.deltaTime + 0.015f);
            }
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(0f, transform.position.y, transform.position.z), Time.deltaTime);
        }
    }
}
