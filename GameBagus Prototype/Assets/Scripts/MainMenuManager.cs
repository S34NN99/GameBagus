using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void Quit()
    {
        Application.Quit();
    }

    public void NewGame()
    {
        Debug.Log("clicking");
        SceneManager.LoadScene(1);
    }
}
