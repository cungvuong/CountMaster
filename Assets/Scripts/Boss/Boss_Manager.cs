using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Boss_Manager : Boss
{
    public GameObject win_Menu;
    public TextMeshPro health_Text;
    public int Health;
    private void Start()
    {
        this.health = Health;
        health_Text.text = this.health.ToString();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "player")
        {
            if (this.health <= 0)
            {
                this.alive = false;
                win_Menu.SetActive(true);
                gameObject.SetActive(false);
                //Player.instance.speedZ = 0f;
                //return;
            }
            this.health--;
            health_Text.text = this.health.ToString();
        }
    }
}
