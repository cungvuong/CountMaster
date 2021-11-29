using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Shoot : MonoBehaviour
{
    public GameObject target;
    public GameObject bullet;
    public GameObject point_Shot;
    bool shot = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPos = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
        transform.LookAt(targetPos);
        Check_Bullet();
    }

    void Check_Bullet()
    {
        if(Player.instance.player_Oj_List.Count != 0)
            for (int i = 0; i < Player.instance.player_Oj_List.Count; i++)
            {
                if(Mathf.Abs((Player.instance.player_Oj_List[i].transform.position - transform.position).magnitude) < 15f)
                {
                    if (shot)
                    {
                        Instantiate(bullet, point_Shot.transform.position, Quaternion.identity);
                        StartCoroutine(Time_Delay_Shot());
                    }
                    shot = false;
                }
            }
    }

    IEnumerator Time_Delay_Shot()
    {
        yield return new WaitForSeconds(1.5f);
        shot = true;
    }
}
