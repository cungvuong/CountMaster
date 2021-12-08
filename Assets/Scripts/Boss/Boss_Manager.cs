using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Boss_Manager : Boss
{
    public GameObject win_Menu;
    public TextMeshPro health_Text;
    private GameObject cam;
    Transform pos_Cam_Start;
    public int Health;
    private void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        this.alive = true;
        this.health = Health;
        health_Text.text = this.health.ToString();
        pos_Cam_Start = cam.transform;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "player")
        {
            this.health--;
            if (this.health == 0)
            {
                Manager_Win();
            }
            health_Text.text = this.health.ToString();
        }
    }

    void Manager_Win()
    {
        this.alive = false;
        win_Menu.SetActive(true);
        cam.transform.position = new Vector3(pos_Cam_Start.position.x, pos_Cam_Start.position.y, cam.transform.position.z); // lay vt x y ban dau
        cam.transform.rotation = pos_Cam_Start.rotation; // lay goc quay ban dau
        // set data player
        List_Level list = Save_Data.Load();
        list.Get_Gold(Random.Range(170, 220));
        Save_Data.Save(list);
        gameObject.SetActive(false);
    }

}
