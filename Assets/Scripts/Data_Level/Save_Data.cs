using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class Save_Data
{
    public static string directory = "/SaveData/";   
    public static string filename = "Level.json";
    
    public static void Save(List_Level data)
    {
        string dir = Application.persistentDataPath + directory;

        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(dir + filename, json);
    }

    public static List_Level Load()
    {
        string fullPath = Application.persistentDataPath + directory + filename;
        List_Level list = new List_Level();

        if (File.Exists(fullPath)){
            string json = File.ReadAllText(fullPath);
            list = JsonUtility.FromJson<List_Level>(json);
        }
        else
        {
            Debug.Log("not exist");
        }
        return list;
    }
}
