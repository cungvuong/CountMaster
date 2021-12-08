using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mn_Ui_Lv : MonoBehaviour
{
    Image[] list_Image_Lv;
    // Start is called before the first frame update
    void Start()
    {
        list_Image_Lv = GetComponentsInChildren<Image>();
        int lv_Index = Save_Data.Load().level_Current % 5; // lay ra in dex hien tai de to mau
        if (lv_Index == 0) lv_Index = 5;
        for(int i=0; i < lv_Index; i++) {
            list_Image_Lv[i].color = Color.red;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
