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
    int mount_Du = 0;

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
                if ((clone_X + Player.instance.player_Oj_List.Count) >= 210)
                {
                    mount_Du = (clone_X + Player.instance.player_Oj_List.Count) - 210;
                    clone_X = 210 - Player.instance.player_Oj_List.Count;
                    Pooling_Player.instance.sodu += mount_Du;
                }
                for (int i = 0; i < clone_X; i++)
                {

                    GameObject x = Pooling_Player.instance.GetPooledObject();
                    if (x != null)
                    {
                        Player.instance.player_Oj_List.Add(x);
                        //x.transform.position = player_Spawn.transform.position + new Vector3(posx, 0f, posz);
                        x.transform.localPosition = Pooling_Player.instance.Get_Pos(Player.instance.player_Oj_List.Count);
                        x.transform.rotation = Quaternion.identity;
                        x.SetActive(true);
                    }
                }
                if (Pooling_Player.instance.sodu > 210)
                    Player.instance.mount_Player.text = Pooling_Player.instance.sodu.ToString();
                if (Player.instance.player_Oj_List.Count < 210)
                {
                    Pooling_Player.instance.UpDate_Spawn_Player(); // tinh lai so du
                    mount_Du = 0;
                    Player.instance.mount_Player.text = Player.instance.player_Oj_List.Count.ToString();
                }
                AddForce_All_Player();
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
                AddForce_All_Player();
            }
        }
        gameObject.GetComponent<BoxCollider>().enabled = false;
        gameObject.SetActive(false);
    }
    void AddForce_All_Player()
    {
        for(int i = 0; i < Player.instance.player_Oj_List.Count; i++)
        {
            Player.instance.player_Oj_List[i].GetComponent<Player_Manager>().Corrotine_Center_Force(0.25f);
        }
    }
}
