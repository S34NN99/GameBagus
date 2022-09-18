using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    public void LoadScene(int sceneID)
    {
        LoadSceneAsync(sceneID);
    }

    void LoadSceneAsync(int sceneID)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneID);

        //while(!operation.isDone)
        //{
        //    yield return null;
        //}
    }
}
