using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.Events;

public class CandleManager : MonoBehaviour {
    //[SerializeField] private GameObject candleTemplate;
    [SerializeField] private GameObject[] candleTemplates;
    [SerializeField] private CandleRandomiser randomiser;
    private CandleRandomiser.State randomiserState;

    //[SerializeField] private Vector2[] candlePositions;
    [SerializeField] private Candle[] candles;
    public IReadOnlyList<Candle> CandleSlots => candles;

    [SerializeField] private CandleSkin[] candleSkins;
    [SerializeField] private CandlePersonality[] candlePersonalities;

    [SerializeField] private UnityEvent onCandlesBurntOut;
    private System.Random prng;

    private void Awake() {
        candles = new Candle[candleTemplates.Length];
        prng = new System.Random();
    }

    private void Start() {
        CheckCandles();
    }

    public void CheckCandles() {
        if (randomiserState == null) {
            randomiserState = randomiser.CreateState();
        }

        for (int i = 0; i < candles.Length; i++) {
            if (candles[i] == null) {
                GameObject candleGO = Instantiate(candleTemplates[i], transform);
                candles[i] = candleGO.GetComponent<Candle>();

                randomiser.Randomise(candles[i], randomiserState);
            }
        }

        //GeneralEventManager.Instance.BroadcastEvent(BossQuotes.OnReplaceAllCandleEvent);
    }

    private void UpdateCandleProfile(Candle candle, string name, string initial) {
        candle.Profile.ProfileName = name;
        candle.Profile.Initials = initial;
    }

    public IEnumerable<Candle> GetCandles() => candles.Where(candle => candle != null);

    public Candle RandomizeCandle() {
        int index = Random.Range(0, candles.Length);
        return candles[index].currCandle;
    }

    public void DestroyAllCandles() {
        for (int i = 0; i < candles.Length; i++) {
            if (candles[i] != null)
                Destroy(candles[i].gameObject);
        }
    }

    public int CheckRemainingCandles() {
        int counter = 0;
        foreach (var candles in this.GetCandles()) {
            counter++;
        }

        return counter;
    }

    public void CheckIfListEmpty() {
        float counter = 0;
        for (int i = 0; i < candles.Length; i++) {
            if (candles[i] == null || this.gameObject == candles[i]) {
                Debug.Log("is null");
                counter++;
            }

            if (counter >= 3) {
                //LoseScreen loseScreen = FindObjectOfType<LoseScreen>();
                //loseScreen.ShowLoseScreen();
                onCandlesBurntOut?.Invoke();
            }
        }
    }
}
