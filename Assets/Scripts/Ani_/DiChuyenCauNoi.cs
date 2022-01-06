using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiChuyenCauNoi : MonoBehaviour
{
    public float max = 2.5f;
    public float min = -2.5f;
    public float speed = 0.03f;
    bool timeCheck;
    // Start is called before the first frame update

    private void OnEnable()
    {
        speed = 0.03f;
        timeCheck = false;
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
        transform.Translate(new Vector3(speed, 0f, 0f));
        if (!timeCheck)
        {
            if (transform.position.x > max)
            {
                speed = -speed;
                StartCoroutine(Delay_Check());
            }
            if (transform.position.x < min)
            {
                speed = -speed;
                StartCoroutine(Delay_Check());
            }
        }
    }

    IEnumerator Delay_Check()
    {
        timeCheck = true;
        yield return new WaitForSeconds(0.5f);
        timeCheck = false;
    }
}
