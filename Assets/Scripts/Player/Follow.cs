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
            if(Player.instance.player_Oj_List.Count != 0)
            {
                if (!Player.instance.player_Oj_List[0].GetComponent<Player_Manager>().boss_Attack)
                {
                    if (transform.position.x < 1.5f || transform.position.x > -1.5f)
                    {
                        transform.position = Vector3.Lerp(transform.position, new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z-z_Cam), Time.deltaTime + 0.05f);
                    }
                    else
                    {
                        if(transform.position.x > 1.5f)
                            transform.position = Vector3.Lerp(transform.position, new Vector3(1.5f, transform.position.y, target.transform.position.z - z_Cam), Time.deltaTime + 0.015f);
                        else if(transform.position.x < -1.5f)
                        {
                            transform.position = Vector3.Lerp(transform.position, new Vector3(-1.5f, transform.position.y, target.transform.position.z - z_Cam), Time.deltaTime + 0.015f);
                        }
                        Debug.Log("follwow");
                    }
                }
            }
            else
            {
                return;
            }
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(0f, transform.position.y, transform.position.z), Time.deltaTime);
        }
    }
}
