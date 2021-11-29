using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_Ques_Range : MonoBehaviour
{
    Range_Ques[] range_Ques;
    // Start is called before the first frame update
    private void Awake()
    {
        range_Ques = GetComponentsInChildren<Range_Ques>();

        for(int i=0; i<range_Ques.Length; i++)
        {
            if (i < 1)
            {
                range_Ques[i].ran_Num_Cal = 5;
            }
            else
            {
                range_Ques[i].ran_Num_Cal = 9;
            }
        }
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
