using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_Grounds : MonoBehaviour
{
    public GameObject ground1; 
    public GameObject ground2; 
    public GameObject gr_co_cau; 
    public GameObject gr_ko_cau; 
    
    public GameObject things_Parent;
    int range;
    public GameObject[] things_Clone_Free;
    public GameObject[] things_Clone_All;
    public GameObject[] things_Map1_1;
    public GameObject[] things_Map1_2;
    //public GameObject things_Clone_Ques;
    public GameObject[] things_Clone_Ques_Trap;
    float[] range_Trap_Dis = { 15f, 40f, 50f, 60f, 70f, 95f, 105f, 115f, 125f, 135f};
    //
    void Start()
    {
        // ran map 1.1 
        Instantiate(things_Clone_All[Random.Range(0, things_Clone_All.Length)], things_Clone_All[Random.Range(0, things_Clone_All.Length)].transform.position, things_Clone_All[Random.Range(0, things_Clone_All.Length)].transform.rotation, things_Parent.transform);
        Instantiate(things_Clone_All[Random.Range(0, things_Clone_All.Length)], things_Clone_All[Random.Range(0, things_Clone_All.Length)].transform.position + new Vector3(0f, 0f, 8f), things_Clone_All[Random.Range(0, things_Clone_All.Length)].transform.rotation, things_Parent.transform);
        //range = Random.Range(0,2);   // range map 1 or 2
        range = Random.Range(0,1);
        if(range == 1)
        {
            range = Random.Range(0, 3);
            if(range == 0)
            {
                Instantiate(ground1, transform.position, Quaternion.identity, transform);
                Instantiate(ground2, transform.position + new Vector3(0f, 0f, 100f), Quaternion.identity, transform);
            }
            if(range == 1)
            {
                Instantiate(ground2, transform.position, Quaternion.identity, transform);
                Instantiate(ground1, transform.position + new Vector3(0f, 0f, 100f), Quaternion.identity, transform);
            }
            if(range == 2)
            {
                Instantiate(ground2, transform.position, Quaternion.identity, transform);
                Instantiate(ground2, transform.position + new Vector3(0f, 0f, 100f), Quaternion.identity, transform);
            }
        } // range ground
        else
        {
            int k = 1;
            Instantiate(gr_co_cau, transform.position, gr_co_cau.transform.rotation, transform);
            Instantiate(gr_co_cau, transform.position + new Vector3(0f, 0f, 55f * k), gr_co_cau.transform.rotation, transform); k++;
            GameObject late = Instantiate(gr_ko_cau, transform.position + new Vector3(0f, 0f, 55f * k), gr_ko_cau.transform.rotation, transform); k++;
            Instantiate(gr_ko_cau, late.transform.position + new Vector3(0f, 0f, 50f), gr_ko_cau.transform.rotation, transform); k++;
        }
        // clone Trap map 1.1
        int trap_Dis = 0;
        range = Random.Range(0, things_Map1_1.Length);
        for(int i=0; i< 1; i++)// range_Trap_Dis.Length; i++)
        {
            Instantiate(things_Map1_1[range], things_Map1_1[range].transform.position + new Vector3(0f, 0f, range_Trap_Dis[trap_Dis]), Quaternion.identity, things_Parent.transform);
            trap_Dis++;
        }
        // clone Trap map 1.2
        for (int i = 0; i < range_Trap_Dis.Length - 4; i++)// range_Trap_Dis.Length; i++)
        {
            range = Random.Range(0, things_Map1_2.Length);
            Instantiate(things_Map1_2[range], things_Map1_2[range].transform.position + new Vector3(0f, 0f, range_Trap_Dis[trap_Dis]), things_Map1_2[range].transform.rotation, things_Parent.transform);
            trap_Dis++;
        }
        // clone Trap map 1.3
        for (int i = 0; i < range_Trap_Dis.Length-7; i++)// range_Trap_Dis.Length; i++)
        {
            range = Random.Range(0, things_Map1_2.Length);
            Instantiate(things_Map1_2[range], things_Map1_2[range].transform.position + new Vector3(0f, 0f, range_Trap_Dis[trap_Dis]), things_Map1_2[range].transform.rotation, things_Parent.transform);
            trap_Dis++;
        }
    }
}
