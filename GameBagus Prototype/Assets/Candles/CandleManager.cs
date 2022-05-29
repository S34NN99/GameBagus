using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class CandleManager : MonoBehaviour {
    private string[] femaleNames = {
        "Elisa",
        "Ashley",
        "Nicole",
        "Sarah",
        "Nadia",
    };

    private string[] maleNames = {
        "Joe",
        "Brian",
        "Adam",
        "Jason",
        "Steven",
    };

    [SerializeField] private GameObject candleTemplate;

    [SerializeField] private Vector2[] candlePositions;
    [SerializeField] private Candle[] candles;

    [SerializeField] private CandleSkin[] candleSkins;
    [SerializeField] private CandlePersonality[] candlePersonalities;

    private void Start() {
        CheckCandles();
    }

    public void CheckCandles() {
        for (int i = 0; i < candles.Length; i++) {
            if (candles[i] == null) {
                // Spawn candle here
                GameObject candleGO = Instantiate(candleTemplate, transform);
                candleGO.GetComponent<RectTransform>().anchoredPosition = candlePositions[i];

                candles[i] = candleGO.GetComponent<Candle>();
                string candleName;
                if (Random.Range(0, 2) == 1) {
                    candleName = femaleNames[Random.Range(0, femaleNames.Length)];
                } else {
                    candleName = maleNames[Random.Range(0, maleNames.Length)];
                }
                candles[i].candleStats.nameText.text = candleName;
                //candles[i].Skin = candleSkins[Random.Range(0, candleSkins.Length)];
                candles[i].Skin = candleSkins[i];

                CandleSpeech candleSpeech = candles[i].GetComponent<CandleSpeech>();
                candleSpeech.ShowDialog("Hi, I'm " + candleName);

                CandleStory candleStory = candles[i].GetComponent<CandleStory>();
                candleStory.Personality = candlePersonalities[Random.Range(0, candlePersonalities.Length)];
            }
        }
    }

    public IEnumerable<Candle> GetCandles() => candles.Where(candle => candle != null);
}
