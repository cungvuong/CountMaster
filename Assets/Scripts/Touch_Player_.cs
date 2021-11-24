using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touch_Player_ : MonoBehaviour
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
            Player.instance.Stop_Corotine_Addforce();
            Player.instance.Start_Addforce();
        }
        Player.instance.mount_Player.text = Player.instance.player_Oj_List.Count.ToString();
        gameObject.GetComponent<BoxCollider>().enabled = false;
    }
}
