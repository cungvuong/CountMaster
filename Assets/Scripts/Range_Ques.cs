using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Range_Ques : MonoBehaviour
{
    public GameObject Ques_Menu;

    public Text So1;
    public Text So2;
    public Text Cal;
    
    public Text Kq1;
    public Text Kq2;
    public Text Kq3;
    public static int kq = 0;

    private void Update()
    {
        if (Player_Manager.instance.boss_Attack)
        {
            if (Ques_Menu.activeInHierarchy)
            {
                Ques_Menu.SetActive(false);
            }
        }    
    }

    private void OnTriggerEnter(Collider other)
    {
        Ques_Menu.SetActive(true);
        Range_Cal();
        gameObject.GetComponent<BoxCollider>().enabled = false;
    }

    void Range_Cal()
    {
        int x = Random.Range(1, 4);
        So1.text = Random.Range(1, 10).ToString();
        So2.text = Random.Range(1, 10).ToString();
        switch (x)
        {
            case 1: Cal.text = "+";
                kq = int.Parse(So1.text.ToString()) + int.Parse(So2.text.ToString());
                break;
            case 2: Cal.text = "-"; 
                kq = int.Parse(So1.text.ToString()) - int.Parse(So2.text.ToString()); ;
                break;
            case 3: Cal.text = "x"; 
                kq = int.Parse(So1.text.ToString()) * int.Parse(So2.text.ToString()); ;
                break;
        }
        //Debug.Log("kq" + kq.ToString());
        int ranKQ = Random.Range(1, 4);
        switch (ranKQ) {
            case 1: 
                Kq1.text = kq.ToString();
                Kq2.text = (kq+2).ToString();
                Kq3.text = (kq-2).ToString(); break;
            case 2:
                Kq3.text = kq.ToString();
                Kq2.text = (kq + 2).ToString();
                Kq1.text = (kq - 2).ToString(); break;
            case 3:
                Kq2.text = kq.ToString();
                Kq1.text = (kq + 2).ToString();
                Kq3.text = (kq - 2).ToString(); break;
        }
    }

    public void Reset()
    {
        SceneManager.LoadScene(0);
    }
}
