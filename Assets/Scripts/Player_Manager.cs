using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Manager : MonoBehaviour
{
    public static Player_Manager instance;
    public float moveSpeed = 5f;
    private Rigidbody rb;
    public GameObject center_Point;
    public GameObject[] boss;
    Vector3 mLookDirection;
    private bool fall_ = false;
    [Range(0,3f)]
    private float fall_Force = 1f;
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
        boss = GameObject.FindGameObjectsWithTag("boss");

        foreach(GameObject x in boss)
        {
            Debug.Log(x.name);
        }
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
        if (time_Center)
        {
            rb.AddForce(mLookDirection * 60f, ForceMode.Force);
        }

        if (fall_)
        {
            transform.Translate(Vector3.up * -moveSpeed/fall_Force * Time.deltaTime);
        }
        //
        if (boss[Check_Current_Boss()].GetComponent<Boss>().alive)
        {
            if (((boss[Check_Current_Boss()].transform.position - transform.position).magnitude) <= 4.5f){
                Time_Attack_Boss();
            }
        }
        else
        {
            // xu ly khi danh xong 1 boss
            Player.instance.can_Move = true;
            Debug.Log("Done boss 1");
        }
    }

    void Attack_Boss()
    {
        if (boss[Check_Current_Boss()].GetComponent<Boss>().alive) // neu boss o gan con song
        {
            Vector3 targetPos = new Vector3(boss[Check_Current_Boss()].transform.position.x, transform.position.y, boss[Check_Current_Boss()].transform.position.z);
            transform.LookAt(targetPos);
            transform.Translate(Vector3.forward * 2f * Time.deltaTime);
            GetComponent<Animator>().Play("Attack");
        }
        else
        {
            boss_Attack = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Box_Trap")
        {
            Player.instance.player_Oj_List.Remove(gameObject);
            Die();
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "boss")
        {
            Player.instance.player_Oj_List.Remove(gameObject);
            Die();
        }
        if (collision.gameObject.tag == "ground")
        {
            fall_ = false;
        }
        if(collision.gameObject.tag == "slide")
        {
            //touch_Brige = true;
        }
    } // check cham bay va cham boss

    public void Die()
    {
        Instantiate(blood, transform.position + new Vector3(0f, 0.5f, 0f), Quaternion.identity);
        Instantiate(blood, transform.position + new Vector3(0f, 0.1f, 0f), Quaternion.Euler(90f, 0f, 0f));
        Player.instance.Stop_Corotine_Addforce();
        Player.instance.Start_Addforce();
        Destroy(this.gameObject);
    }

    private void OnCollisionStay(Collision collision)  // check len cau
    {
        if (collision.gameObject.tag == "slide")
        {
            fall_ = false;
        }
    }


    private void OnCollisionExit(Collision collision) // check ra khoi cau
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
            Time_Attack_Boss();
        }
    } //khi dang danh boss
    public void Time_Attack_Boss()
    {
        foreach (GameObject x in Player.instance.player_Oj_List)
        {
            //x.transform.parent = null;
            x.transform.GetComponent<Player_Manager>().boss_Attack = true;
        }
    }

    public float Area_()
    {
        return Mathf.PI * Mathf.Pow(GetComponent<CapsuleCollider>().radius,2);
    }

    int Check_Current_Boss()
    {
        int id=0;
        if (boss[0].GetComponent<Boss>().alive)
        {
            float dis = Mathf.Abs((boss[0].gameObject.transform.position - center_Point.transform.position).magnitude);
            for (int i = 0; i < boss.Length; i++)
            {
                if (Mathf.Abs((boss[i].gameObject.transform.position - center_Point.transform.position).magnitude) < dis)
                {
                    dis = Mathf.Abs((boss[i].gameObject.transform.position - transform.position).magnitude); // lay khoang cach den con boss gan nhat
                    id = i;
                }
            }
        }
        else
        {
            return 0;
        }
        return id;
    }

}
