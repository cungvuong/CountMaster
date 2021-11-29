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
    private bool fly = false;
    [Range(0,3f)]
    private float fall_Force = 1f;
    //private bool touch_Brige = false;
    [HideInInspector]
    public bool boss_Attack;
    [HideInInspector]
    public bool tru_Attack;
    public GameObject blood;
    public bool time_Center;
    public GameObject tru;
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
        tru = GameObject.FindGameObjectWithTag("tru");
    }

    private void Update()
    {
        if (boss_Attack || tru_Attack)
        {
            if (boss_Attack)
            {
                Attack_Boss(boss);
            }
            if (tru_Attack)
            {
                Attack_Boss(tru);
            }
        }
        else
            Movement();
    }
    
    void Movement()
    {
        mLookDirection = (center_Point.transform.position - transform.position).normalized;
        if (time_Center)  // time_Center
        {
            rb.AddForce(mLookDirection * 60f, ForceMode.Force);
        }
        if (fall_) // nhan vat ha canh'
        {
            transform.Translate(Vector3.up * -moveSpeed * 1.5f * Time.deltaTime);
        }
        if (fly) // nv bay len 
        {
            transform.Translate(Vector3.up * moveSpeed * 1.2f * Time.deltaTime);
        }
        //else
        //{
        //    transform.Translate(Vector3.up * -moveSpeed * 1.5f * Time.deltaTime);
        //}
        //
        Attack_Tru();
        if (boss.GetComponent<Boss>().alive) // khi boss con song
        {
            if (Mathf.Abs((boss.transform.position - transform.position).magnitude) <= 4.5f){
                Time_Attack_Boss();
            }
        }
        else
        {
            // xu ly khi danh xong 1 boss
            if (boss.GetComponent<Boss>().gameObject.name != "Final_Boss")
            {
                Player.instance.can_Move = true;
                GetComponent<Animator>().Play("run");
            }
        }
    }

    void Attack_Boss(GameObject enemy)
    {
        if (enemy.GetComponent<Boss>().alive) // neu boss o gan con song
        {
            Vector3 targetPos = new Vector3(enemy.transform.position.x, transform.position.y, enemy.transform.position.z);
            transform.LookAt(targetPos);
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(enemy.transform.position.x, transform.position.y, enemy.transform.position.z), 0.1f);
            GetComponent<Animator>().Play("Attack");
        }
        else
        {
            transform.rotation = Quaternion.identity;
            boss_Attack = false;
            tru_Attack = false;
            Player.instance.can_Move = true;
            GetComponent<Animator>().Play("run");
        }
    }

    void Attack_Tru()
    {
        if(tru != null)
        if (Mathf.Abs((tru.transform.position - transform.position).magnitude) <= 4.5f)
        {
            if (tru.GetComponent<Boss>().alive)
                Time_Attack_Tru();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Box_Trap")
        {
            Player.instance.player_Oj_List.Remove(this.gameObject);
            Die();
        }
        if (collision.gameObject.tag == "boss")
        {
            Player.instance.player_Oj_List.Remove(this.gameObject);
            Die();
            if(Player.instance.player_Oj_List.Count == 0) // end game
                Player.instance.End_Game();
        }
        if (collision.gameObject.tag == "tru")
        {
            Player.instance.player_Oj_List.Remove(this.gameObject);
            Die();
            if (Player.instance.player_Oj_List.Count == 0) // end game
                Player.instance.End_Game();
            Debug.Log("cham tru");
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

    public void Die()
    {
        Player.instance.mount_Player.text = Player.instance.player_Oj_List.Count.ToString();
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
            StartCoroutine(Time_Exit_Slide());
        }
    }

    public void Time_Attack_Boss()
    {
        Player.instance.can_Move = false;
        foreach (GameObject x in Player.instance.player_Oj_List)
        {
            x.transform.GetComponent<Player_Manager>().boss_Attack = true;
        }
    }

    //void Exit_Slide()
    //{
    //    for(int i=0; i<10; i++) {
    //        if(i>4)
    //            transform.Translate(Vector3.up * -moveSpeed * Time.deltaTime);
    //        else
    //        {
    //        }
    //        //transform.position = new Vector3(transform.position.x, transform.position.y + i/10f, transform.position.z);
    //    }
    //}

    IEnumerator Time_Exit_Slide()
    {
        fly = true;
        yield return new WaitForSeconds(0.2f);
        fall_ = true;
        fly = false;
        Player.instance.Stop_Corotine_Addforce(); // dung corotin trc
        Player.instance.Start_Addforce();
    }

    public void Time_Attack_Tru()
    {
        Player.instance.can_Move = false;
        foreach (GameObject x in Player.instance.player_Oj_List)
        {
            x.transform.GetComponent<Player_Manager>().tru_Attack = true;
        }
    }
}
