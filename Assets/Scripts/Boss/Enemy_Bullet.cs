using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bullet : MonoBehaviour
{
    [HideInInspector]
    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 2f);
        target = Player.instance.player_Oj_List[Random.Range(0, Player.instance.player_Oj_List.Count)];
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        //if(target != null)
        if (!target.activeSelf)
        {
            target = Player.instance.player_Oj_List[Random.Range(0, Player.instance.player_Oj_List.Count)];
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, 0.2f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "player")
        {
            Destroy(gameObject);
        }
    }
}
