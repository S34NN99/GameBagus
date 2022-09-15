using System.Collections.Generic;
using System.Collections;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Obsolete]
public class MetricsPanel : MonoBehaviour {
    [SerializeField] private string mainMenuSceneName;
    [SerializeField] private string nextProjectSceneName;

    [Header("UI Components")]
    [SerializeField] private RectTransform rootTransform;
    [SerializeField] private UnityEvent<string> updateTasksCompletedTextCallback;
    [SerializeField] private UnityEvent<string> updateBurnoutCountTextCallback;
    [SerializeField] private Image[] starImages;

    public void Show(float delay = 0) {
        IEnumerator DelayCoroutine() {
            yield return new WaitForSeconds(delay);
            rootTransform.gameObject.SetActive(true);
            Time.timeScale = 0;
        }

        StartCoroutine(DelayCoroutine());
    }

    public void ExitToMainMenu() {
        SceneManager.LoadScene(mainMenuSceneName, LoadSceneMode.Single);

        Time.timeScale = 1;
    }

    public void ProceedToNextProject() {
        SceneManager.LoadScene(nextProjectSceneName, LoadSceneMode.Single);

        Time.timeScale = 1;
    }

    public void UpdateBurnoutCount(int oldVal, int burnoutCount) {
        updateBurnoutCountTextCallback.Invoke("Candles burnt out : " + burnoutCount);
    }

    public void UpdateTasksCompleted(int oldVal, int tasksCompleted) {
        updateTasksCompletedTextCallback.Invoke("Tasks completed : " + tasksCompleted);
    }

    public void UpdateStars(int oldVal, int starsEarned) {
    }
}
