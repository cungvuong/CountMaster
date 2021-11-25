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
    public List<GameObject> player_Oj_List;
    public GameObject menu_PlayerAgain;
    public GameObject menu_Controll;
    public static Player instance;
    public Camera cam;
    float area_Circle;
    public bool time_Center;
    public bool time_Center_Insti;
    public Coroutine time_Center_Check;
    public bool can_Move = true;
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
    }

    void Update()
    {
        Movement();
    }
    
    void Movement()
    {
        if (player_Oj_List.Count != 0 && can_Move)
        {
            if(!player_Oj_List[0].GetComponent<Player_Manager>().boss_Attack) // chua danh boss
                transform.Translate(Vector3.forward * speedZ * Time.deltaTime);
            if (player_Oj_List[0].GetComponent<Player_Manager>().boss_Attack)
            {
                can_Move = false;
                cam.transform.position = Vector3.Lerp(cam.transform.position, new Vector3(0f, cam.transform.position.y, cam.transform.position.z), Time.deltaTime);
            }
            if (time_Center)
            {
                foreach(GameObject x in player_Oj_List)
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
        }
        else if(player_Oj_List.Count == 0)
        {
            menu_PlayerAgain.SetActive(true);
            menu_Controll.SetActive(false);
        }
    }

    public float Manager_Area()
    {
        area_Circle = player_Oj_List.Count * player_Oj_List[0].GetComponent<Player_Manager>().Area_();
        return Mathf.Sqrt(area_Circle/Mathf.PI);
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
