using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Login : MonoBehaviour
{
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    public Button loginButton;
    public Button goToRegisterButton;

    ArrayList credentials;

    // Start is called before the first frame update
    void Start()
    {
        loginButton.onClick.AddListener(LoginUser);
        goToRegisterButton.onClick.AddListener(MoveToRegister);

        if (File.Exists(Application.dataPath + "/credentials.txt"))
        {
            credentials = new ArrayList(File.ReadAllLines(Application.dataPath + "/credentials.txt"));
        }
        else
        {
            Debug.Log("Credential file doesn't exist");
        }
    }

    // Update is called once per frame
    void LoginUser()
    {
        bool isExists = false;

        credentials = new ArrayList(File.ReadAllLines(Application.dataPath + "/credentials.txt"));

        foreach (var i in credentials)
        {
            string line = i.ToString();
            if (line.Substring(0, line.IndexOf(":")).Equals(usernameInput.text) &&
                line.Substring(line.IndexOf(":") + 1).Equals(passwordInput.text))
            {
                isExists = true;
                break;
            }
        }

        if (isExists)
        {
            Debug.Log($"Logging in '{usernameInput.text}'");
            UserSession.Instance.SetUsername(usernameInput.text);
            LoadWelcomeScreen();
        }
        else
        {
            Debug.Log("Incorrect credentials");
        }
    }

    void MoveToRegister()
    {
        SceneManager.LoadScene("RegisterMenu");
    }

    void LoadWelcomeScreen()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
