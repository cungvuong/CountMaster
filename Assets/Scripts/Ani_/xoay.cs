using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class xoay : MonoBehaviour
{
    float xoay_ran = 5f;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * xoay_ran);
    }
}
