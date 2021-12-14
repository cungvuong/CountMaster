using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public static Follow instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public GameObject target;
    //public GameObject point;
    //// Update is called once per frame
    [Range(0f,20f)]
    public float z_Cam = 10f;
    GameObject boss;
    [HideInInspector]
    public bool win_Boss_Game;
    private void Start()
    {
        boss = GameObject.FindGameObjectWithTag("boss");
    }

    private void LateUpdate()
    {
        if (!win_Boss_Game)
        {
            if (Player.instance.player_Oj_List.Count != 0)
                if (Player.instance.can_Move)
                {
                    if (Player.instance.player_Oj_List.Count != 0)
                    {
                        if(Player.instance.player_Oj_List[0].gameObject != null)
                        if (!Player.instance.player_Oj_List[0].GetComponent<Player_Manager>().boss_Attack)
                        {
                            transform.position = Vector3.Lerp(transform.position, new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z - z_Cam), Time.deltaTime + 0.08f);
                        }
                        else
                        {

                        }
                    }
                    if (transform.position.x > 0.7f)
                    {
                        transform.position = new Vector3(0.7f, transform.position.y, transform.position.z);
                    }
                    if (transform.position.x < -0.7f)
                    {
                        transform.position = new Vector3(-0.7f, transform.position.y, transform.position.z);
                    }
                }
                else if (Player.instance.player_Oj_List[0].GetComponent<Player_Manager>().boss_Attack)
                {
                    // xu ly khi dang danh boss
                    //
                    transform.position = new Vector3(8f, 5f, 140f);
                    transform.rotation = Quaternion.AngleAxis(-37f, Vector3.up);
                }
        }
        else
        {
            transform.position = new Vector3(0f, 8.5f, transform.position.z);
        }
    }

    public void Reset_Cam()
    {
        transform.rotation = Quaternion.identity; // lay goc quay ban dau
        transform.Rotate(Vector3.right, 40f);
    }
}
