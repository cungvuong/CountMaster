using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooling_Player : MonoBehaviour
{
    public static Pooling_Player instance;
    public GameObject curr_Player_Pool;
    private GameObject player_Spawn_;
    public List<GameObject> list_Obj = new List<GameObject>();
    List<GameObject> list_Destroy = new List<GameObject>();

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        // khoi tao so luong player khi bat dau game
        curr_Player_Pool = Resources.LoadAll<GameObject>("Clother_Obj")[Save_Data.Load().index_Curr];
        player_Spawn_ = GameObject.FindGameObjectWithTag("point_spawn");

        for (int i = 0; i < Save_Data.Load().mount_Player; i++)
        {
            float posx = Random.Range(-1f, 1f);
            float posz = Random.Range(-1f, 1f);
            GameObject x = Instantiate(curr_Player_Pool, player_Spawn_.transform.position + new Vector3(posx, 0f, posz), curr_Player_Pool.transform.rotation, player_Spawn_.transform.parent);

            list_Obj.Add(x);
            list_Destroy.Add(x); // list destroy
            Player.instance.player_Oj_List.Add(x);
            Player.instance.Set_Mount_Text();
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
    
    public void UpDate_Spawn_Player() // trong game
    {
        // spawn 
        if (list_Obj.Count <= 300)
        {
            GameObject x = Instantiate(curr_Player_Pool, player_Spawn_.transform.position, player_Spawn_.transform.rotation, player_Spawn_.transform.parent);
            x.SetActive(false);
            list_Obj.Add(x);
        }
    }

    public List<GameObject> Set_Data_Again() // set data truoc khi vao game
    {
        list_Obj = new List<GameObject>();
        for (int i = 0; i < Save_Data.Load().mount_Player; i++)
        {
            float posx = Random.Range(-1f, 1f);
            float posz = Random.Range(-1f, 1f);
            GameObject x = Instantiate(curr_Player_Pool, player_Spawn_.transform.position + new Vector3(posx, 0f, posz), curr_Player_Pool.transform.rotation, player_Spawn_.transform.parent);

            list_Obj.Add(x);
            list_Destroy.Add(x); // list destroy
        }
        return list_Obj;
    }

    public void Destroy_Player()
    {
        for(int i=list_Destroy.Count-1; i>=0; i--)
        {
            if (!list_Destroy[i].activeSelf)
            {
                Destroy(list_Destroy[i].gameObject);
                Debug.Log("destroy" + list_Destroy[i].name);
            }
        }
        List<GameObject> list = list_Destroy;
        list_Destroy = new List<GameObject>();
        list_Destroy = list;
    }
}
