using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class CandleSpeech : MonoBehaviour {
    [SerializeField] private CandleStateDialog dialogs;
    [SerializeField] private Candle candle;

    [Header("Cooldown")]
    [SerializeField] private float minDialogCooldown = 8;
    [SerializeField] private float maxDialogCooldown = 15;

    private float cooldownTimer;

    private void Start() {
        cooldownTimer = Random.Range(minDialogCooldown, maxDialogCooldown);
    }

    private void Update() {
        cooldownTimer -= Time.deltaTime;
        if (cooldownTimer <= 0) {
            candle.ShowDialog(GetDialog());
            cooldownTimer = Random.Range(minDialogCooldown, maxDialogCooldown);
        }
    }

    public string GetDialog() {
        return dialogs.GetDialogFromCandleState(candle.SM.workingState.Name, candle.SM.moodState.Name);
    }
}
