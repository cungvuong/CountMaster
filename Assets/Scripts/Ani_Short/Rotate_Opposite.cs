using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate_Opposite : MonoBehaviour
{
    int ran;
    bool check_Rotate;
    // Start is called before the first frame update
    void Start()
    {
        ran = Random.Range(0, 2);
        check_Rotate = true;
    }

    private void Update()
    {
        if (check_Rotate)
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
        if(transform.position.z < Player.instance.gameObject.transform.position.z - 10f)
        {
            check_Rotate = false;
        }
        else
        {
            check_Rotate = true;
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
