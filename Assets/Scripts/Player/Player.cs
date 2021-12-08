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
    public Coroutine time_Center_Check;
    public bool can_Move;
    [HideInInspector]
    public bool startGame;
    public GameObject point_Spawn;

    [HideInInspector]
    public GameObject[] list_Obj; // list obj khoi dau 9 player
    [HideInInspector]
    public GameObject curr_Player;
    int dem_So_Lan_Click = 0;
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
        //player_Oj = GameObject.FindGameObjectsWithTag("player");
        //foreach (GameObject x in player_Oj)
        //{
        //    player_Oj_List.Add(x);
        //}
        List_Level list = Save_Data.Load(); // lay data hien tai
        list = list.Next_Level(); // set them 1 level
        Save_Data.Save(list); // save data.
        point_Spawn = GameObject.FindGameObjectWithTag("point_spawn"); // diem sinh nhan vat
        list_Obj = Resources.LoadAll<GameObject>("Clother_Obj");

        curr_Player = list_Obj[Save_Data.Load().index_Curr]; // load ra player tran trc

    }

    private void Start()
    {
        float posx, posz;
        for (int i = 0; i < Save_Data.Load().mount_Player; i++)
        {
            posx = Random.Range(-1f, 1f);
            posz = Random.Range(-1f, 1f);
            GameObject x = Pooling_Player.instance.GetPooledObject();
            if (x != null)
            {
                x.SetActive(true);
                x.transform.position += new Vector3(posx, 0f, posz);
                player_Oj_List.Add(x);
                mount_Player.text = player_Oj_List.Count.ToString();
            }
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
            else
            {
                // xu ly may quay
                //cam.transform.position = new Vector3(13.3f, 7.7f, 133.6f);
                //cam.transform.rotation = Quaternion.AngleAxis(-37f, Vector3.up);
            }
            if (player_Oj_List.Count !=1) //time_Center
            {
                Debug.Log("ep");
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
            if (player_Oj_List[0].GetComponent<Player_Manager>().tru_Attack)
            {
                speedZ = 2f;
            }
            else
            {
                speedZ = 7f;
            }

        }
        else
        {
            End_Game();
        }
    } // di chuyen
    public void End_Game()
    {
        StartCoroutine(Time_Appear());
        menu_Controll.SetActive(false);
        can_Move = false;
        //Debug.Log("Die");
    } // end game

    IEnumerator Time_Appear()
    {
        yield return new WaitForSeconds(0.5f);
        menu_PlayerAgain.SetActive(true);
    }

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
        dem_So_Lan_Click++;
        for (int j = 0; j < Player.instance.player_Oj_List.Count; j++) // xet ma sat thap de de ep
        {
            player_Oj_List[j].GetComponent<Rigidbody>().drag = 10f;
        }

        foreach (GameObject destroy in player_Oj_List) // huy het nhung player cu
        {
            destroy.SetActive(false);
        }
        player_Oj_List = new List<GameObject>(); // khoi tao lai list

        curr_Player = list_Obj[new_Index]; // set lai curr player
        Pooling_Player.instance.curr_Player_Pool = curr_Player; // set object pooling curr
        player_Oj_List = Pooling_Player.instance.Set_Data_Again(); // spawn ra cac player moi
        Set_Mount_Text();

        for (int k = 0; k < player_Oj_List.Count; k++) // xet ma sat cao
        {
            player_Oj_List[k].GetComponent<Rigidbody>().drag = 10f;
        }

        if (dem_So_Lan_Click > 5) // neu chon nv nhieu hon 5 lan
        {
            dem_So_Lan_Click = 0;
            Pooling_Player.instance.Destroy_Player();
        }
        List_Level list = new List_Level(Save_Data.Load().level_Current, Save_Data.Load().mount_Player, new_Index);
        Save_Data.Save(list); // luu lai NV dang chon
        
        return list_Obj[new_Index];
    }
    
    public void Set_Mount_Text() // set lai so luong player
    {
        mount_Player.text = player_Oj_List.Count.ToString();
    }

    public void Inscrease_Player_ByGold(int mount) // tang player khi mua cap player
    {
        float posx = Random.Range(-1f, 1f);
        float posz = Random.Range(-1f, 1f);
        for(int i=0; i< mount; i++)
        {
            GameObject x = Instantiate(curr_Player, point_Spawn.transform.position + new Vector3(posx, 0f, posz), curr_Player.transform.rotation, point_Spawn.transform.parent);
            player_Oj_List.Add(x);
            Set_Mount_Text();
        }
    }
}
