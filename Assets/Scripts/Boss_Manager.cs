using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Manager : MonoBehaviour
{
    public static Boss_Manager instance;
    int health = 30;
    [HideInInspector]
    public bool alive = true;
    public GameObject win_Menu;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            return;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "player")
        {
            if (health <= 0)
            {
                //Debug.Log("End!");
                alive = false;
                win_Menu.SetActive(true);
                Destroy(gameObject);
            }
            health--;
            //Debug.Log(health + " hp");
        }
    }
}
