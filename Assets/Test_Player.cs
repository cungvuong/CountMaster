using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Player : MonoBehaviour
{
    public static Test_Player instance;

    public GameObject[] list_Ground_Dai;
    GameObject[] list_Ground_Ngan; // cho dung cho boss 
    public GameObject[] list_Ground_Ngan_CoCau;
    GameObject boss_House;
    GameObject cau_Noi;
    GameObject ground_Player;
    GameObject[] all_Ground;
    // list cac object pooling
    List<GameObject> list_Gound = new List<GameObject>();
    List<GameObject> list_CauNoi = new List<GameObject>();
    Vector3[] list_Pos = { new Vector3(-3.13f, -0.2f, -15.06f), new Vector3(-3.13f, -0.2f, 35f), new Vector3(3.12f, -0.2f, 15f), new Vector3(3.12f, -0.2f, -35f) };
    public GameObject Water;
    public GameObject Lava;
    public GameObject Ho_Sau;
    List<GameObject> list_Trap_ = new List<GameObject>();
    GameObject x3;
    // object pooling on ground check
    public GameObject[] Trap_Cau_Gay;
    List<GameObject> list_Trap2 = new List<GameObject>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        all_Ground = Resources.LoadAll<GameObject>("Grounds");
        list_Ground_Ngan = Resources.LoadAll<GameObject>("GroundNgan");
        cau_Noi = list_Ground_Ngan[0];
        boss_House = GameObject.FindGameObjectWithTag("bosshouse");
        ground_Player = GameObject.FindGameObjectWithTag("groundplayer");
    }

    private void OnEnable()
    {
        Range_Pooling_Ground(); // pooling ground
        Range_Start_CauGay(); // pooling trap oon ground
        RangeMap_Start();
    }

    void Range_Pooling_Ground()
    {
        for (int i = 0; i < all_Ground.Length; i++) // pooling cac object
        {
            GameObject instan = all_Ground[i];
            GameObject x = Instantiate(instan, instan.transform.position, instan.transform.rotation, transform);
            list_Gound.Add(x);
            x.SetActive(false);
        }
        GameObject ground_Boss = list_Ground_Ngan[Random.Range(1, list_Ground_Ngan.Length)];
        x3 = Instantiate(ground_Boss, ground_Boss.transform.position, Quaternion.identity, transform); // instatiate nha boss
        // 
        for (int i = 0; i < 2; i++)
        {
            GameObject x = Instantiate(cau_Noi, Vector3.zero, Quaternion.identity, transform);
            list_CauNoi.Add(x);
            x.SetActive(false);
        }
    }

    public void RangeMap_Start()
    {
        Reset_Map(); // reset lai map
        int x1 = Random.Range(0, list_Gound.Count);
        int x2 = Random.Range(0, list_Gound.Count);
        while (x2 == x1)
        {
            x2 = Random.Range(0, list_Gound.Count);
        }
        GameObject Player = GameObject.FindGameObjectWithTag("Player"); // lay trans player 
        GameObject ground1 = list_Gound[x1]; // lay ra map dc chon 1
        GameObject ground2 = list_Gound[x2]; // lay ra map dc chon 2
        ground1.transform.localPosition = Vector3.zero; // set vi tri ground 1
        Range_Map_Check_Pos(ground1, ground2); // ground 2
        Range_Map_Check_Pos(ground2, x3); // ground 3
        ground1.SetActive(true); // set hien thi g1 
        if (ground1.GetComponent<CauGay>())
        {
            Set_Map_CauDaiGay(ground1);
            Map_CauGay(ground1);
        }
        else
        {
            if (CheckDai(ground1))
            {
                Set_Pos_OnGround1(8, ground1);
            }
            else
            {
                Set_Pos_OnGround1(4, ground1);
            }
        }
        ground2.SetActive(true); // set hien thi g2
        if (ground2.GetComponent<CauGay>()) // set trap tren gr2
        {
            Set_Map_CauDaiGay(ground2);
        }
        else
        {
            if (CheckDai(ground2))
            {
                Set_Pos_OnGround2(8, ground2);
            }
            else
            {
                Set_Pos_OnGround2(4, ground2);
            }
        }
        //
        boss_House.transform.position = new Vector3(x3.transform.localPosition.x, boss_House.transform.localPosition.y, x3.transform.localPosition.z + 15f); // set vi tri nha boss
        ground_Player.transform.position = new Vector3(ground1.transform.localPosition.x, ground_Player.transform.localPosition.y, (CheckDai(ground1) ? (-62.5f) : (-37.5f))); // set vi tri ground start player
        ground_Player.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        Player.transform.position = new Vector3(Player.transform.localPosition.x, Player.transform.localPosition.y, ground_Player.transform.localPosition.z - 3f); // set trans Player
    }

    void Map_CauGay(GameObject ground)
    {
        int i = 0;
        Manager_Grounds.instance.manager_Map_Clone_Free = Manager_Grounds.ShuffleList<GameObject>(Manager_Grounds.instance.manager_Map_Clone_Free);
        for (i = 0; i < Manager_Grounds.instance.manager_Map_Clone_Free.Count; i++)
        {
            GameObject x = Manager_Grounds.instance.manager_Map_Clone_Free[i];
            x.transform.parent = ground.transform;
            x.transform.localPosition = new Vector3(x.transform.localPosition.x, x.transform.localPosition.y, ground.transform.localPosition.z + (5 * ((i%2==0) ? 1 : -1))); x.SetActive(true);
        }
    } 

    void Set_Pos_OnGround2(int mountTrap, GameObject ground)
    {
        int kc = 1;
        Manager_Grounds.instance.manager_Map = Manager_Grounds.ShuffleList<GameObject>(Manager_Grounds.instance.manager_Map);
        for (int i = 0; i < mountTrap; i++)
        {
            if (i == (mountTrap / 2)) kc = 1;
            if (i <= (mountTrap/2)-1)
            {
                GameObject x = GetObjectPoolOf(Manager_Grounds.instance.manager_Map);
                x.transform.position = new Vector3(x.transform.position.x, x.transform.position.y, ground.transform.position.z + (-10f * kc)); x.SetActive(true);
                x.transform.parent = ground.transform;
                kc++;
            }
            else
            {
                GameObject x = GetObjectPoolOf(Manager_Grounds.instance.manager_Map);
                x.transform.position = new Vector3(x.transform.position.x, x.transform.position.y, ground.transform.position.z + (10f * kc)); x.SetActive(true);
                x.transform.parent = ground.transform;
                kc++;
            }
        }
    }

    void Set_Pos_OnGround1(int mountTrap, GameObject ground)
    {
        int kc = 1;
        int i = 0;
        Manager_Grounds.instance.manager_Map = Manager_Grounds.ShuffleList<GameObject>(Manager_Grounds.instance.manager_Map);
        Manager_Grounds.instance.manager_Map_Clone_Free = Manager_Grounds.ShuffleList<GameObject>(Manager_Grounds.instance.manager_Map_Clone_Free);
        for (i = 0; i < Manager_Grounds.instance.manager_Map_Clone_Free.Count; i++)
        {
            GameObject x = Manager_Grounds.instance.manager_Map_Clone_Free[i];
            if (mountTrap == 4) // neu la duong ngan
            {
                x.transform.parent = ground.transform;
                x.transform.localPosition = new Vector3(x.transform.localPosition.x, x.transform.localPosition.y, ground.transform.localPosition.z + (-10f * i)); x.SetActive(true);
            }
            else
            {
                x.transform.parent = ground.transform;
                x.transform.localPosition = new Vector3(x.transform.localPosition.x, x.transform.localPosition.y, ground.transform.localPosition.z + (-10f * (i + 3))); x.SetActive(true);
            }
        }

        for (int j = (i); j < mountTrap; j++)
        {
            if (mountTrap == 4 && i == 0) { kc++; continue; };
            if (mountTrap == 8 && (j == 0 || j == 1)) { kc++; continue; };
            if (j == (mountTrap / 2)) kc = 1;
            if (j <= (mountTrap / 2) - 1)
            {
                GameObject x = GetObjectPoolOf(Manager_Grounds.instance.manager_Map);
                x.transform.parent = ground.transform;
                x.transform.localPosition = new Vector3(x.transform.localPosition.x, x.transform.localPosition.y, ground.transform.position.z + (-10f * kc)); x.SetActive(true);
                kc++;
            }
            else
            {
                GameObject x = GetObjectPoolOf(Manager_Grounds.instance.manager_Map);
                x.transform.parent = ground.transform;
                x.transform.localPosition = new Vector3(x.transform.localPosition.x, x.transform.localPosition.y, ground.transform.position.z + (10f * kc)); x.SetActive(true);
                kc++;
            }
        }
    }
    void Set_Map_CauDaiGay(GameObject caugay)
    {
        for (int i = 0; i < 4; i++)
        {
            Range_Trap_OnGround(i, caugay.transform);
        }
        Range_Trap2_OnGround_(0, caugay.transform);
        Range_Trap2_OnGround_(1, caugay.transform);
    }

    public void Reset_Map()
    {
        for (int i = 0; i < list_Gound.Count; i++) // reset list ground
        {
            GameObject x = list_Gound[i];
            if (x.activeSelf)
                x.SetActive(false); // set false an ground
        }
        for (int i = 0; i < list_CauNoi.Count; i++) // reset list cau noi
        {
            GameObject x = list_CauNoi[i];
            if (x.activeSelf)
                x.SetActive(false); // set false an ground
        }
        for (int i = 0; i < list_Trap_.Count; i++) // reset trap
        {
            GameObject x = list_Trap_[i];
            if (x.activeSelf)
                x.SetActive(false);
        }
        for (int i = 0; i < list_Trap2.Count; i++) // reset trap2
        {
            GameObject x = list_Trap2[i];
            if (x.activeSelf)
                x.SetActive(false);
        }
    } // set active false some objects

    void Range_Map_Check_Pos(GameObject checkObj, GameObject instan_Obj) // tinh  khoang cach va dua ra trap
    {
        float Z = 0f;
        Z = (CheckDai(checkObj) ? 50f : 25f) + KcRange(checkObj) + (CheckDai(instan_Obj) ? 50f : 25f);
        instan_Obj.transform.localPosition = checkObj.transform.localPosition + new Vector3(0f, 0f, Z);
        if (KcRange(checkObj) == 8f) //  neu la duong ko co cau
        {
            GameObject x = GetObjectPoolOf(list_CauNoi);
            if (x != null)
            {
                x.transform.localPosition = checkObj.transform.localPosition + new Vector3(0f, 0f, (CheckDai(checkObj) ? 50f : 25f) + 4f); // tinh lai vi tri
                x.SetActive(true);
            }
        }
    }

    GameObject GetObjectPoolOf(List<GameObject> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (!list[i].activeSelf)
                return list[i];
        }
        return null;
    }

    float KcRange(GameObject checkObj)
    {
        float kc;
        if (checkObj.transform.CompareTag("caungan"))
        {
            kc = 3f; // neu doan trc la duong ngan
        }
        else
        {
            kc = 8f; // neu doan trc la duong dai
        }
        return kc;
    }

    bool CheckDai(GameObject checkObj) // check cau dai hay ngan
    {
        if (checkObj.CompareTag("caudai"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static List<E> ShuffleList<E>(List<E> inputList)
    {
        List<E> randomList = new List<E>();

        int randomIndex;
        while (inputList.Count > 0)
        {
            randomIndex = Random.Range(0, inputList.Count);
            randomList.Add(inputList[randomIndex]);
            inputList.RemoveAt(randomIndex);
        }
        return randomList;
    }

    void Range_Start_CauGay() // pooling cac object water, lava, hosau
    {
        for (int i = 0; i < 2; i++) // add dung nham vaf nuoc
        {
            GameObject x1 = Instantiate(Water, transform);
            list_Trap_.Add(x1); x1.SetActive(false);
            GameObject x2 = Instantiate(Lava, transform);
            list_Trap_.Add(x2); x2.SetActive(false);
        }
        for (int i = 0; i < 4; i++) // add ho sau
        {
            GameObject x3 = Instantiate(Ho_Sau, transform);
            list_Trap_.Add(x3);
            x3.SetActive(false);
        }
        int dem = 0;
        for (int i = 0; i < 6; i++) // add trap2 onground
        {
            GameObject x3 = Instantiate(Trap_Cau_Gay[dem], transform);
            list_Trap2.Add(x3); x3.SetActive(false);
            dem++;
            if (dem == 2) dem = 0;
        }
    }

    void Range_Trap_OnGround(int dem, Transform parent) // lay cac object water, lava, hosau khi can dung va set lai vi tri 
    {
        GameObject x = GetObjectPoolOf(list_Trap_);
        x.transform.position = list_Pos[dem];
        x.SetActive(true);
        x.transform.parent = parent;
    }

    void Range_Trap2_OnGround_(int dem, Transform parent)
    {
        GameObject x = GetObjectPoolOf(list_Trap2);
        if (dem == 0)
            x.transform.position = new Vector3(x.transform.position.x, x.transform.position.y, -25f);
        else
            x.transform.position = new Vector3(x.transform.position.x, x.transform.position.y, 25f);
        x.SetActive(true);
        x.transform.parent = parent;
    }
}
