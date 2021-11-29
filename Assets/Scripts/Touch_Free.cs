using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Touch_Free : MonoBehaviour
{
    int clone_Free_X;
    public bool cal_Free;
    public GameObject player_Free;
    //public GameObject cam;
    public GameObject player_Spawn_;
    public GameObject box_Left_;
    public TextMeshPro text_Mesh_Clone;
    // Start is called before the first frame update

    private void Start()
    {
        if (cal_Free)
        {
            clone_Free_X = Random.Range(2, 5);
            text_Mesh_Clone.text = "x" + clone_Free_X.ToString();
        }
        else
        {
            clone_Free_X = 5 * Random.Range(2, 5); // range so clone
            text_Mesh_Clone.text = "+" + clone_Free_X.ToString();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "player")
        {
            int mount_Clone = Player.instance.player_Oj_List.Count * (clone_Free_X - 1);
            // neu tra loi dung
                if (cal_Free)
                {
                    for (int i = 0; i < Player.instance.player_Oj_List.Count; i++)
                    {
                        Player.instance.player_Oj_List[i].GetComponent<Rigidbody>().drag = 1f;
                    }
                    for (int i = 0; i < mount_Clone; i++)
                    {
                        float posx = Random.Range(-1f, 1f);
                        float posz = Random.Range(-1f, 1f);
                        GameObject x = Instantiate(player_Free, player_Spawn_.transform.position + new Vector3(posx, 0f, posz), Quaternion.identity, player_Spawn_.transform.parent);
                        Player.instance.player_Oj_List.Add(x);
                    }
                }
                else
                {
                    for (int i = 0; i < clone_Free_X; i++)
                    {
                        float posx = Random.Range(-1f, 1f);
                        float posz = Random.Range(-1f, 1f);
                        GameObject x = Instantiate(player_Free, player_Spawn_.transform.position + new Vector3(posx, 0f, posz), Quaternion.identity, player_Spawn_.transform.parent);
                        Player.instance.player_Oj_List.Add(x);
                    }
                }
                box_Left_.GetComponent<BoxCollider>().enabled = false;
                for (int i = 0; i < Player.instance.player_Oj_List.Count; i++)
                {
                    Player.instance.player_Oj_List[i].GetComponent<Rigidbody>().drag = 10f;
                }
                Player.instance.Stop_Corotine_Addforce(); // dung corotin trc
                Player.instance.Start_Addforce(); // chay corotine moi
            
        }
        Player.instance.mount_Player.text = Player.instance.player_Oj_List.Count.ToString();
        gameObject.GetComponent<BoxCollider>().enabled = false;
        gameObject.SetActive(false);
    }
}
