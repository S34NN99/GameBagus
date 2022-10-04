using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;


public class MainMenuManager : MonoBehaviour {

    [SerializeField] private int startLevelBuildIndex = 1;
    [SerializeField] private MultipleEndingsSystem mes;
    [SerializeField] private UnityEvent FirstLaunch;

    private void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        CheckFirstLaunch();
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

    public void CheckFirstLaunch()
    {
        int checking = PlayerPrefs.GetInt("CompletedTutorial", 0);

        if (checking == 0)
            FirstLaunch.Invoke();
        else
            return;

    }
}
