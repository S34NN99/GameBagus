using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {
    public GameObject creditScreen;
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

    public void OpenCredit() {
        creditScreen.SetActive(true);
    }

    public void CloseCredit() {
        creditScreen.SetActive(false);
    }
}
