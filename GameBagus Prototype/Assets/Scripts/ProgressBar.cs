using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProgressBar : MonoBehaviour {
    [Space(20)]
    [Header("ProgressBar")]
    public float currentProgress;
    public TextMeshProUGUI progressText;
    public Animator wokAnim;

    [Space(20)]
    [Header("Candle")]
    [SerializeField] private CandleManager candleManager;

    [Space(20)]
    [Header("Clock")]
    [SerializeField] private ProjectClock clock;


    // Update is called once per frame
    void Update() {

        foreach (var candle in candleManager.GetCandles()) {
            candle.currCandle.SM.UpdateStates(this);
        }
    }

    public void UpdateVisuals() {

        if (currentProgress >= 100) {
            currentProgress = 0;
            wokAnim.SetTrigger("WokLoop");
            candleManager.CheckCandles();
            clock.ResetClock(clock.ProjectDuration);
        }

        progressText.text = (currentProgress).ToString("0.0") + " %";
    }
}
