using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProgressBar : MonoBehaviour
{
    [Space(20)]
    [Header("ProgressBar")]
    public float currentProgress;
    public Slider progressSlider;

    [Space(20)]
    [Header("Candle")]
    public List<Candle> Candles;

    // Start is called before the first frame update
    void Start()
    {
        foreach(Candle candle in FindObjectsOfType<Candle>())
        {
            Candles.Add(candle);
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateVisuals();

        foreach (Candle candle in Candles)
        {
            if(candle != null)
            candle.currCandle.SM.UpdateStates(this);
        }
    }

    void UpdateVisuals()
    {
        progressSlider.value = currentProgress;
    }
}
