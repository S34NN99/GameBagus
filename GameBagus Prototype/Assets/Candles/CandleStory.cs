using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class CandleStory : MonoBehaviour {
    [SerializeField] private CandleSpeech candleSpeech;

    [SerializeField] private string[] personalQuotes;

    private float cooldownTimer;
    private int currentProgressInStory = 0;

    private void Start() {
        cooldownTimer = Random.Range(10f, 20f);
    }

    private void Update() {
        cooldownTimer -= Time.deltaTime;
        if (cooldownTimer <= 0) {
            if (currentProgressInStory >= personalQuotes.Length) return;

            candleSpeech.ShowDialog(personalQuotes[currentProgressInStory]);
            currentProgressInStory++;

            cooldownTimer = Random.Range(10f, 20f);
        }
    }
}
