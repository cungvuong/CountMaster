using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Boss_Manager : Boss
{
    public GameObject win_Menu;
    public TextMeshPro health_Text;
    public Text earn_Gold;
    private GameObject cam;
    Transform pos_Cam_Start;
    GameObject center_Point;
    public int Health;

    private void Start()
    {
        Set_Data_Boss();
    }

    public void Set_Data_Boss()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        this.alive = true;
        this.health = Health;
        health_Text.text = this.health.ToString();
        pos_Cam_Start = cam.transform;
        transform.rotation = Quaternion.Euler(0f, 180f, 0f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "player")
        {
            this.health--;
            if (this.health < 0 && Player.instance.player_Oj_List.Count >0)
            {
                Manager_Win();
            }
            health_Text.text = this.health.ToString();
        }
    }

    void Manager_Win()
    {
        //
        this.alive = false;
        cam.transform.position = new Vector3(pos_Cam_Start.position.x, pos_Cam_Start.position.y, cam.transform.position.z); // lay vt x y ban dau
        cam.GetComponent<Follow>().Reset_Cam();  
      

        // lay goc quay ban dau
        Follow.instance.win_Boss_Game = true;
        // set data player
        List_Level list = Save_Data.Load(); // lay data hien tai
        list = list.Next_Level(); // set them 1 level
        Save_Data.Save(list); // save data.
        //
        int gold_Range = Random.Range(170, 220);
        list.Get_Gold(Random.Range(170, 220));
        Save_Data.Save(list);
        win_Menu.SetActive(true);
        earn_Gold.text = "+ " + gold_Range.ToString();
        transform.parent.gameObject.SetActive(false);
    }
}
