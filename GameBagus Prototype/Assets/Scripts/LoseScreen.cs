using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LoseScreen : MonoBehaviour
{
    public HeadManager headManager;
    public CandleManager candleManager;
    public ProjectClock projectClock;
    public ProgressBar progressBar;

    public GameObject loseScreen;
    public TextMeshProUGUI headCountText;
    public TextMeshProUGUI completedProjectText;

    private void DisplayStats()
    {
        headCountText.text = "Candles burnt out : " + headManager.HeadCount;
        completedProjectText.text = " Projects completed : " + progressBar.completedProjectCounter;
    }

    public void ShowLoseScreen()
    {
        Debug.Log("Show Lose Screen");
        candleManager.DestroyAllCandles();
        Time.timeScale = 0;
        DisplayStats();
        loseScreen.gameObject.SetActive(true);
    }

    public void Reset()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }


}
