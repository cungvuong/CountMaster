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
    }

    public GameObject ground1; 
    public GameObject ground2; 
    public GameObject gr_co_cau; 
    public GameObject gr_ko_cau; 
    
    public GameObject things_Parent;
    int range;
    public GameObject[] things_Clone_Free;
    public GameObject[] things_Clone_All;
    public GameObject[] things_Map1_1;
    public GameObject[] things_Map1_2;
    //public GameObject things_Clone_Ques;
    public GameObject[] things_Clone_Ques_Trap;
    public GameObject mountain; // nui'
    public GameObject mountain_Parent;
    public GameObject trans_Player;
    public GameObject Boss; // ran boss
    GameObject[] clone_ques;
    GameObject[] clone_free;
    float[] range_Trap_Dis = {15f, 40f, 50f, 60f, 70f, 95f, 105f, 115f, 125f, 135f};
    int trap_Dis;
    //
    private void Start()
    {
        CreateNewMap();
        Range_Grounds();
    }

    public void CreateNewMap()
    {
        // ran map 1.1 
        Instantiate(things_Clone_All[Random.Range(0, things_Clone_All.Length)], things_Clone_All[Random.Range(0, things_Clone_All.Length)].transform.position, things_Clone_All[Random.Range(0, things_Clone_All.Length)].transform.rotation, things_Parent.transform);
        Instantiate(things_Clone_All[Random.Range(0, things_Clone_All.Length)], things_Clone_All[Random.Range(0, things_Clone_All.Length)].transform.position + new Vector3(0f, 0f, 8f), things_Clone_All[Random.Range(0, things_Clone_All.Length)].transform.rotation, things_Parent.transform);
        //range = Random.Range(0,2);   // range map 1 or 2
        // clone Trap map 1.1
        trap_Dis = 0;
        range = Random.Range(0, things_Map1_1.Length);
        for(int i=0; i< 1; i++)// range_Trap_Dis.Length; i++)
        {
            Instantiate(things_Map1_1[range], things_Map1_1[range].transform.position + new Vector3(0f, 0f, range_Trap_Dis[trap_Dis]), Quaternion.identity, things_Parent.transform);
            trap_Dis++;
        }
        // clone Trap map 1.2
        for (int i = 0; i < range_Trap_Dis.Length - 4; i++)// range_Trap_Dis.Length; i++)
        {
            range = Random.Range(0, things_Map1_2.Length);
            Instantiate(things_Map1_2[range], things_Map1_2[range].transform.position + new Vector3(0f, 0f, range_Trap_Dis[trap_Dis]), things_Map1_2[range].transform.rotation, things_Parent.transform);
            trap_Dis++;
        }
        // clone Trap map 1.3
        for (int i = 0; i < range_Trap_Dis.Length-7; i++)// range_Trap_Dis.Length; i++)
        {
            range = Random.Range(0, things_Map1_2.Length);
            Instantiate(things_Map1_2[range], things_Map1_2[range].transform.position + new Vector3(0f, 0f, range_Trap_Dis[trap_Dis]), things_Map1_2[range].transform.rotation, things_Parent.transform);
            trap_Dis++;
        }
        Range_Mountain();
        Manager_Clone_Range();
        Manager_Tru();
        Manager_Bua_Dao();
    }

    void Range_Grounds()
    {
        range = Random.Range(0, 1);
        if (range == 1)
        {
            range = Random.Range(0, 3);
            if (range == 0)
            {
                Instantiate(ground1, transform.position, Quaternion.identity, transform);
                Instantiate(ground2, transform.position + new Vector3(0f, 0f, 100f), Quaternion.identity, transform);
            }
            if (range == 1)
            {
                Instantiate(ground2, transform.position, Quaternion.identity, transform);
                Instantiate(ground1, transform.position + new Vector3(0f, 0f, 100f), Quaternion.identity, transform);
            }
            if (range == 2)
            {
                Instantiate(ground2, transform.position, Quaternion.identity, transform);
                Instantiate(ground2, transform.position + new Vector3(0f, 0f, 100f), Quaternion.identity, transform);
            }
        } // range ground
        else
        {
            int k = 1;
            Instantiate(gr_co_cau, transform.position, gr_co_cau.transform.rotation, transform);
            Instantiate(gr_co_cau, transform.position + new Vector3(0f, 0f, 53f * k), gr_co_cau.transform.rotation, transform); k++;
            GameObject late = Instantiate(gr_ko_cau, transform.position + new Vector3(0f, 0f, 53f * k), gr_ko_cau.transform.rotation, transform); k++;
            Instantiate(gr_ko_cau, late.transform.position + new Vector3(0f, 0f, 50f), gr_ko_cau.transform.rotation, transform); k++;
        }
    }

    void Range_Mountain()
    {
        for(int i=0; i<200; i++)
        {
            float x = Random.Range(-40f, 40f);
            float z = Random.Range(0f, 200f);
            float y = Random.Range(-7.5f, -5f);
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
            Debug.Log("destroy 1 tru");
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
        Transform[] list_Child_Map = things_Parent.GetComponentsInChildren<Transform>();
        Transform[] list_Child_Nui = mountain_Parent.GetComponentsInChildren<Transform>();

        for(int i=1; i<list_Child_Map.Length;i++)
        {
            list_Child_Map[i].gameObject.SetActive(false);
        }
        
        for(int i=1; i< list_Child_Nui.Length;i++)
        {
            list_Child_Nui[i].gameObject.SetActive(false);
        }

        if(list_Child_Map.Length > 100)
        {
            for (int i = 1; i < list_Child_Map.Length; i++)
            {
                if(!list_Child_Map[i].gameObject.activeSelf)
                    Destroy(list_Child_Map[i].gameObject);
            }
        }
        if(list_Child_Nui.Length > 1000)
        {
            for (int i = 1; i < list_Child_Nui.Length; i++)
            {
                if(!list_Child_Nui[i].gameObject.activeSelf)
                    Destroy(list_Child_Nui[i].gameObject);
            }
        }
        Reset_Pos_Player();
        Reset_Boss();
    }

    void Reset_Pos_Player()
    {
        
        trans_Player.transform.position = new Vector3(0f, 0f, -15f);
        Camera.main.transform.position = new Vector3(0f, 8.5f, -29);
    }

    void Reset_Boss()
    {
        Boss.SetActive(true); // hien lai boss
        Boss.GetComponentInChildren<Boss_Manager>().Set_Data_Boss(); // se lai data cho boss
        Boss.GetComponentInChildren<Boss_Manager>().gameObject.transform.rotation = Quaternion.Euler(0f, 180f, 0f); // se lai data cho boss
        trans_Player.GetComponent<Player>().Reset_Game_Init();
        Follow.instance.win_Boss_Game = false;
    }
}
