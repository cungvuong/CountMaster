using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public GameObject target;
    //public GameObject point;
    //// Update is called once per frame
    [Range(0f,20f)]
    public float z_Cam = 10f;
    GameObject boss;
    private void Start()
    {
        boss = GameObject.FindGameObjectWithTag("boss");
    }

    private void LateUpdate()
    {
        if (Player.instance.can_Move)
        {
            if(Player.instance.player_Oj_List.Count != 0)
            {
                if (!Player.instance.player_Oj_List[0].GetComponent<Player_Manager>().boss_Attack)
                {
                    transform.position = Vector3.Lerp(transform.position, new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z-z_Cam), Time.deltaTime + 0.08f);
                }
                else
                {
                    Vector3 targetPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                    transform.LookAt(targetPos);
                }
            }
            else
            {
            }
            if(transform.position.x > 0.7f)
            {
                transform.position = new Vector3(0.7f, transform.position.y, transform.position.z);
            }
            if (transform.position.x < -0.7f)
            {
                transform.position = new Vector3(-0.7f, transform.position.y, transform.position.z);
            }
        }
        else
        {
            // xu ly khi dang danh boss
            Vector3 targetPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            transform.LookAt(targetPos);
            transform.position = new Vector3(13.5f, 7.7f, 133f);
            transform.rotation = Quaternion.AngleAxis(-37f, Vector3.up);
            Debug.Log("Camera Move");
        }
    }
}
