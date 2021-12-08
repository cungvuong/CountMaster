using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cuc_Gai_Lan : MonoBehaviour
{
    public float speed = 10f;
    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        transform.parent.Translate(Vector3.right * speed * Time.deltaTime);
        if (transform.position.x > 4.5f)
        {
            speed = -speed;
        }
        if(transform.position.x < -4.5f)
        {
            speed = -speed;
        }
        transform.RotateAround(transform.position, Vector3.forward, -speed);
    }
}
