using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Range_Ques : MonoBehaviour
{
    int ran_Num_Cal = 5; // chon cau hoi cal 1 or cal 2
    public static Range_Ques instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public TextMeshPro ques_Cal;

    public TextMeshPro Kq1;
    public TextMeshPro Kq2;

    public int kq = 0;

    int kqs = 0;

    private void OnEnable()
    {
        Range_Cal();
    }

    void Range_Cal()
    {
        Touch_Player[] list_Clone = GetComponentsInChildren<Touch_Player>(true);
        for(int i=0; i<list_Clone.Length; i++)
        {
            list_Clone[i].gameObject.GetComponent<BoxCollider>().enabled = true;
            list_Clone[i].gameObject.SetActive(true);
        }
        int dau1 = Random.Range(1, 5); // phep tinh 1
        int dau2 = Random.Range(1, 5); // phep tinh 2
        int so1 = Random.Range(1, 7);
        int so2 = Random.Range(1, 7);
        int so3 = Random.Range(1, 7);
        string cal1 = Get_Cal(dau1); // lay dau 1
        string cal2 = Get_Cal(dau2); // lay dau 2
        // xu ly neu co phep chia'
        //ran_Num_Cal = Random.Range(0, 11); 
        if (ran_Num_Cal <= 7)
        {
            Range_A_Cal(so1, so2, cal1);
        }
        else
        {
            Range_Two_Cal(so1, so2, so3, cal1, cal2);
        }

        switch (Random.Range(1,3)) {
            case 1:
                Kq1.text = kq.ToString();
                Kq2.text = kqs.ToString();
                break;
            case 2:
                Kq2.text = kq.ToString();
                Kq1.text = kqs.ToString();
                break;
        } // ran o dap an
    }

    void Range_A_Cal(int so1, int so2, string cal1)
    {
        //if (cal1 == "x") { cal1 = "+"; }
        //if (cal1 == ":") { cal1 = "-"; }
        Check_Devide(so1,ref so2, cal1);
        ques_Cal.text = so1.ToString() + " " + cal1 + " " + so2.ToString();
        switch (cal1)
        {
            case "+": kq = so1 + so2; kqs = so1 - so2;
                break;
            case "-": kq = so1 - so2; kqs = so1 + so2;
                break;
            case "x": kq = so1 * so2; kqs = so1 + so2;
                break;
            case ":": kq = so1 / so2; kqs = so1 + so2;
                break;
        } // ran 1 phep tinh
    }

    void Check_Devide(int so1,ref int so2, string cal1)
    {
        if (cal1 == ":")  // xu ly so1 va so2
        {
            List<int> listUocSo = new List<int>();
            if (so1 % so2 != 0)
            {
                for (int i = 1; i <= so1; i++)
                {
                    if (so1 % i == 0) // neu i la uoc cua so1
                    {
                        listUocSo.Add(i);
                    }
                }
            }
            if (listUocSo.Count > 1)
                so2 = listUocSo[Random.Range(0, listUocSo.Count)];
            else
            {
                so2 = so1;
            }
        }
    }

    void Range_Two_Cal(int so1, int so2, int so3, string cal1, string cal2)
    {
        if (cal1 == ":")  // xu ly so1 va so2
        {
            List<int> listUocSo = new List<int>();
            if (so1 % so2 != 0)
            {
                for (int i = 1; i <= so1; i++)
                {
                    if (so1 % i == 0) // neu i la uoc cua so1
                    {
                        listUocSo.Add(i);
                    }
                }
            }
            if (listUocSo.Count > 1)
                so2 = listUocSo[Random.Range(0, listUocSo.Count)];
            else
            {
                so2 = so1;
            }
        }
        if (cal2 == ":") // xu ly so2 va so3
        {
            List<int> listUocSo = new List<int>();
            if (so2 % so3 != 0)
            {
                for (int i = 1; i <= so2; i++)
                {
                    if (so2 % i == 0) // neu i la uoc cua so1
                    {
                        listUocSo.Add(i);
                    }
                }
            }
            if (listUocSo.Count > 1)
                so3 = listUocSo[Random.Range(0, listUocSo.Count)];
            else
            {
                so3 = so2;
            }
        }
        if (cal1 == ":" && cal2 == ":") // neu c? 2 phep chia se doi cal2 thanh phep +
        {
            cal2 = "+";
        }
        ques_Cal.text = so1.ToString() + " " + cal1 + " " + so2.ToString() + " " + cal2 + " " + so3.ToString();
        switch (cal1)
        {
            case "+":
                switch (cal2)
                {
                    case "+": kq = so1 + so2 + so3; kqs = kq + 10; break;
                    case "-": kq = so1 + so2 - so3; kqs = so1 + so2 + so3; break;
                    case "x": kq = so1 + (so2 * so3); kqs = (so1 + so2) * so3; break;
                    case ":": kq = so1 + (so2 / so3); kqs = (so1 - so2) / so3; break;
                }
                break;
            case "-":
                switch (cal2)
                {
                    case "+": kq = so1 - so2 + so3; kqs = kq - 10; break;
                    case "-": kq = so1 - so2 - so3; kqs = so1 - so2 + so3; break;
                    case "x": kq = so1 - (so2 * so3); kqs = (so1 + so2) * so3; break;
                    case ":": kq = so1 - (so2 / so3); kqs = (so1 + so2) / so3; break;
                }
                break;
            case "x":
                switch (cal2)
                {
                    case "+": kq = so1 * so2 + so3; kqs = so1 + so2 * so3; break;
                    case "-": kq = (so1 * so2) - so3; kqs = (so1 - so2) * so3; break;
                    case "x": kq = so1 * so2 * so3; kqs = (so1 + so2) * so3; break;
                    case ":": kq = so1 * (so2 / so3); kqs = (so1 - so2) / so3; break;
                }
                break;
            case ":":
                switch (cal2)
                {
                    case "+": kq = so1 / so2 + so3; kqs = kq + 10; break;
                    case "-": kq = so1 / so2 - so3; kqs = so1 - so2 + so3; break;
                    case "x": kq = so1 / so2 * so3; kqs = (so1 + so2) * so3; break;
                    case ":": kq = so1 / (so2 / so3); kqs = (so1 + so2) / so3; break;
                }
                break;
        }  // ran 2 phep tinh
    }

    string Get_Cal(int x)
    {
        string s = "";
        switch (x)
        {
            case 1:
                s = "+";
                break;
            case 2:
                s = "-";
                break;
            case 3:
                s = "x";
                break;
            case 4:
                s = ":";
                break;
        }
        return s;
    }

    public void Reset()
    {
        SceneManager.LoadScene(0);
    }
}
