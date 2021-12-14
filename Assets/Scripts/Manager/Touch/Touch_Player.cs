using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Touch_Player : MonoBehaviour
{
    public static Touch_Player instance;
    int clone_X = 40;
    public bool cal;
    //public GameObject cam;
    public GameObject player_Spawn;
    public GameObject box_Left;
    // Start is called before the first frame update

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }    
    }

    private void Start()
    {
        player_Spawn = GameObject.FindGameObjectWithTag("point_spawn");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player"))
        {
            if (GetComponentInChildren<TextMeshPro>().text == transform.parent.GetComponent<Range_Ques>().kq.ToString()) // neu tra loi dung
            {
                for (int i = 0; i < clone_X; i++)
                {
                    Pooling_Player.instance.UpDate_Spawn_Player(); // moi lan sinh se set active 1 object moi

                    float posx = Random.Range(-1.5f, 1.5f);
                    float posz = Random.Range(-1.5f, 1.5f);
                    GameObject x = Pooling_Player.instance.GetPooledObject();
                    if (x != null)
                    {
                        x.transform.position = player_Spawn.transform.position + new Vector3(posx, 0f, posz);
                        x.SetActive(true);
                        Player.instance.player_Oj_List.Add(x);
                        Player.instance.mount_Player.text = Pooling_Player.instance.Mount_Player_Active().ToString();
                    }
                    else
                    {
                        Debug.Log("du");
                        Pooling_Player.instance.Manager_Du();
                    }
                }
                box_Left.GetComponent<BoxCollider>().enabled = false;
            }
            else
            {
                // xu ly sai ket qua
                box_Left.GetComponent<BoxCollider>().enabled = false;
                int dem = 10;
                for (int i = Player.instance.player_Oj_List.Count - 1; i >= 0; i--)
                {
                    dem--;
                    Player.instance.player_Oj_List[i].GetComponent<Player_Manager>().Die();
                    if (dem == 0)
                    {
                        break;
                    }
                }
            }
        }
        gameObject.GetComponent<BoxCollider>().enabled = false;
        gameObject.SetActive(false);
    }
}
