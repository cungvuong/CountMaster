using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood_Destroy : MonoBehaviour
{
    public static Blood_Destroy instance = null;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    public GameObject blood;
    List<GameObject> list = new List<GameObject>();

    private void Start()
    {
        for (int i=0; i<10; i++)
        {
            GameObject x = Instantiate(blood, transform);
            x.SetActive(false);
            list.Add(x);
        }
    }

    public GameObject Spawn_()
    {
        for(int i=0; i<list.Count; i++)
        {
            if (!list[i].activeInHierarchy)
            {
                StartCoroutine(Time_Appear(list[i]));
                return list[i];
            }
        }
        return null;
    }

    IEnumerator Time_Appear(GameObject x)
    {
        yield return new WaitForSeconds(2f);
        x.SetActive(false);
    }
}
