using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Range(5, 20)]
    public float speedX = 5f;
    [Range(1, 10)]
    public float speedZ = 5f;
    public TextMeshProUGUI mount_Player;
    [HideInInspector]
    public GameObject[] player_Oj;
    [HideInInspector]
    public List<GameObject> player_Oj_List = new List<GameObject>();
    public GameObject menu_PlayerAgain;
    public GameObject menu_Controll;
    public static Player instance;
    public Camera cam;
    public bool time_Center;
    public bool time_Center_Insti;
    public Coroutine time_Center_Check;
    public bool can_Move;
    [HideInInspector]
    public bool startGame;
    public GameObject point_Spawn;

    GameObject[] list_Obj;
    [HideInInspector]
    public GameObject curr_Player;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            return;
        }
        player_Oj = GameObject.FindGameObjectsWithTag("player");
        foreach (GameObject x in player_Oj)
        {
            player_Oj_List.Add(x);
        }
        int sotang = Save_Data.Load().level_Current + 1;
        List_Level list = new List_Level(sotang, 1, Save_Data.Load().index_Curr);
        Save_Data.Save(list);
        point_Spawn = GameObject.FindGameObjectWithTag("point_spawn"); // diem sinh nhan vat
        list_Obj = Resources.LoadAll<GameObject>("Clother_Obj");

        curr_Player = list_Obj[Save_Data.Load().index_Curr]; // load ra player tran trc

        float posx, posz;
        for (int i = 0; i < Save_Data.Load().mount_Player; i++)
        {
            posx = Random.Range(-1f, 1f);
            posz = Random.Range(-1f, 1f);
            GameObject x = Instantiate(curr_Player, point_Spawn.transform.position + new Vector3(posx, 0f, posz), curr_Player.transform.rotation, point_Spawn.transform.parent);
            player_Oj_List.Add(x);
            mount_Player.text = player_Oj_List.Count.ToString();
        }
    }

    void Update()
    {
        if (startGame)
        {
            if (transform.position.x <= -4f)
            {
                transform.position = new Vector3(-4f, transform.position.y, transform.position.z);
            }
            if (transform.position.x >= 4f)
            {
                transform.position = new Vector3(4f, transform.position.y, transform.position.z);
            }

            if (can_Move)
            {
                Movement();
            }
        }
    }

    void Movement()
    {
        if (player_Oj_List.Count != 0) // khi van con player
        {
            if (!player_Oj_List[0].GetComponent<Player_Manager>().boss_Attack) // chua danh boss
                transform.Translate(Vector3.forward * speedZ * Time.deltaTime);
            if (time_Center)
            {
                foreach (GameObject x in player_Oj_List)
                {
                    x.GetComponent<Player_Manager>().time_Center = true;
                }
            }
            else
            {
                foreach (GameObject x in player_Oj_List)
                {
                    x.GetComponent<Player_Manager>().time_Center = false;
                }
            }
            if (player_Oj_List[0].GetComponent<Player_Manager>().boss_Attack)
            {
                can_Move = false;
            }
            else
            {
                can_Move = true;
            }
        }
        else
        {
            End_Game();
        }
    } // di chuyen
    public void End_Game()
    {
        menu_PlayerAgain.SetActive(true);
        menu_Controll.SetActive(false);
        can_Move = false;
        //Debug.Log("Die");
    } // end game

    public void Start_Addforce()
    {
        time_Center_Check = StartCoroutine(Time_Center());
    } // nhan vat ep gan nhau

    public void Stop_Corotine_Addforce() // nhan vat ep gan nhau
    {
        if(time_Center_Check != null)
        {
            StopCoroutine(time_Center_Check);
        }
    } 

    IEnumerator Time_Center()
    {
        time_Center = true;
        yield return new WaitForSeconds(1f);
        time_Center = false;
    }


    public void Reset() // load scene
    {
        SceneManager.LoadScene(0);
    }

    public void StartGame(GameObject button)
    {
        startGame = true;
        button.SetActive(false);
        button.transform.parent.gameObject.SetActive(false);
    }

    public GameObject Load_Player_Start(int new_Index)
    {
        float posx = Random.Range(-1f, 1f);
        float posz = Random.Range(-1f, 1f);
        for (int j = 0; j < Player.instance.player_Oj_List.Count; j++) // xet ma sat thap de de ep
        {
            player_Oj_List[j].GetComponent<Rigidbody>().drag = 10f;
        }

        foreach(GameObject destroy in player_Oj_List) // huy het nhung player cu
        {
            Destroy(destroy);
        }
        player_Oj_List = new List<GameObject>();
        for(int z=0; z<Save_Data.Load().mount_Player; z++) // instantiate ra nhung player moi
        {
            GameObject x = Instantiate(list_Obj[new_Index], point_Spawn.transform.position + new Vector3(posx, 0f, posz), list_Obj[new_Index].transform.rotation, point_Spawn.transform.parent);
            player_Oj_List.Add(x);
            Set_Mount_Text();
        }

        for (int k = 0; k < player_Oj_List.Count; k++) // xet ma sat cao
        {
            player_Oj_List[k].GetComponent<Rigidbody>().drag = 10f;
        }

        curr_Player = list_Obj[new_Index]; // set lai curr player
        List_Level list = new List_Level(Save_Data.Load().level_Current, Save_Data.Load().mount_Player, new_Index);
        Save_Data.Save(list); // luu lai NV dang chon
        return list_Obj[new_Index];
    }
    
    void Set_Mount_Text() // set lai so luong player
    {
        mount_Player.text = player_Oj_List.Count.ToString();
    }
}
