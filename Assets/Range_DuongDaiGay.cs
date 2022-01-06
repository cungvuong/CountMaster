using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range_DuongDaiGay : MonoBehaviour
{
    public GameObject Water;
    public GameObject Lava;
    public GameObject Ho_Sau;
    List<GameObject> list_Trap_ = new List<GameObject>();
    List<GameObject> list_Trap_Play;
    // Start is called before the first frame update
    Vector3[] list_Pos = { new Vector3(-3.13f, 0f, -15.06f), new Vector3(-3.13f, 0f, 35f), new Vector3(3.12f, 0f, 15f), new Vector3(3.12f, 0f, -35f)};

    private void Awake()
    {
        Range_Start_CauGay();
    }

    void Range_Start_CauGay()
    {
        GameObject x1 = Instantiate(Water, transform);
        list_Trap_.Add(x1); x1.SetActive(false);
        GameObject x2 = Instantiate(Lava, transform);
        list_Trap_.Add(x2); x2.SetActive(false);
        for (int i = 0; i < 2; i++)
        {
            GameObject x3 = Instantiate(Ho_Sau, transform);
            list_Trap_.Add(x3);
            x3.SetActive(false);
        }
    }

    private void OnEnable()
    {
        list_Trap_Play = list_Trap_;
        for (int i=0; i<4; i++)
        {
            int ran = Random.Range(0, list_Trap_Play.Count);
            Range_Trap(ran, i);
        }
    }

    void Range_Trap(int x, int dem)
    {
        list_Trap_Play[x].transform.position = list_Pos[dem];
        list_Trap_Play[x].SetActive(true);
        list_Trap_Play.Remove(list_Trap_Play[x]);
    }
}
