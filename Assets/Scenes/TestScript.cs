using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public GameObject sphere;

    private void Start()
    {
        float khoangcachx = 1f;
        float khoangcachy = 0.8f;
        int dotang = 10;
        int demvong=0;
        for (int i=1; i<=dotang; i++)
        {
            Vector3 pos = sphere.transform.position;
            GameObject x = Instantiate(sphere, null);
            float angle = i * (2 * 3.14159f / (dotang));
            float x_ = Mathf.Cos(angle) * (khoangcachx);
            float y_ = Mathf.Sin(angle) * (khoangcachy);
            //
            x.transform.position = pos + new Vector3(x_, 0f, y_);
            if (i == dotang)
            {
                dotang += 10;
                khoangcachx += 0.5f;
                khoangcachy += 0.5f;
                i = 0;
                demvong++;
            }
            if (demvong == 8)
            {
                break;
            }
        }
    }
}
