using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class CandleSpeech : MonoBehaviour {
    [SerializeField] private CandleDialog dialogs;
    [SerializeField] private float minDialogCooldown = 3;
    [SerializeField] private float maxDialogCooldown = 6;
    [SerializeField] private float dialogLingerDuration = 2;
    [SerializeField] private Candle candle;

    [Space]
    [SerializeField] private UnityEvent onShowDialog;
    [SerializeField] private UnityEvent onHideDialog;
    [SerializeField] private TextMeshProUGUI dialogUiText;

    private float cooldownTimer;

    private void Start() {
        cooldownTimer = Random.Range(minDialogCooldown, maxDialogCooldown);
    }

    private void Update() {
        cooldownTimer -= Time.deltaTime;
        if (cooldownTimer <= 0) {
            string dialog = GetDialog();
            ShowDialog(dialog);
        }
    }

    public string GetDialog()
    {
        return dialogs.GetDialogFromCandleState(candle.SM.workingState.Name, candle.SM.moodState.Name);
    }

    public void ShowDialog(string dialogText) {
        onShowDialog.Invoke();
        dialogUiText.text = dialogText;

        StartCoroutine(HideDialog());

        IEnumerator HideDialog() {
            yield return new WaitForSeconds(dialogLingerDuration);
            cooldownTimer = Random.Range(minDialogCooldown, maxDialogCooldown) + dialogLingerDuration;
            onHideDialog.Invoke();
        }
    }
}
