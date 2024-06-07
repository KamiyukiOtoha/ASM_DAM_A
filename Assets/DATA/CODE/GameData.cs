using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using System.IO;
using UnityEditor;

//[Serializable] dùng để lưu trữ dữ liệu của class này
[Serializable]

public class GameData
{
    public int score;
    public float timeplay;
}
// class dùng để xử lý đọc và ghi file 
public class DataManager
{
    const string FILE_NAME = "data.txt";
    public static bool SaveData(GameData data)
    {
        try
        {
            // chuyển sang dạng text
            var json = JsonUtility.ToJson(data);
            var fileStream = new FileStream(FILE_NAME, FileMode.Create);
            using (var writer = new StreamWriter(fileStream))
            {
                writer.Write(json);
            }
            return true;
        }
        catch (Exception e)
        {
            Debug.Log($"Save data error:{e.Message}");
        }
        return false;
    }

    public static GameData ReadData()
    {
        try
        {
            if (File.Exists(FILE_NAME))
            {
                // Open file
                var fileStream = new FileStream(FILE_NAME, FileMode.Open);
                using (var reader = new StreamReader(fileStream))
                {
                    // doc date on file
                    var json = reader.ReadToEnd();
                    // chuyen dy lieu tu json sang class
                    var data = JsonUtility.FromJson<GameData>(json);
                    return data;
                }
            }
        }
        catch (System.Exception e)
        {
            Debug.Log("Enter Loading file;" + e.Message);
        }
        return null;
    }
}