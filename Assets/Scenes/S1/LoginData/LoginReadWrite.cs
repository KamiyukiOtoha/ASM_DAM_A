using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.IO;
using System.IO; // Muon doc ghi file thif khai bao IO

public class LoginReadWrite : MonoBehaviour
{
    // Khai bao cac phuong thuc Field
    public TMP_InputField IdInputField;
    public TMP_InputField NameInputField;
    public TMP_InputField PasswordInputField;

    public void SaveLoginData()
    {
        LoginMenu Data = new LoginMenu();
        Data.ID = IdInputField.text; // Nguoi choi nhap ID
        Data.Name = NameInputField.text; // Nguoi choi nhap User Name
        Data.Password = PasswordInputField.text; // Nguoi choi nhap Password

        string json = JsonUtility.ToJson(Data,true); // Luu Json
        File.WriteAllText(Application.dataPath +"FormLogin.json",json);
        Debug.Log("Success");


    }

    public void LoadLoginData()
    {
       string json =  File.ReadAllText(Application.dataPath + "FormLogin.json");
        LoginMenu Data = JsonUtility.FromJson<LoginMenu>(json);
        IdInputField.text = Data.ID;
        NameInputField.text = Data.Name;
        PasswordInputField.text = Data.Password;
    }
}
