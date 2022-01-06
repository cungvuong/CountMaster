using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cuc_Gai_Lan : MonoBehaviour
{
    public float speed = 8f;
    public float kcmax = 4.5f;
    public float kcmin = -4.5f;
    bool check_Change;
    // Update is called once per frame
    private void OnEnable()
    {
        speed = 8f;
        check_Change = false;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    void Update()
    {
        Movement();
    }

    void Movement()
    {
        transform.parent.Translate(Vector3.right * speed * Time.deltaTime);
        if (!check_Change)
        {
            if (transform.parent.position.x > kcmax)
            {
                speed = -speed;
                StartCoroutine(Time_Check());
            }
            if(transform.parent.position.x < kcmin)
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
        yield return new WaitForSeconds(0.5f);
        check_Change = false;
    }
}
