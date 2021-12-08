using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager_Shop : MonoBehaviour
{
    public Text lv_Curr_Player;
    public Text lv_Earn_Gold;

    Awake
    private void Start()
    {
        List_Level list = Save_Data.Load();
        lv_Curr_Player.text = "Lv: " + list.mount_Player.ToString();
        lv_Earn_Gold.text = "Lv: " + list.earn_Gold_Level.ToString();
    }

    public void Increase_Player_Curr()  // mua cap do khoi dau cua player
    {
        if (Save_Data.Load().mount_Gold >= 100)
        {
            Debug.Log("Da tang level curr_Player");
            List_Level list = Save_Data.Load();
            list.mount_Gold -= 100;
            list.mount_Player++;
            Save_Data.Save(list);
            lv_Curr_Player.text = "Lv: " + list.mount_Player.ToString(); // set lai level text
            Player.instance.Inscrease_Player_ByGold(1);
        }
        else
        {
            Debug.Log("Chua du gold!");
        }
    }

    public void Increase_Earn_Gold() // mua ti le tang vang cua player
    {
        if (Save_Data.Load().mount_Gold >= 100)
        {
            Debug.Log("Da tang level earn_Gold");
            List_Level list = Save_Data.Load();
            list.mount_Gold -= 100;
            list.earn_Gold_Level++;
            Save_Data.Save(list);
            lv_Earn_Gold.text = "Lv: " + list.earn_Gold_Level.ToString(); // set lai level text
        }
        else
        {
            Debug.Log("Chua du gold!");
        }
    }
}
