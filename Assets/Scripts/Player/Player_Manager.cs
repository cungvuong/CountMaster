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
    private bool real_Exit_Slide = false;
    //private bool touch_Brige = false;
    [HideInInspector]
    public bool boss_Attack;
    //[HideInInspector]
    public bool tru_Attack=false;
    public GameObject blood;
    public bool time_Center;
    public GameObject[] tru;
    GameObject curr_Tru;
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
        tru_Attack = false;
        boss_Attack = false;
        rb = GetComponent<Rigidbody>();
        center_Point = GameObject.FindGameObjectWithTag("center");
        boss = GameObject.FindGameObjectWithTag("boss");
        tru = GameObject.FindGameObjectsWithTag("tru");
        //if(tru.Length!=0)
        //    curr_Tru = tru[0];
    }

    private void Update()
    {
        if (boss_Attack)
        {
            Attack_Boss(boss);
        }
        else if (tru_Attack)
        {
            Attack_Boss(curr_Tru);
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
            transform.Translate(Vector3.up * -moveSpeed * 1.2f * Time.deltaTime);
        }
        if (fly) // nv bay len 
        {
            transform.Translate(Vector3.up * moveSpeed * 1.3f * Time.deltaTime);
            transform.Translate(Vector3.forward * moveSpeed * 1.5f * Time.deltaTime);
        }
        Attack_Tru();
        if (boss.GetComponent<Boss_Manager>().alive) // khi boss con song
        {
            if (Mathf.Abs((boss.transform.position - center_Point.transform.position).magnitude) <= 10f){
                boss_Attack = true;
            }
        }
        else
        {
            if (boss.GetComponent<Boss>().gameObject.name != "Final_Boss")
            {
                Player.instance.can_Move = true;
                GetComponent<Animator>().Play("run");
            }
        }
    }

    void Attack_Boss(GameObject enemy)
    {
        if (enemy != null)
        {
            if (enemy.tag == "boss")
            {
                if (enemy.GetComponent<Boss_Manager>().alive)
                {
                    Vector3 targetPos = new Vector3(enemy.transform.position.x, transform.position.y, enemy.transform.position.z);
                    transform.LookAt(targetPos);
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(enemy.transform.position.x, 1f, enemy.transform.position.z), 0.1f);
                    GetComponent<Animator>().Play("Attack");
                    Debug.Log("attack");
                }
                else
                {
                    transform.rotation = Quaternion.identity;
                    boss_Attack = false;
                    tru_Attack = false;
                    Player.instance.can_Move = true;
                    GetComponent<Animator>().Play("run");
                }
            }else if(enemy.tag == "tru")
            {
                if (enemy.GetComponent<Boss_Enemy>().alive)
                {
                    Vector3 targetPos = new Vector3(enemy.transform.position.x, transform.position.y, enemy.transform.position.z);
                    transform.LookAt(targetPos);
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(enemy.transform.position.x, 1f, enemy.transform.position.z), 0.1f);
                    GetComponent<Animator>().Play("Attack");
                    Debug.Log("attack");
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

        }
    }

    void Attack_Tru()
    {
        if(tru.Length != 0)
        {
            foreach(GameObject x in tru)
            {
                if (Mathf.Abs((x.transform.position - center_Point.transform.position).magnitude) <= 8f)
                {
                    Debug.Log("attack tru");
                    curr_Tru = x;
                    tru_Attack = true;
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Box_Trap")
        {
            Player.instance.player_Oj_List.Remove(this.gameObject);
            Die();
            Player.instance.Stop_Corotine_Addforce(); // dung corotin trc
            Player.instance.Start_Addforce();
        }
        if (collision.gameObject.tag == "die_zone")
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "exit_slide")
        {
            real_Exit_Slide = true;
        }
    }

    public void Die()
    {
        Player.instance.mount_Player.text = Player.instance.player_Oj_List.Count.ToString();
        //Instantiate(blood, transform.position + new Vector3(0f, 0.5f, 0f), Quaternion.identity);
        Instantiate(blood, transform.position + new Vector3(0f, 0.1f, 0f), Quaternion.Euler(90f, 0f, 0f));

        Stop_Corotine_Die();
        StartCoroutine(Delay_Center_Die());

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
            if (real_Exit_Slide)
            {
                StartCoroutine(Time_Exit_Slide());
                StartCoroutine(Time_Center_Exit_Slide());
            }
        }
    }

    IEnumerator Time_Exit_Slide()
    {
        fly = true;
        yield return new WaitForSeconds(0.2f);
        fall_ = true;
        fly = false;
    }

    IEnumerator Time_Center_Exit_Slide()
    {
        yield return new WaitForSeconds(1f);
        Player.instance.Stop_Corotine_Addforce(); // dung corotin trc
        Player.instance.Start_Addforce();
    }

    IEnumerator Delay_Center_Die() // sau khi chet se ep doi hinh
    {
        yield return new WaitForSeconds(1f);
        Player.instance.Stop_Corotine_Addforce();
        Player.instance.Start_Addforce();
    }

    void Stop_Corotine_Die()
    {
        Coroutine coroutine = StartCoroutine(Delay_Center_Die());
        StopCoroutine(coroutine);
    }
}
