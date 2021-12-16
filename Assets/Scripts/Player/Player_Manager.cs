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
    public bool center_Done;
    public GameObject[] tru;
    GameObject curr_Tru;
    Coroutine corotinCenter;

    bool Time_Addforce = true;
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
        Corrotine_Center_Force(0.3f);
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
        tru = new GameObject[3];
        tru_Attack = false;
        boss_Attack = false;
        rb = GetComponent<Rigidbody>();
        center_Point = GameObject.FindGameObjectWithTag("center");
        boss = GameObject.FindGameObjectWithTag("boss");
        tru = GameObject.FindGameObjectsWithTag("tru");
    }

    void Movement()
    {
        if (fall_) // nhan vat ha canh'
        {
            transform.Translate(Vector3.up * -moveSpeed * 1.5f * Time.deltaTime);
        }
        if (fly) // nv bay len 
        {
            transform.Translate(Vector3.up * moveSpeed * 1.3f * Time.deltaTime);
        }
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
        if (Time_Addforce)
        {
            transform.position = Vector3.MoveTowards(transform.position, center_Point.transform.position, Time.deltaTime*2.1f);
        }
    }

    IEnumerator Center_Player_Addforce(float time)
    {
        Time_Addforce = true;
        yield return new WaitForSeconds(time);
        Time_Addforce = false;
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

    public void Attack_Tru(GameObject tru)
    {
        this.tru_Attack = true;
        curr_Tru = tru;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Box_Trap"))
        {
            Die();
        }
        if (collision.gameObject.tag == "die_zone")
        {
            Die();
        }
        if (collision.gameObject.tag == "boss")
        {
            Die();
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
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("exit_slide"))
        {
            StartCoroutine(Time_Exit_Slide());
        }
    }

    //private void OnCollisionStay(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("slide"))
    //    {
    //        this.StopCoroutine(corotinCenter);
    //    }
    //}

    public void Die()
    {
        if (Player.instance.time_Center)
        {
            Player.instance.Start_AddForce();
        }
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

    IEnumerator Time_Exit_Slide()
    {
        this.fly = true;
        if (gameObject.activeSelf)
        {
            yield return new WaitForSeconds(0.3f);
            this.fall_ = true;
            this.fly = false;
        }
    }

    public void Corrotine_Center_Force(float time)
    {
        this.StopCoroutine(corotinCenter);
        this.StartCoroutine(Center_Player_Addforce(time));
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
        corotinCenter = StartCoroutine(Center_Player_Addforce(0.3f));
        Corrotine_Center_Force(0.5f);
    }

}
