using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cuc_Gai_Lan : MonoBehaviour
{
    public float speed = 10f;
    bool check_Change;
    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        transform.parent.Translate(Vector3.right * speed * Time.deltaTime);
        if (!check_Change)
        {
            if (transform.position.x > 4.5f)
            {
                speed = -speed;
                StartCoroutine(Time_Check());
            }
            if(transform.position.x < -4.5f)
            {
                speed = -speed;
                StartCoroutine(Time_Check());
            }
        }
        transform.RotateAround(transform.position, Vector3.forward, -speed * 5f);
    }

    IEnumerator Time_Check()
    {
        check_Change = true;
        yield return new WaitForSeconds(2f);
        check_Change = false;
    }
}
