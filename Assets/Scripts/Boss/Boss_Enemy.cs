using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Boss_Enemy : Boss
{
    public TextMeshPro health_Text;
    GameObject center_Point;
    Vector3 pos_Default = new Vector3();
    private void Awake()
    {
        pos_Default = transform.position;
    }

    private void OnEnable()
    {
        this.alive = true;
        this.health = 20;
        health_Text.text = health.ToString();
        center_Point = GameObject.FindGameObjectWithTag("center");
        transform.position = new Vector3(transform.position.x, pos_Default.y, transform.position.z);
    }
    private void Update()
    {
        Check_Player_Attack();
    }

    void Check_Player_Attack()
    {
        if (Mathf.Abs((center_Point.transform.position - transform.position).magnitude) <= 8f)
        {
            Player.instance.tru = this.gameObject;
            Player.instance.TimeMoveToTru = true;
            Player.instance.player_Oj_List.ForEach(x =>
            {
                x.GetComponent<Player_Manager>().Attack_Tru(this.gameObject);
            });
        }
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
