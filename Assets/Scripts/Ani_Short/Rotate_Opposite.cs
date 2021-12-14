using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate_Opposite : MonoBehaviour
{
    int ran;
    // Start is called before the first frame update
    void Start()
    {
        ran = Random.Range(0, 2);
    }

    private void Update()
    {
        if (ran == 1)
        {
            Rotate_1();
        }
        else
        {
            Rotate_2();
        }
    }

    void Rotate_1()
    {
        transform.Rotate(Vector3.forward, 5f);
    }

    void Rotate_2()
    {
        transform.Rotate(Vector3.forward, -5f);
    }
}
