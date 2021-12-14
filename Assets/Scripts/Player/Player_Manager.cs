using System.Collections;
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
    //private bool touch_Brige = false;
    [HideInInspector]
    public bool boss_Attack;
    //[HideInInspector]
    public bool tru_Attack = false;
    public GameObject blood;
    public bool time_Center = true;
    public bool center_Done;
    public GameObject[] tru;
    GameObject curr_Tru;
    private void Awake()
    {
        if (instance == null)
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
        Start_Player_();
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

    public void Start_Player_()
    {
        tru_Attack = false;
        boss_Attack = false;
        rb = GetComponent<Rigidbody>();
        center_Point = GameObject.FindGameObjectWithTag("center");
        boss = GameObject.FindGameObjectWithTag("boss");
        tru = GameObject.FindGameObjectsWithTag("tru");
    }

    void Movement()
    {
        mLookDirection = (center_Point.transform.position - transform.position).normalized;
        //if (Mathf.Abs((center_Point.transform.position - transform.position).magnitude)<=0.01f)
        //{
        //    center_Done = true;
        //}
        //if (time_Center)  // time_Center
        //{
        //    if(!fall_ && !fly && !center_Done)
        //    {
        //        rb.AddForce(mLookDirection * (40f), ForceMode.Force);
        //        //transform.position = Vector3.MoveTowards(transform.position, center_Point.transform.position, Time.deltaTime);
        //    }
        //}
        if (fall_) // nhan vat ha canh'
        {
            transform.Translate(Vector3.up * -moveSpeed * 1.2f * Time.deltaTime);
        }
        if (fly) // nv bay len 
        {
            transform.Translate(Vector3.up * moveSpeed * 1.2f * Time.deltaTime);
            //Debug.Log("fly");
        }
        Attack_Tru();
        if (boss.GetComponent<Boss_Manager>().alive) // khi boss con song
        {
            if (Mathf.Abs((boss.transform.position - center_Point.transform.position).magnitude) <= 10f)
            {
                boss_Attack = true;
            }
        }
        else
        {
            if (boss.GetComponent<Boss>().gameObject.name != "Final_Boss")
            {
                Player.instance.can_Move = true;
                transform.gameObject.GetComponentInChildren<Animator>().Play("run");
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
                    transform.position = Vector3.MoveTowards(new Vector3(transform.position.x, 0.25f, transform.position.z), new Vector3(enemy.transform.position.x, 1f, enemy.transform.position.z), 0.1f);
                    if (Mathf.Abs((enemy.transform.position - transform.position).magnitude) <= 3f)
                    {
                        transform.gameObject.GetComponentInChildren<Animator>().Play("Attack");
                    }
                }
                else
                {
                    transform.rotation = Quaternion.identity;
                    boss_Attack = false;
                    tru_Attack = false;
                    Player.instance.can_Move = true;
                    transform.gameObject.GetComponentInChildren<Animator>().Play("run");
                }
            }
            else if (enemy.tag == "tru")
            {
                if (enemy.GetComponent<Boss_Enemy>().alive)
                {
                    Vector3 targetPos = new Vector3(enemy.transform.position.x, transform.position.y, enemy.transform.position.z);
                    transform.LookAt(targetPos);
                    transform.position = Vector3.MoveTowards(new Vector3(transform.position.x, 0.25f, transform.position.z), new Vector3(enemy.transform.position.x, 1f, enemy.transform.position.z), 0.1f);
                    if (Mathf.Abs((enemy.transform.position - transform.position).magnitude) <= 3f)
                    {
                        transform.gameObject.GetComponentInChildren<Animator>().Play("Attack");
                    }
                }
                else
                {
                    transform.rotation = Quaternion.identity;
                    boss_Attack = false;
                    tru_Attack = false;
                    Player.instance.can_Move = true;
                    transform.gameObject.GetComponentInChildren<Animator>().Play("run");
                }
            }

        }
    }

    void Attack_Tru()
    {
        if (tru.Length != 0)
        {
            foreach (GameObject x in tru)
            {
                if (x.gameObject.GetComponent<Boss_Enemy>().alive)
                    if (Mathf.Abs((x.transform.position - center_Point.transform.position).magnitude) <= 8f)
                    {
                        curr_Tru = x;
                        tru_Attack = true;
                        //Player.instance.can_Move = false;
                    }
                    else { }
                else
                {
                    tru_Attack = false;
                    Player.instance.can_Move = true;
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Box_Trap")
        {
            Die();
        }
        if (collision.gameObject.tag == "die_zone")
        {
            Die();
        }
        if (collision.gameObject.tag == "boss")
        {
            Player.instance.player_Oj_List.Remove(this.gameObject);
            Die_When_Attack_Boss();
            if (Player.instance.player_Oj_List.Count == 0) // end game
                Player.instance.End_Game();
        }
        if (collision.gameObject.tag == "tru")
        {
            Die();
            if (Player.instance.player_Oj_List.Count == 0) // end game
                Player.instance.End_Game();
        }

        if (collision.gameObject.tag == "ground")
        {
            this.fall_ = false;
        }
        //if (collision.collider.tag == "slide")
        //{
        //    foreach(GameObject x in Pooling_Player.instance.list_Obj)
        //    {
        //        x.GetComponent<Player_Manager>().time_Center = false;
        //    }
        //}
    }

    //private void OnCollisionStay(Collision collision)
    //{
    //    if(collision.gameObject.tag == "player")
    //    {
    //        if (!collision.gameObject.GetComponent<Player_Manager>().center_Done)
    //        {
    //            this.center_Done = false;
    //            Debug.Log("center_Done");
    //        }
    //        else
    //        {
    //            this.center_Done = true;
    //        }
    //    }
    //}


    IEnumerator Time_Delay()
    {
        time_Center = false;
        yield return new WaitForSeconds(1f);
        foreach (GameObject x in Pooling_Player.instance.list_Obj)
        {
            x.GetComponent<Player_Manager>().time_Center = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("exit_slide"))
        {
            StartCoroutine(Time_Exit_Slide());
        }
    }

    public void Die()
    {
        Player.instance.player_Oj_List.Remove(this.gameObject);
        Player.instance.mount_Player.text = Player.instance.player_Oj_List.Count.ToString();
        //Instantiate(blood, transform.position + new Vector3(0f, 0.1f, 0f), Quaternion.Euler(90f, 0f, 0f));
        GameObject x = Blood_Destroy.instance.Spawn_();
        if (x != null)
        {
            x.transform.position = transform.position + new Vector3(0f, 0.1f, 0f);
            x.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
            x.SetActive(true);
        }
        gameObject.SetActive(false);
    }

    void Die_When_Attack_Boss()
    {
        Player.instance.mount_Player.text = Player.instance.player_Oj_List.Count.ToString();

        StartCoroutine(Start_Die_Ani());
    }

    IEnumerator Time_Exit_Slide()
    {
        this.fly = true;
        if (gameObject.activeSelf)
        {
            yield return new WaitForSeconds(0.2f);
            //if (gameObject.activeSelf)
            //{

            //}
            this.fall_ = true;
            this.fly = false;
        }
    }

    IEnumerator Start_Die_Ani()
    {
        gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }
    private void OnEnable()
    {
        Start_Player_();
        fly = false;
        fall_ = false;
    }
}
