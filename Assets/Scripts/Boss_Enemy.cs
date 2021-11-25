using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Enemy : Boss
{
    private void Start()
    {
        this.health = 10;    
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "player")
        {
            if (health <= 0)
            {
                this.alive = false;
                gameObject.SetActive(false);
            }
            health--;
        }
    }
}
