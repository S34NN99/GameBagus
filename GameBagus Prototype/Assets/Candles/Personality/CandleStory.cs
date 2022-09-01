using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class CandleStory : MonoBehaviour {
    [SerializeField] private Candle candle;

    [SerializeField] private CandlePersonality _personality;
    public CandlePersonality Personality { get => _personality; set => _personality = value; }

    private float cooldownTimer;
    private int currentProgressInStory = 0;

    private void Start() {
        cooldownTimer = Random.Range(10f, 20f);
    }

    private void Update() {
        cooldownTimer -= Time.deltaTime;
        if (cooldownTimer <= 0) {
            if (currentProgressInStory >= Personality.StoryQuotes.Length) return;

            candle.ShowDialog(Personality.StoryQuotes[currentProgressInStory]);
            currentProgressInStory++;

            cooldownTimer = Random.Range(10f, 20f);
        }
    }
}
