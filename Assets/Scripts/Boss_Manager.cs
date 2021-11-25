using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Manager : Boss
{
    public GameObject win_Menu;

    private void Start()
    {
        this.health = 20;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "player")
        {
            if (this.health <= 0)
            {
                win_Menu.SetActive(true);
                gameObject.SetActive(false);
            }
            this.health--;
        }
    }
}
