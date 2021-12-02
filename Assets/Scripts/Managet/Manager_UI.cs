using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager_UI : MonoBehaviour
{
    public GameObject UI_Clother;
    public GameObject Start_UI;
    public Text level_Txt;
    void Start()
    {
        level_Txt.text = "Level: " + Save_Data.Load().level_Current.ToString();
    }

    public void Open_Clother_UI()
    {
        UI_Clother.SetActive(true);
        Start_UI.SetActive(false);
    }

    public void Close_Clother_UI()
    {
        Start_UI.SetActive(true);
        UI_Clother.SetActive(false);
    }
}
