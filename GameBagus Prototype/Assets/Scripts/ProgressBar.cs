using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProgressBar : MonoBehaviour {
    [Space(20)]
    [Header("ProgressBar")]
    public float currentProgress;
    public Slider progressSlider;

    [Space(20)]
    [Header("Candle")]
    [SerializeField] private CandleManager candleManager;

    [Space(20)]
    [Header("Clock")]
    [SerializeField] private ProjectClock clock;


    // Update is called once per frame
    void Update() {

        foreach (var candle in candleManager.GetCandles())
        {
            candle.currCandle.SM.UpdateStates(this);
        }
    }

    public void UpdateVisuals() {
        progressSlider.value = currentProgress;

        if (progressSlider.value >= progressSlider.maxValue)
        {
            progressSlider.value = 0;
            currentProgress = 0;
            Debug.Log("Resetting");
            candleManager.CheckCandles();
            clock.ResetClock(clock.ProjectDuration);
        }
    }
}
