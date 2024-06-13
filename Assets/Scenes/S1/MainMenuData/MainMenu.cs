﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("S4");
        Time.timeScale = 1;

    }
    public void QuitGame()
    {
        SceneManager.LoadScene("LoginMenu");
        //Application.Quit();
    }
    public void EXAP()
    {
       
        Application.Quit();
    }
}
