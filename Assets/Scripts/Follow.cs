using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public GameObject target;
    //public GameObject point;
    //// Update is called once per frame
    [Range(0f,5f)]
    public float z_Cam;
    void Update()
    {
    }

    private void LateUpdate()
    {
        if (!Player_Manager.instance.boss_Attack)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z-z_Cam), Time.deltaTime+0.001f);
        }
    }
}
