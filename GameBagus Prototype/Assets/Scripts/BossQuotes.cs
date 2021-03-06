using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using TMPro;

public class BossQuotes : MonoBehaviour {
    public const string NearingDeadlineEvent = "Nearing Deadline";
    public const string OnProjectFinishedEvent = "On Project Finished";

    public const string OnCandleBurnoutEvent = "On Candle Burnout";
    public const string OnReplaceAllCandleEvent = "On Replace All Candle";
    public const string OnCandleVacationEvent = "On Candle Vacation";

    [Header("Quotes")]
    [SerializeField] private string[] quotes_nearingDeadline;
    [SerializeField] private string[] quotes_projectFinished;

    [SerializeField] private string[] quotes_candleBurnout;
    [SerializeField] private string[] quotes_replaceAllCandle;
    [SerializeField] private string[] quotes_candleVacation;

    [Header("Dialog UI")]
    [SerializeField] private RectTransform dialogUiParent;
    [SerializeField] private TextMeshProUGUI dialogUiTxt;
    [SerializeField] private CanvasGroup dialogUiGroup;
    [SerializeField] private float dialogFlashDuration = 0.5f;
    [SerializeField] private float dialogLingerDuration = 3f;

    private float dialogCooldown;

    private void Start() {
        GameEventManager.Instance.CreateNewEvent(NearingDeadlineEvent);
        GameEventManager.Instance.SubscribeToEvent(NearingDeadlineEvent, () => {
            ShowDialog(quotes_nearingDeadline[Random.Range(0, quotes_nearingDeadline.Length)]);
        });

        GameEventManager.Instance.CreateNewEvent(OnProjectFinishedEvent);
        GameEventManager.Instance.SubscribeToEvent(OnProjectFinishedEvent, () => {
            ShowDialog(quotes_projectFinished[Random.Range(0, quotes_projectFinished.Length)]);
        });



        GameEventManager.Instance.CreateNewEvent(OnCandleBurnoutEvent);
        GameEventManager.Instance.SubscribeToEvent(OnCandleBurnoutEvent, () => {
            ShowDialog(quotes_candleBurnout[Random.Range(0, quotes_candleBurnout.Length)]);
        });

        GameEventManager.Instance.CreateNewEvent(OnReplaceAllCandleEvent);
        GameEventManager.Instance.SubscribeToEvent(OnReplaceAllCandleEvent, () => {
            ShowDialog(quotes_replaceAllCandle[Random.Range(0, quotes_replaceAllCandle.Length)]);
        });

        GameEventManager.Instance.CreateNewEvent(OnCandleVacationEvent);
        GameEventManager.Instance.SubscribeToEvent(OnCandleVacationEvent, () => {
            ShowDialog(quotes_candleVacation[Random.Range(0, quotes_candleVacation.Length)]);
        });
    }

    private void Update() {
        if (dialogCooldown > dialogLingerDuration + dialogFlashDuration) {
            dialogCooldown -= Time.deltaTime;

            float flashVal = Mathf.InverseLerp(dialogFlashDuration, 0, dialogCooldown - dialogLingerDuration - dialogFlashDuration);

            dialogUiParent.localScale = new Vector3(flashVal, flashVal, 1);
            dialogUiGroup.alpha = flashVal;
        } else if (dialogCooldown > dialogFlashDuration) {
            dialogCooldown -= Time.deltaTime;

            //float lingerVal = Mathf.InverseLerp(dialogLingerDuration, 0, dialogCooldown - dialogFlashDuration);

            //dialogUiGroup.alpha = 1 - lingerVal;
        } else if (dialogCooldown > 0) {
            dialogCooldown -= Time.deltaTime;

            float flashVal = Mathf.InverseLerp(0, dialogFlashDuration, dialogCooldown);

            dialogUiParent.localScale = new Vector3(flashVal, flashVal, 1);
            dialogUiGroup.alpha = flashVal;
        }
    }

    public void ShowDialog(string dialogText) {
        dialogCooldown = dialogFlashDuration + dialogLingerDuration + dialogFlashDuration;
        dialogUiTxt.text = dialogText;
    }
}
