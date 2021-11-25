using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Range_Ques : MonoBehaviour
{
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

    private void Start()
    {
        Range_Cal();
    }

    void Range_Cal()
    {
        int dau1 = Random.Range(1, 5); // phep tinh 1
        int dau2 = Random.Range(1, 5); // phep tinh 2
        int so1 = Random.Range(1, 7);
        int so2 = Random.Range(1, 7);
        int so3 = Random.Range(1, 7);
        string cal1 = Get_Cal(dau1); // lay dau 1
        string cal2 = Get_Cal(dau2); // lay dau 2
        // xu ly neu co phep chia
        if(cal1 == ":")  // xu ly so1 va so2
        {
            List<int> listUocSo = new List<int>();
            if (so1 % so2!= 0)
            {
                for(int i = 1; i <= so1; i++)
                {
                    if(so1 % i == 0) // neu i la uoc cua so1
                    {
                        listUocSo.Add(i);
                    }
                }
            }
            if(listUocSo.Count > 1)
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
        if (cal1 == ":" && cal2 == ":") // neu có 2 phep chia se doi cal2 thanh phep +
        {
            cal2 = "+";
        }
        //
        ques_Cal.text = so1.ToString() + " " + cal1 + " " + so2.ToString() + " " + cal2 + " " + so3.ToString();
        switch (cal1)
        {
            case "+": 
                switch (cal2) {
                    case "+": kq = so1 + so2 + so3; kqs = kq + 10; break;
                    case "-": kq = so1 + so2 - so3; kqs = so1 - so2 + so3; break;
                    case "x": kq = so1 + so2 * so3; kqs = (so1 + so2)*so3; break;
                    case ":": kq = so1 + (so2 / so3); kqs = (so1 + so2)/so3; break;
                }
                break;
            case "-":
                switch (cal2)
                {
                    case "+": kq = so1 - so2 + so3; kqs = kq - 10; break;
                    case "-": kq = so1 - so2 - so3; kqs = so1 - so2 + so3; break;
                    case "x": kq = so1 - so2 * so3; kqs = (so1 - so2) * so3; break;
                    case ":": kq = so1 - (so2 / so3); kqs = (so1 - so2) / so3; break;
                }
                break;
            case "x":
                switch (cal2)
                {
                    case "+": kq = so1 * so2 + so3; kqs = so1 + so2 * so3; break;
                    case "-": kq = so1 * so2 - so3; kqs = so1 - so2 * so3; break;
                    case "x": kq = so1 * so2 * so3; kqs = (so1 + so2) * so3; break;
                    case ":": kq = so1 * (so2 / so3); kqs = (so1 + so2) / so3; break;
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
        }

        //Debug.Log(kq);
        switch (Random.Range(1,3)) {
            case 1:
                Kq1.text = kq.ToString();
                Kq2.text = kqs.ToString();
                break;
            case 2:
                Kq2.text = kq.ToString();
                Kq1.text = kqs.ToString();
                break;
        }
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
