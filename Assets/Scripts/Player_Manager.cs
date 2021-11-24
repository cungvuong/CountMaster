using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Manager : MonoBehaviour
{
    public static Player_Manager instance;
    public float moveSpeed = 5f;
    private Rigidbody rb;
    public GameObject center_Point;
    public GameObject boss;
    Vector3 mLookDirection;
    private bool fall_ = false;
    [Range(1,2f)]
    private float fall_Force = 5f;
    //private bool touch_Brige = false;
    [HideInInspector]
    public bool boss_Attack;
    public GameObject blood;
    public bool time_Center;
    //private bool check_Ground;
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
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        center_Point = GameObject.FindGameObjectWithTag("center");
        boss = GameObject.FindGameObjectWithTag("boss");
    }

    private void Update()
    {
        if(boss_Attack)
            Attack_Boss();
        else
            Movement();
    }
    
    void Movement()
    {
        mLookDirection = (center_Point.transform.position - transform.position).normalized;
        //dis_Center = (new Vector3(transform.position.x, 0f, transform.position.z) - new Vector3(center_Point.transform.position.x, 0f, center_Point.transform.position.z)).magnitude;
        if (time_Center)
        {
            rb.AddForce(mLookDirection * 60f, ForceMode.Force);
        }

        if (fall_)
        {
            transform.Translate(Vector3.up * -moveSpeed/fall_Force * Time.deltaTime);
        }
        //if (!touch_Brige)
        //{
        //    transform.position = new Vector3(transform.position.x, 1.70f, transform.position.z);
        //}
    }

    void Attack_Boss()
    {
        if (Boss_Manager.instance.alive)
        {
            Vector3 targetPos = new Vector3(boss.transform.position.x, transform.position.y, boss.transform.position.z);
            transform.LookAt(targetPos);
            transform.Translate(Vector3.forward * 2f * Time.deltaTime);
            GetComponent<Animator>().Play("Attack");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Box_Trap")
        {
            Player.instance.player_Oj_List.Remove(gameObject);
            Instantiate(blood, transform.position + new Vector3(0f, 0.5f, 0f), Quaternion.identity);
            Instantiate(blood, transform.position + new Vector3(0f, 0.1f, 0f), Quaternion.Euler(90f, 0f, 0f));
            Player.instance.Stop_Corotine_Addforce();
            Player.instance.Start_Addforce();
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "boss")
        {
            Player.instance.player_Oj_List.Remove(gameObject);
            Destroy(gameObject, 0.25f);
        }
        if (collision.gameObject.tag == "ground")
        {
            fall_ = false;
        }
        if(collision.gameObject.tag == "slide")
        {
            //touch_Brige = true;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "player")
        {
            //move_ = false;
        }
        if (collision.gameObject.tag == "slide")
        {
            fall_ = false;
        }
    }


    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "slide")
        {
            fall_ = true;
            //touch_Brige = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "boss_attack")
        {
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
            foreach(GameObject x in Player.instance.player_Oj_List)
            {
                x.transform.parent = null;
                x.transform.GetComponent<Player_Manager>().boss_Attack = true;
            }
        }
    }

    public float Area_()
    {
        return Mathf.PI * Mathf.Pow(GetComponent<CapsuleCollider>().radius,2);
    }

}
