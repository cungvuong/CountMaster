using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager_Shop : MonoBehaviour
{
    public Text lv_Curr_Player;
    public Text lv_Earn_Gold;
    public Text curr_Gold;
    GameObject[] list_GameObject_Pre;
    public GameObject circle_Colors;
    bool time_Change_Color;
    private void Start()
    {
        List_Level list = Save_Data.Load();
        lv_Curr_Player.text = "Lv: " + list.mount_Player.ToString();
        lv_Earn_Gold.text = "Lv: " + list.earn_Gold_Level.ToString();
        list_GameObject_Pre = Resources.LoadAll<GameObject>("Clother_Obj");
    }
    private void Update()
    {
        if (time_Change_Color)
        {
            circle_Colors.transform.Rotate(Vector3.forward * Random.Range(5, 15));
        }
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
            Set_Gold_Curr(list.mount_Gold.ToString());
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
            Set_Gold_Curr(list.mount_Gold.ToString());
            lv_Earn_Gold.text = "Lv: " + list.earn_Gold_Level.ToString(); // set lai level text
        }
        else
        {
            Debug.Log("Chua du gold!");
        }
    }

    void Set_Gold_Curr(string x)
    {
        curr_Gold.text = x;
    }

    public void Change_Color_Player()
    {
        StartCoroutine(Time_Change_Color());
        //list_GameObject_Pre[Save_Data.Load().index_Curr].GetComponentInChildren<SkinnedMeshRenderer>().material.color = new Color(range_Color, range_Color1, range_Color2, range_Color3);
    }

    IEnumerator Time_Change_Color()
    {
        time_Change_Color = true;
        yield return new WaitForSeconds(1.5f);
        time_Change_Color = false;
        float range_Color = Random.Range(0f, 1f);
        float range_Color1 = Random.Range(0f, 1f);
        float range_Color2 = Random.Range(0f, 1f);
        float range_Color3 = Random.Range(0f, 1f);
        foreach (GameObject x in Pooling_Player.instance.list_Obj)
        {
            x.GetComponentInChildren<Transform>().GetComponentInChildren<SkinnedMeshRenderer>().material.color = new Color(range_Color, range_Color1, range_Color2, range_Color3);
        }
        
        Pooling_Player.instance.curr_Player_Pool = Pooling_Player.instance.list_Obj[0];

        Pooling_Player.instance.curr_Player_Pool.GetComponentInChildren<Transform>().GetComponentInChildren<SkinnedMeshRenderer>().material.color = new Color(range_Color, range_Color1, range_Color2, range_Color3); // set object pooling curr

        Player.instance.curr_Player = Pooling_Player.instance.curr_Player_Pool; // set lai mau khi sinh them
    }
}
