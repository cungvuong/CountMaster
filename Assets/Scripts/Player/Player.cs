using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static Player instance;
    [Range(5, 20)]
    public float speedX = 5f;
    [Range(1, 10)]
    public float speedZ = 5f;
    public TextMeshProUGUI mount_Player;
    [HideInInspector]
    public GameObject[] player_Oj;
    [HideInInspector]
    public List<GameObject> player_Oj_List = new List<GameObject>(); // luu cac objetc dang su dung
    public GameObject menu_PlayerAgain;
    public GameObject menu_Controll;
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
    public GameObject start_UI;
    GameObject boss;
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
        
        point_Spawn = GameObject.FindGameObjectWithTag("point_spawn"); // diem sinh nhan vat
        boss = GameObject.FindGameObjectWithTag("boss");
        time_Center = true;
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

            if (player_Oj_List[0].GetComponent<Player_Manager>().boss_Attack)
            {
                can_Move = false;
            }
            if (player_Oj_List[0].GetComponent<Player_Manager>().tru_Attack)
            {
                speedZ = 3f;
                can_Move = false;
            }
            else
            {
                speedZ = 7f;
            }
        }
        else if(player_Oj_List.Count == 0 || Pooling_Player.instance.Lose_Game())
        {
            End_Game();
        }
        if (!boss.GetComponent<Boss_Manager>().alive)
        {
            StartCoroutine(Time_Win_Game());
        }
    } // di chuyen
    
    public void Start_AddForce()
    {
        StartCoroutine(AddForce_All_Player());
    }
    public IEnumerator AddForce_All_Player()
    {
        time_Center = false;
        yield return new WaitForSeconds(1.8f);
        foreach (GameObject x in player_Oj_List)
        {
            x.GetComponent<Player_Manager>().Corrotine_Center_Force(0.8f);
        }
        time_Center = true;
    }

    public void End_Game() // end game
    {
        StartCoroutine(Time_Appear());
        menu_Controll.SetActive(false);
        can_Move = false;
    } 

    public void Reset_Game_Init()
    {
        startGame = false;
        can_Move = false;
        start_UI.SetActive(true);
        menu_Controll.SetActive(true);
    }

    IEnumerator Time_Win_Game()
    {
        yield return new WaitForSeconds(4f);
        startGame = false;
    }

    IEnumerator Time_Appear()
    {
        yield return new WaitForSeconds(0.5f);
        menu_PlayerAgain.SetActive(true);
    }

    public void StartGame()  // touch to play
    {
        startGame = true;
        can_Move = true;
        start_UI.SetActive(false);
        StopAllCoroutines();
    }

    public void Load_Player_Start(int index)
    {
        int curr_Index = Save_Data.Load().index_Curr; // load index trc do
        List_Level list = new List_Level(Save_Data.Load().level_Current, Save_Data.Load().mount_Player, index);
        Save_Data.Save(list); // luu lai NV dang chon
        Pooling_Player.instance.Change_Player(curr_Index);
    }
    
    public void Set_Mount_Text() // set lai so luong player
    {
        mount_Player.text = player_Oj_List.Count.ToString();
    }

    public void Inscrease_Player_ByGold(int mount) // tang player khi mua cap player
    {
        GameObject x = Pooling_Player.instance.GetPooledObject();
        if (x != null)
        {
            Player.instance.player_Oj_List.Add(x);
            x.transform.localPosition = Pooling_Player.instance.Get_Pos(Player.instance.player_Oj_List.Count);
            x.transform.rotation = Quaternion.identity;
            x.SetActive(true);
        }
        Set_Mount_Text();
    }
}
