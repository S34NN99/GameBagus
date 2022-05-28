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


    // Update is called once per frame
    void Update() {
        UpdateVisuals();

        foreach (var candle in candleManager.GetCandles()) {
            candle.currCandle.SM.UpdateStates(this);
        }
    }

    void UpdateVisuals() {
        progressSlider.value = currentProgress;
    }
}
