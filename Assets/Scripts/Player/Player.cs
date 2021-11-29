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
    public bool can_Move = true;
    [HideInInspector]
    public List_Level list = new List_Level();

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
        Save_Data.Save(list);
    }

    void Update()
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
        }
        else
        {
            End_Game();
            return;
        }
    }
    public void End_Game()
    {
        menu_PlayerAgain.SetActive(true);
        menu_Controll.SetActive(false);
        can_Move = false;
        Debug.Log("Die");
    }

    public void Start_Addforce()
    {
        time_Center_Check = StartCoroutine(Time_Center());
    }

    public void Stop_Corotine_Addforce()
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


    public void Reset()
    {
        SceneManager.LoadScene(0);
    }
}
