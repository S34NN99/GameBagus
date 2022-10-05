using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    [SerializeField] private int sceneIndex = 1;
    [SerializeField] private bool isMainMenu;

    public void LoadScene()
    {
        if(isMainMenu && PlayerPrefs.GetInt("Current_Level") != 0)
        {
            StartCoroutine(LoadSceneAsync(PlayerPrefs.GetInt("Current_Level")));
        }
        else
            StartCoroutine(LoadSceneAsync(sceneIndex));
    }

    IEnumerator LoadSceneAsync(int sceneID)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneID);

        while (!operation.isDone)
        {
            yield return null;
        }
    }


}
