using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Touch_Player : MonoBehaviour
{
    public int clone_X;
    public bool cal;
    public GameObject player;
    //public GameObject cam;
    public GameObject player_Spawn;
    public GameObject box_Left;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "player")
        {
            int mount_Clone = Player.instance.player_Oj_List.Count * (clone_X-1);
            // neu tra loi dung
            if (GetComponentInChildren<TextMeshPro>().text == transform.parent.GetComponent<Range_Ques>().kq.ToString()) 
            {
                Debug.Log("win");
                if (cal)
                {
                    for (int i = 0; i < Player.instance.player_Oj_List.Count; i++)
                    {
                        Player.instance.player_Oj_List[i].GetComponent<Rigidbody>().drag = 1f;
                    }
                    for (int i = 0; i < mount_Clone; i++)
                    {
                        float posx = Random.Range(-1f, 1f);
                        float posz = Random.Range(-1f, 1f);
                        GameObject x = Instantiate(player, player_Spawn.transform.position + new Vector3(posx, 0f, posz), Quaternion.identity, player_Spawn.transform.parent);
                        Player.instance.player_Oj_List.Add(x);
                    }
                }
                else
                {
                    for (int i = 0; i < clone_X; i++)
                    {
                        float posx = Random.Range(-1f, 1f);
                        float posz = Random.Range(-1f, 1f);
                        GameObject x = Instantiate(player, player_Spawn.transform.position + new Vector3(posx, 0f, posz), Quaternion.identity, player_Spawn.transform.parent);
                        Player.instance.player_Oj_List.Add(x);
                    }
                }
                box_Left.GetComponent<BoxCollider>().enabled = false;
                for (int i = 0; i < Player.instance.player_Oj_List.Count; i++)
                {
                    Player.instance.player_Oj_List[i].GetComponent<Rigidbody>().drag = 10f;
                }
                Player.instance.Stop_Corotine_Addforce(); // dung corotin trc
                Player.instance.Start_Addforce(); // chay corotine moi
            }
            else
            {
                // xu ly sai ket qua
                box_Left.GetComponent<BoxCollider>().enabled = false;
                int dem = 10;
                for(int i=Player.instance.player_Oj_List.Count-1; i >=0; i--)
                {
                    dem--;
                    Player.instance.player_Oj_List[i].GetComponent<Player_Manager>().Die();
                    Player.instance.player_Oj_List.RemoveAt(i);
                    if (dem == 0)
                    {
                        break;
                    }
                }
            }
        }
        Player.instance.mount_Player.text = Player.instance.player_Oj_List.Count.ToString();
        gameObject.GetComponent<BoxCollider>().enabled = false;
        gameObject.SetActive(false);
    }
}
