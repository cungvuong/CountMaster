using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooling_Player : MonoBehaviour
{
    public static Pooling_Player instance;
    public GameObject curr_Player_Pool;
    private GameObject player_Spawn_;
    public List<GameObject> list_Obj = new List<GameObject>();
    public List<GameObject> list_Destroy = new List<GameObject>();
    GameObject[] list_Obj_Start;
    public GameObject playerAll; // nhan vat trong game
    int sodu = 300;
    PlayerCounter p;
    Vector3[] pos_All = new Vector3[280];
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        Application.targetFrameRate = 90;
        curr_Player_Pool = playerAll;
        player_Spawn_ = GameObject.FindGameObjectWithTag("point_spawn");
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

        for(int i=0; i<360; i++) // sinh truoc 300 object
        {
            GameObject x = Instantiate(curr_Player_Pool, player_Spawn_.transform.position, player_Spawn_.transform.rotation, player_Spawn_.transform.parent);
            x.SetActive(false);
            list_Obj.Add(x);
        }
        p = new PlayerCounter(360, false);
    }

    private void Start()
    {
        Player.instance.curr_Player = curr_Player_Pool; // set nhan vat ban dau chon
        for (int i = 0; i < Save_Data.Load().mount_Player; i++)
        {
            float posx = Random.Range(-1f, 1f);
            float posz = Random.Range(-1f, 1f);
            GameObject x = GetPooledObject();
            //GameObject x = Instantiate(curr_Player_Pool, player_Spawn_.transform.position + new Vector3(posx, 0f, posz), curr_Player_Pool.transform.rotation, player_Spawn_.transform.parent);
            Player.instance.player_Oj_List.Add(x);
            // set lai vi tri nhan vat
            //x.transform.position = player_Spawn_.transform.position + new Vector3(posx, 0f, posz);
            x.transform.position = Get_Player_Pos(Player.instance.player_Oj_List.Count); // lay ra vi tri hien tai cho player == index
            x.SetActive(true);
            p.SetAlive(Player.instance.player_Oj_List.Count);
        }
        Player.instance.Set_Mount_Text();
        //Start_Game_Ran(); // start game
    }

    public Vector3 Get_Player_Pos(int index)
    {
        Vector3 pos = player_Spawn_.transform.position;
        float khoangcachx = 0.4f;
        float khoangcachy = 0.4f;
        int dotang = 10;
        int demvong=0;
        // player can tim la con thu index +1;
        int player_Index_Set = 0;
        //
        for (int i = 1; i <= dotang; i++)
        {
            float angle = i * (2 * 3.14159f / (dotang));
            float x_ = Mathf.Cos(angle) * (khoangcachx);
            float y_ = Mathf.Sin(angle) * (khoangcachy);
            //
            pos += new Vector3(x_, 0f, y_);

            if (player_Index_Set == index)
                return pos;
            if (i == dotang)
            {
                dotang += 10;
                khoangcachx += 0.2f;
                khoangcachy += 0.2f;
                i = 0;
                demvong++;
            }
            if (demvong == 8)
            {
                break;
            }
            player_Index_Set++;
        }
        return pos;
    } 

    public void Change_Player()
    {
        int index = 0;
        foreach(GameObject x in list_Obj)
        {
            for (int i = 0; i < x.GetComponentsInChildren<Check_Player>(true).Length; i++)
            {
                if (i != Save_Data.Load().index_Curr)
                    x.GetComponentsInChildren<Check_Player>(true)[i].gameObject.SetActive(false);
                else
                {
                    index = i;
                    x.GetComponentsInChildren<Check_Player>(true)[i].gameObject.SetActive(true);
                    curr_Player_Pool = x;
                }
            }
        }
        Player.instance.curr_Player = curr_Player_Pool;
    }

    public void Start_Game_Ran()
    {
        Player.instance.player_Oj_List = new List<GameObject>();
        //list_Obj = new List<GameObject>();
        for (int i = 0; i < list_Obj.Count; i++) // set lai cac object false
        {
            list_Obj[i].SetActive(false);
        }

        for (int i = 0; i < Save_Data.Load().mount_Player; i++) // khoi tao cac object ban dau
        {
            float posx = Random.Range(-1f, 1f);
            float posz = Random.Range(-1f, 1f);
            //GameObject x = Instantiate(curr_Player_Pool, player_Spawn_.transform.position + new Vector3(posx, 0f, posz), curr_Player_Pool.transform.rotation, player_Spawn_.transform.parent) as GameObject;
            GameObject x = GetPooledObject();
            if (x != null)
            {
                //x.transform.position = player_Spawn_.transform.position + new Vector3(posx, 0f, posz);
                Player.instance.player_Oj_List.Add(x);
                x.transform.position = Get_Player_Pos(Player.instance.player_Oj_List.Count);
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
        // spawn 
        if (list_Obj.Count <= 300)
        {
            sodu = 300;
        }
    }

    public int Mount_Player_Active()
    {
        int dem = 0;
        foreach(GameObject x in list_Obj)
        {
            if (x.activeSelf)
            {
                dem++;
            }
        }
        return dem;
    }

    public void Manager_Du()
    {
        sodu++;
        Player.instance.mount_Player.text = sodu.ToString();
    }
}
