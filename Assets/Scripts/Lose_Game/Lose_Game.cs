using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lose_Game : MonoBehaviour
{
    private void Awake()
    {
        Check_Bt();
    }

    void Check_Bt()
    {
        Get_Child_Component.Get_Child(transform, "Lose_Play_Again").GetComponent<Button>().onClick.AddListener(() =>
        {
            Player_Again();
        });
    }

    void Player_Again()
    {
        Manager_Grounds.instance.ResetMap();
        Manager_Grounds.instance.CreateNewMap();
        Pooling_Player.instance.Start_Game_Ran();
        Follow.instance.Reset_Cam();
        Player.instance.can_Move = true;
        gameObject.SetActive(false);
    }
}
