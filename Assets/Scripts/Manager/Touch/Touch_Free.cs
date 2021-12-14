using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Touch_Free : MonoBehaviour
{
    public static Touch_Free instance;

    int clone_Free_X;
    public bool cal_Free;
    //public GameObject cam;
    public GameObject player_Spawn_;
    public GameObject box_Left_;
    public TextMeshPro text_Mesh_Clone;
    int mount_Clone =0;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }    
    }

    private void OnEnable()
    {
        clone_Free_X = Random.Range(2, 5);
        if (cal_Free)
        {
            text_Mesh_Clone.text = "x" + clone_Free_X.ToString();
        }
        else
        {
            clone_Free_X *= 15;
            text_Mesh_Clone.text = "+" + clone_Free_X.ToString();
        }
        player_Spawn_ = GameObject.FindGameObjectWithTag("point_spawn");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player"))
        {
            if (cal_Free)
            {
                clone_Free_X--;
                mount_Clone = Pooling_Player.instance.Mount_Player_Active() * clone_Free_X;
            }
            else
            {
                mount_Clone = clone_Free_X;
            }
            // neu tra loi dung
            //for (int i = 0; i < Player.instance.player_Oj_List.Count; i++)
            //{
            //    Player.instance.player_Oj_List[i].GetComponent<Rigidbody>().drag = 10f;
            //}
            for (int j = 0; j < mount_Clone; j++) // sinh them so luong player
            {
                Pooling_Player.instance.UpDate_Spawn_Player(); // moi lan sinh se set active 1 object moi

                float posx = Random.Range(-1.5f, 1.5f);
                float posz = Random.Range(-1.5f, 1.5f);
                GameObject x = Pooling_Player.instance.GetPooledObject();
                if (x != null)
                {
                    x.transform.position = player_Spawn_.transform.position + new Vector3(posx, 0f, posz);
                    x.SetActive(true);
                    Player.instance.player_Oj_List.Add(x);
                    Player.instance.mount_Player.text = Pooling_Player.instance.Mount_Player_Active().ToString();
                }
                else
                {
                    Debug.Log("du");
                    Pooling_Player.instance.Manager_Du();
                }
            }
            box_Left_.GetComponent<BoxCollider>().enabled = false;
            for (int i = 0; i < Player.instance.player_Oj_List.Count; i++)
            {
                Player.instance.player_Oj_List[i].GetComponent<Rigidbody>().drag = 10f;
            }
        }
        gameObject.GetComponent<BoxCollider>().enabled = false;
        gameObject.SetActive(false);
    }
}
