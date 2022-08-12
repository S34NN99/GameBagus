using System.Collections.Generic;
using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MetricsPanel : MonoBehaviour {
    [SerializeField] private string mainMenuSceneName;
    [SerializeField] private string nextProjectSceneName;

    [Header("UI Components")]
    [SerializeField] private RectTransform rootTransform;
    [SerializeField] private TextMeshProUGUI tasksCompletedText;
    [SerializeField] private TextMeshProUGUI burnoutCountText;
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
        burnoutCountText.text = "Candles burnt out : " + burnoutCount;
    }

    public void UpdateTasksCompleted(int oldVal, int tasksCompleted) {
        tasksCompletedText.text = "Tasks completed : " + tasksCompleted;
    }

    public void UpdateStars(int oldVal, int starsEarned) {
    }
}
