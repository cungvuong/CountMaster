using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Boss_Enemy : Boss
{
    public TextMeshPro health_Text;
    
    private void Start()
    {
        this.alive = true;
        this.health = 20;
        health_Text.text = health.ToString();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "player")
        {
            health--;
            transform.position += new Vector3(0f, -0.1875f, 0f);
            if (health <= 0)
            {
                this.alive = false;
                gameObject.SetActive(false);
            }
            health_Text.text = this.health.ToString();
        }
    }
}
