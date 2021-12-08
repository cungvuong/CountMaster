using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager_UI : MonoBehaviour
{
    public GameObject UI_Clother;
    public GameObject Start_UI;
    public GameObject Menu_Controll;
    public Text level_Txt;
    public Text gold_Txt;
    public Text diamond_Txt;
    
    void Start()
    {
        gold_Txt.text = Save_Data.Load().mount_Gold.ToString();
        diamond_Txt.text = "0";
        level_Txt.text = "Level: " + Save_Data.Load().level_Current.ToString();
    }

    public void Open_Clother_UI()
    {
        UI_Clother.SetActive(true);
        Start_UI.SetActive(false);
        Menu_Controll.SetActive(false);
    }

    public void Close_Clother_UI()
    {
        UI_Clother.SetActive(false);
        Menu_Controll.SetActive(true);
        Start_UI.SetActive(true);
    }

}
