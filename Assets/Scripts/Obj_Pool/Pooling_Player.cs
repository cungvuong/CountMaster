using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pooling_Player : MonoBehaviour
{
    public static Pooling_Player instance;
    public GameObject curr_Player_Pool;
    private GameObject player_Spawn_;
    public List<GameObject> list_Obj = new List<GameObject>();
    public List<GameObject> list_Destroy = new List<GameObject>();
    public GameObject playerAll; // nhan vat trong game
    [HideInInspector]
    public int sodu = 210;
    GameObject Player_Big;
    int new_Index=0;
    int curr_Index=0;
    //PlayerCounter p;
    Vector3[] pos_All = new Vector3[281];
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        Application.targetFrameRate = 90;
        curr_Player_Pool = playerAll;
        player_Spawn_ = GameObject.FindGameObjectWithTag("point_spawn");
        Player_Big = GameObject.FindGameObjectWithTag("Player");

        // xu ly chon nhan vat
        for (int i=0; i< curr_Player_Pool.GetComponentsInChildren<Check_Player>(true).Length; i++) // lay ra list child object
        {
            if (i != Save_Data.Load().index_Curr)
                curr_Player_Pool.GetComponentsInChildren<Check_Player>(true)[i].gameObject.SetActive(false);
            else
            {
                curr_Player_Pool.GetComponentsInChildren<Check_Player>(true)[i].gameObject.SetActive(true);
            }
        }

        for(int i=0; i<210; i++) // sinh truoc 300 object
        {
            GameObject x = Instantiate(curr_Player_Pool, playerAll.transform.position, player_Spawn_.transform.rotation, player_Spawn_.transform.parent);
            x.SetActive(false);
            list_Obj.Add(x);
        }
        Set_Pos();
    }

    private void Start()
    {
        Player.instance.curr_Player = curr_Player_Pool; // set nhan vat ban dau chon
        for (int i = 0; i < Save_Data.Load().mount_Player; i++)
        {
            GameObject x = GetPooledObject();
            Player.instance.player_Oj_List.Add(x);
            // set lai vi tri nhan vat
            x.transform.localPosition = Get_Pos(Player.instance.player_Oj_List.Count); // lay ra vi tri hien tai cho player == index
            x.SetActive(true);
            //p.SetAlive(Player.instance.player_Oj_List.Count);
        }
        Player.instance.Set_Mount_Text();

    }

    void Set_Pos()
    {
        for (int i = 0; i < pos_All.Length; i++)
        {
            pos_All[i] = Get_Player_Pos(i);
        }
    }

    public Vector3 Get_Pos(int index)
    {
        return pos_All[index];
    }

    public Vector3 Get_Player_Pos(int index)
    {
        Vector3 pos = new Vector3(0f, 0.2f, 0f);
        float khoangcachx = -0.3f;
        float khoangcachy = -0.3f;
        int dotang = 10;
        int demvong = 0;
        int player_Index_Set = 0;
        //
        for (int i = 1; i <= dotang; i++)
        {
            float angle = (i - 1) * (2 * 3.14159f / (dotang));
            float x_ = Mathf.Cos(angle) * (khoangcachx);
            float y_ = Mathf.Sin(angle) * (khoangcachy);
            //
            pos += new Vector3(x_, 0f, y_);
            pos_All[player_Index_Set] = pos;
            pos = Vector3.zero;

            if (player_Index_Set == index)
                return pos;
            if (i == dotang)
            {
                dotang += 10;
                khoangcachx += 0.01f;
                khoangcachy += 0.01f;
                i = 0;
                demvong++;
            }
            if (demvong == 6)
            {
                break;
            }
            player_Index_Set++;
        }
        return pos;
    } 

    public void Change_Player(int curr_Index)
    {
        this.curr_Index = curr_Index;
        new_Index = Save_Data.Load().index_Curr;
        Debug.Log(curr_Index + " curr" + new_Index + " new");
        var list_Active = Player.instance.player_Oj_List;
        list_Active.ForEach(x =>  // setnhung player active
        {
            x.gameObject.GetComponentsInChildren<Check_Player>(true)[new_Index].gameObject.SetActive(true);
            x.gameObject.GetComponentsInChildren<Check_Player>(true)[curr_Index].gameObject.SetActive(false);
        });
    }

    public void Set_NV_After_Back()
    {
        // set lai index
        var list_Active = list_Obj;
        list_Active.ForEach(x =>
        {
            if(!x.gameObject.GetComponentsInChildren<Check_Player>(true)[new_Index].gameObject.activeSelf)
            {
                x.gameObject.GetComponentsInChildren<Check_Player>(true)[new_Index].gameObject.SetActive(true);
                x.gameObject.GetComponentsInChildren<Check_Player>(true)[this.curr_Index].gameObject.SetActive(false);
            }
        });
    }

    public void Start_Game_Ran()
    {
        sodu = 210;
        Player.instance.player_Oj_List.Clear();
        for (int i = 0; i < list_Obj.Count; i++) // set lai cac object false
        {
            if(list_Obj[i].activeSelf)
                list_Obj[i].SetActive(false);
        }
        for (int i = 0; i < Save_Data.Load().mount_Player; i++) // khoi tao cac object ban dau
        {
            GameObject x = GetPooledObject();
            if (x != null)
            {
                Player.instance.player_Oj_List.Add(x);
                x.transform.localPosition = Get_Pos(Player.instance.player_Oj_List.Count);
                x.transform.rotation = Quaternion.identity;
                x.SetActive(true);
                Player.instance.Set_Mount_Text();
            }
        }
    }

    public GameObject GetPooledObject()
    {
        for(int i=0; i<list_Obj.Count; i++)
        {
            if (!list_Obj[i].activeInHierarchy)
            {
                return list_Obj[i];
            }
        }
        return null;
    }

    public bool Lose_Game()
    {
        for (int i = 0; i < list_Obj.Count; i++)
        {
            if (list_Obj[i].activeSelf)
            {
                return false;
            }
        }
        return true;
    }

    public void UpDate_Spawn_Player() // trong game   
    {
        this.sodu = 210;
    }

    public int Mount_Player_Active()
    {
        return Player_Big.GetComponentsInChildren<Player_Manager>().Length;
    }

    public void Manager_Du()
    {
        this.sodu++;
    }
}
