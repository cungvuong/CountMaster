using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_Grounds : MonoBehaviour
{
    public static Manager_Grounds instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        CreateNewMap();
    }

    public GameObject ground1; 
    public GameObject ground2; 
    public GameObject gr_co_cau; 
    public GameObject gr_ko_cau; 
    
    public GameObject things_Parent;
    public GameObject[] things_Clone_Free;
    public GameObject[] things_Clone_All;
    public GameObject[] things_Map1_1;
    public GameObject[] things_Map1_2;
    //public GameObject things_Clone_Ques;
    public GameObject[] things_Clone_Ques_Trap;
    public GameObject mountain; // nui'
    public GameObject mountain_Parent;
    public GameObject trans_Player;
    GameObject Boss; // ran boss
    GameObject[] clone_ques;
    GameObject[] clone_free;
    [HideInInspector]
    public List<GameObject> manager_Map = new List<GameObject>();
    public List<GameObject> manager_Map_Clone_Free = new List<GameObject>();
    float[] range_Trap_Dis = {15f, 40f, 50f, 60f, 70f, 95f, 105f, 115f, 125f, 135f};
    //
    private void OnEnable()
    {
        Boss = GameObject.FindGameObjectWithTag("boss");
    }

    public void CreateNewMap()
    {
        // ran map 1.1 
        GameObject free1 = Instantiate(things_Clone_All[Random.Range(0, things_Clone_All.Length)], things_Clone_All[Random.Range(0, things_Clone_All.Length)].transform.position, things_Clone_All[Random.Range(0, things_Clone_All.Length)].transform.rotation, things_Parent.transform);
        manager_Map_Clone_Free.Add(free1);
        GameObject free2 = Instantiate(things_Clone_All[Random.Range(0, things_Clone_All.Length)], things_Clone_All[Random.Range(0, things_Clone_All.Length)].transform.position + new Vector3(0f, 0f, 8f), things_Clone_All[Random.Range(0, things_Clone_All.Length)].transform.rotation, things_Parent.transform);
        manager_Map_Clone_Free.Add(free2);
        // pooling all trap ground
        Debug.Log(things_Map1_2.Length);
        for (int i = 0; i < 2; i++)
        {
            for(int j=0; j<things_Map1_2.Length; j++)
            {
                GameObject x = Instantiate(things_Map1_2[j], things_Map1_2[j].transform.position, things_Map1_2[j].transform.rotation,things_Parent.transform);
                manager_Map.Add(x);
                x.SetActive(false);
            }
        }
    }

    public static List<E> ShuffleList<E>(List<E> inputList)
    {
        List<E> randomList = new List<E>();

        int randomIndex = 0;
        while (inputList.Count > 0)
        {
            randomIndex = Random.Range(0, inputList.Count);
            randomList.Add(inputList[randomIndex]);
            inputList.RemoveAt(randomIndex);
        }
        return randomList;
    }

    public void Set_New_Map()
    {
        manager_Map = ShuffleList(manager_Map); // random lai map
        manager_Map_Clone_Free = ShuffleList(manager_Map_Clone_Free);
        for (int i=0; i < manager_Map_Clone_Free.Count; i++)
        {
            manager_Map_Clone_Free[i].SetActive(false); // disable de reset object
            manager_Map_Clone_Free[i].SetActive(true);
        }
        for (int i = 0; i < manager_Map.Count; i++)
        {
            GameObject x = manager_Map[i];
            if(x.activeSelf)
                x.SetActive(false); // disable de reset object
        }
        Check_TruvsClone();
    }

    void Check_TruvsClone()
    {
        GameObject[] list_Tru = GameObject.FindGameObjectsWithTag("tru");
        clone_free = GameObject.FindGameObjectsWithTag("free_clone");

        for (int i = 0; i < clone_free.Length; i++)
        {
            for (int j = 0; j < list_Tru.Length; j++)
            {
                if (clone_free[i].transform.position.z > list_Tru[j].transform.position.z)
                {
                    Vector3 x = clone_free[i].transform.position;
                    clone_free[i].transform.position = new Vector3(clone_free[i].transform.position.x,clone_free[i].transform.position.y,list_Tru[j].transform.position.z);
                    list_Tru[j].transform.position = new Vector3(list_Tru[j].transform.position.x, list_Tru[j].transform.position.y, x.z);
                }
            }
        }
    }

    void Range_Mountain()
    {
        for (int i = 0; i < 200; i++)
        {
            float x = Random.Range(-40f, 40f);
            float z = Random.Range(0f, 200f);
            //float y = Random.Range(-7.5f, -5f);
            Instantiate(mountain, transform.position + new Vector3(x, -7.5f, z), Quaternion.identity, mountain_Parent.transform);
        }
    }

    void Manager_Clone_Range()
    {
        clone_free = GameObject.FindGameObjectsWithTag("free_clone");
        clone_ques = GameObject.FindGameObjectsWithTag("clone_ques");

        for (int i = 0; i < clone_free.Length; i++)
        {
            for (int j = 0; j < clone_ques.Length; j++)
            {
                if (clone_free[i].transform.position.z >= clone_ques[j].transform.position.z)
                {
                    Vector3 x = clone_free[i].transform.position;
                    clone_free[i].transform.position = clone_ques[j].transform.position;
                    clone_ques[j].transform.position = x;
                }
            }
        }
    }

    void Manager_Tru()
    {
        GameObject[] list_Tru = GameObject.FindGameObjectsWithTag("tru");
        if(list_Tru.Length >= 2)
        {
            for(int i=1; i<list_Tru.Length; i++)
            {
                // sinh ra free clone thay the
                list_Tru[i].SetActive(false);
            }
        }
    }

    void Manager_Bua_Dao()
    {
        GameObject[] list_Dao = GameObject.FindGameObjectsWithTag("dao");
        GameObject[] list_Bua = GameObject.FindGameObjectsWithTag("bua");
        GameObject[] super_Trap = GameObject.FindGameObjectsWithTag("super_trap");
        GameObject[] list_free = GameObject.FindGameObjectsWithTag("free_clone");
        if (list_Dao.Length >2)
        {
            for (int i = 2; i < list_Dao.Length; i++)
            {
                // sinh ra free clone thay the
                Instantiate(things_Clone_All[Random.Range(0, things_Clone_All.Length)], things_Clone_All[Random.Range(0, things_Clone_All.Length)].transform.position + new Vector3(0f, 0f, list_Dao[i].transform.position.z), things_Clone_All[Random.Range(0, things_Clone_All.Length)].transform.rotation, things_Parent.transform);
                list_Dao[i].SetActive(false);
            }
        }

        if (list_Bua.Length > 2)
        {
            for (int i = 2; i < list_Bua.Length; i++)
            {
                // sinh ra free clone thay the
                Instantiate(things_Clone_All[Random.Range(0, things_Clone_All.Length)], things_Clone_All[Random.Range(0, things_Clone_All.Length)].transform.position + new Vector3(0f, 0f, list_Bua[i].transform.position.z), things_Clone_All[Random.Range(0, things_Clone_All.Length)].transform.rotation, things_Parent.transform);

                list_Bua[i].SetActive(false);
            }
        }

        if (super_Trap.Length > 2)
        {
            for (int i = 2; i < super_Trap.Length; i++)
            {
                // sinh ra free clone thay the
                Instantiate(things_Clone_All[Random.Range(0, things_Clone_All.Length)], things_Clone_All[Random.Range(0, things_Clone_All.Length)].transform.position + new Vector3(0f, 0f, super_Trap[i].transform.position.z), things_Clone_All[Random.Range(0, things_Clone_All.Length)].transform.rotation, things_Parent.transform);
                super_Trap[i].SetActive(false);
            }
        }

        if (list_free.Length > 4)
        {
            for (int i = 4; i < list_free.Length; i++)
            {
                // sinh ra free clone thay the
                list_free[i].SetActive(false);
            }
        }
    }

    public void ResetMap()  // set lai map moi
    {
        Set_New_Map();
        Reset_Pos_Player();
        Reset_Boss();
        Follow.instance.Reset_Cam();
    }

    void Reset_Pos_Player()
    {
        Test_Player.instance.RangeMap_Start();
        Player.instance.gameObject.transform.position = GameObject.FindGameObjectWithTag("groundplayer").transform.position + new Vector3(0f, 0f, -3f);
        Player.instance.tru = null;
        Camera.main.transform.position = new Vector3(0f, 8.5f, -29);
    }
     
    void Reset_Boss()
    {
        //Boss.SetActive(true); // hien lai boss
        Boss.GetComponentInChildren<Boss_Manager>().Set_Data_Boss(); // se lai data cho boss
        trans_Player.GetComponent<Player>().Reset_Game_Init();
        Follow.instance.win_Boss_Game = false;
    }
}
