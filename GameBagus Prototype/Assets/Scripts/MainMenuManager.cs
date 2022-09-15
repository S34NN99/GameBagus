using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

    [SerializeField] private int startLevelBuildIndex = 1;

    private void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    public void Quit() {
        Application.Quit();
    }

    public void NewGame() {
        Debug.Log("clicking");
        SceneManager.LoadScene(startLevelBuildIndex);
    }

    public void OpenScreen(GameObject screen)
    {
        screen.SetActive(true);
    }

    public void CloseScreen(GameObject screen)
    {
        screen.SetActive(false);
    }
}
