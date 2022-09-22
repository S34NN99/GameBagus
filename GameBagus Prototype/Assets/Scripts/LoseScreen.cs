using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

[System.Obsolete]
public class LoseScreen : MonoBehaviour {
    public CandleManager candleManager;
    public ProjectCalendar projectClock;
    public Project progressBar;

    public GameObject loseScreen;
    public TextMeshProUGUI headCountText;
    public TextMeshProUGUI completedProjectText;

    [SerializeField] private IntProperty headCountProp;
    [SerializeField] private IntProperty completedProjectsProp;

    private void DisplayStats() {
        headCountText.text = "Candles burnt out : " + headCountProp.Value;
        completedProjectText.text = " Projects completed : " + completedProjectsProp.Value;
    }

    public void ShowLoseScreen() {
        Debug.Log("Show Lose Screen");
        candleManager.DestroyAllCandles();
        Time.timeScale = 0;
        DisplayStats();
        loseScreen.gameObject.SetActive(true);
    }

    public void Reset() {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void GoBackToMainMenu() {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }


}
