using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Check_Button : MonoBehaviour
{
    public GameObject player;
    public GameObject parent;
    public GameObject player_Spawn;
    public GameObject Ques_Menu;
    //private List<GameObject> list_Stick;

    public void Check_KQ(Text kqText)
    {
        if (int.Parse(kqText.text.ToString()) == int.Parse(Range_Ques.kq.ToString()))
        {
            for(int i=0; i< Player.instance.player_Oj_List.Count; i++)
            {
                Player.instance.player_Oj_List[i].GetComponent<Rigidbody>().drag = 1f;
            }
            for(int i=0; i<10; i++)
            {
                float posx = Random.Range(-1f, 1f);
                float posz = Random.Range(-1f, 1f);
                GameObject x = Instantiate(player, player_Spawn.transform.position + new Vector3(posx, 0f, posz), Quaternion.identity, parent.transform);
                Player.instance.player_Oj_List.Add(x);
            }
            Player.instance.Stop_Corotine_Addforce();
            Player.instance.Start_Addforce();
        }
        else
        {
            // xu ly tra loi sai
        }
        for (int i = 0; i < Player.instance.player_Oj_List.Count; i++)
        {
            Player.instance.player_Oj_List[i].GetComponent<Rigidbody>().drag = 10f;
        }
        Player.instance.mount_Player.text = Player.instance.player_Oj_List.Count.ToString();
        Ques_Menu.SetActive(false);
    }
}
