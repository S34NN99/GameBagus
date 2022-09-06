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

    //[SerializeField] private GameObject candleTemplate;
    [SerializeField] private GameObject[] candleTemplates;

    //[SerializeField] private Vector2[] candlePositions;
    [SerializeField] private Candle[] candles;

    [SerializeField] private CandleSkin[] candleSkins;
    [SerializeField] private CandlePersonality[] candlePersonalities;

    private void Awake() {
        candles = new Candle[candleTemplates.Length];
    }

    private void Start() {
        CheckCandles();
    }

    public void CheckCandles() {
        for (int i = 0; i < candles.Length; i++) {
            if (candles[i] == null) {
                GameObject candleGO = Instantiate(candleTemplates[i], transform);

                candles[i] = candleGO.GetComponent<Candle>();
                string candleName;
                if (Random.Range(0, 2) == 1) {
                    candleName = femaleNames[Random.Range(0, femaleNames.Length)];
                } else {
                    candleName = maleNames[Random.Range(0, maleNames.Length)];
                }
                //candles[i].candleStats.updateNameCallback.Invoke(candleName);
                candles[i].Stats.updateNameCallback.Invoke(candleName);
                //candles[i].Skin = candleSkins[Random.Range(0, candleSkins.Length)];
                candles[i].Skin = candleSkins[i];

                CandleSpeech candleSpeech = candles[i].GetComponent<CandleSpeech>();
                //candleSpeech.ShowDialog("Hi, I'm " + candleName);

                CandleStory candleStory = candles[i].GetComponent<CandleStory>();
                candleStory.Personality = candlePersonalities[Random.Range(0, candlePersonalities.Length)];
            }
        }

        GeneralEventManager.Instance.BroadcastEvent(BossQuotes.OnReplaceAllCandleEvent);
    }

    public IEnumerable<Candle> GetCandles() => candles.Where(candle => candle != null);

    public Candle RandomizeCandle()
    {
        int index = Random.Range(0, candles.Length);
        return candles[index].currCandle;
    }

    public void DestroyAllCandles() {
        for (int i = 0; i < candles.Length; i++) {
            if (candles[i] != null)
                Destroy(candles[i].gameObject);
        }
    }

    public void CheckIfListEmpty() {
        float counter = 0;
        for (int i = 0; i < candles.Length; i++) {
            if (candles[i] == null || this.gameObject == candles[i]) {
                Debug.Log("is null");
                counter++;
            }

            if (counter >= 3) {
                LoseScreen loseScreen = FindObjectOfType<LoseScreen>();
                loseScreen.ShowLoseScreen();
            }
        }
    }
}


public class CandleRandomiser : ScriptableObject {
    [SerializeField] private GameObject[] candleTemplates;
    [SerializeField] private CandleSkin[] candleSkins;
    [SerializeField] private CandlePersonality[] candlePersonalities;

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

    public void Randomise(Candle candle) {
        string candleName;
        if (Random.Range(0, 2) == 1) {
            candleName = femaleNames[Random.Range(0, femaleNames.Length)];
        } else {
            candleName = maleNames[Random.Range(0, maleNames.Length)];
        }
        //candle.candleStats.updateNameCallback.Invoke(candleName);
        candle.Stats.updateNameCallback.Invoke(candleName);
        candle.Skin = candleSkins[Random.Range(0, candleSkins.Length)];

        CandleSpeech candleSpeech = candle.GetComponent<CandleSpeech>();
        //candleSpeech.ShowDialog("Hi, I'm " + candleName);

        CandleStory candleStory = candle.GetComponent<CandleStory>();
        candleStory.Personality = candlePersonalities[Random.Range(0, candlePersonalities.Length)];
    }
}
