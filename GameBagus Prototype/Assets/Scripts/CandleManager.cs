using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class CandleManager : MonoBehaviour {
    [SerializeField] private GameObject candleTemplate;

    [SerializeField] private Vector2[] candlePositions;
    [SerializeField] private Candle[] candles;

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
            }
        }
    }

    public IEnumerable<Candle> GetCandles() => candles.Where(candle => candle != null);
}
