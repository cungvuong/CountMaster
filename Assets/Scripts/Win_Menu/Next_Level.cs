using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Next_Level : MonoBehaviour
{
    private void Awake()
    {
        Check_Bt();
    }

    void Check_Bt()
    {
        Get_Child_Component.Get_Child(transform, "Next_Level_bt").GetComponent<Button>().onClick.AddListener(() =>
        {
            Next_Level_();
        });
    }

    void Next_Level_()
    {
        Manager_Grounds.instance.ResetMap();
        Manager_Grounds.instance.CreateNewMap();
        Pooling_Player.instance.Start_Game_Ran();
        gameObject.SetActive(false);
    }

}
