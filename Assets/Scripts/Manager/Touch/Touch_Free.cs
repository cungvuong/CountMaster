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
    int mount_Du =0;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }    
        player_Spawn_ = GameObject.FindGameObjectWithTag("point_spawn");
    }

    private void OnEnable()
    {
        clone_Free_X = Random.Range(2, 4);
        if (cal_Free)
        {
            text_Mesh_Clone.text = "x" + clone_Free_X.ToString();
        }
        else
        {
            clone_Free_X *= 15;
            text_Mesh_Clone.text = "+" + clone_Free_X.ToString();
        }
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
            if((mount_Clone + Player.instance.player_Oj_List.Count) >= 210)
            {
                mount_Du = (mount_Clone + Player.instance.player_Oj_List.Count) - 210;
                mount_Clone = 210 - Player.instance.player_Oj_List.Count;
                Pooling_Player.instance.sodu += mount_Du;
            }
            if(mount_Clone!=0)
            for (int j = 0; j < mount_Clone; j++) // sinh them so luong player
            {
                GameObject x = Pooling_Player.instance.GetPooledObject();
                if (x != null)
                {
                    Player.instance.player_Oj_List.Add(x);
                    x.transform.localPosition = Pooling_Player.instance.Get_Pos(Player.instance.player_Oj_List.Count);
                    x.transform.rotation = Quaternion.identity;
                    x.SetActive(true);
                }
            }
            if (Pooling_Player.instance.sodu > 210)
            {
                Player.instance.mount_Player.text = Pooling_Player.instance.sodu.ToString();
            }
            if(Player.instance.player_Oj_List.Count <210)
            {
                Pooling_Player.instance.UpDate_Spawn_Player(); // tinh lai so du
                mount_Du = 0;
                Player.instance.mount_Player.text = Player.instance.player_Oj_List.Count.ToString();
            }
            AddForce_All_Player();
            box_Left_.GetComponent<BoxCollider>().enabled = false;
        }
        gameObject.GetComponent<BoxCollider>().enabled = false;
        gameObject.SetActive(false);
    }

    void AddForce_All_Player()
    {
        for (int i = 0; i < Player.instance.player_Oj_List.Count; i++)
        {
            Player.instance.player_Oj_List[i].GetComponent<Player_Manager>().Corrotine_Center_Force(0.25f);
        }
    }
}
