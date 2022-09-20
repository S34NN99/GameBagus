using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.Events;

public class ScriptedDialog : MonoBehaviour {
    [SerializeField] private CandleManager candles;
    [SerializeField] private Dialog[] dialogs;
    [SerializeField] private UnityEvent<CandleProfile, string> showDialogCallback;
    //[SerializeField] private bool randomiseCandles = true;

    [SerializeField] private int candlesRequired;

    private void Awake() {
        if (dialogs.Any()) {
            candlesRequired = dialogs.Max(x => x.candleProfileId);
        }
    }

    public void StartDialog() {
        Candle[] remainingCandles = candles.GetCandles().ToArray();

        if (remainingCandles.Length < candlesRequired) {
            Debug.Log("Not enough candles!");
            return;
        }

        foreach (var dialog in dialogs) {
            CandleProfile profile = remainingCandles[dialog.candleProfileId - 1].Profile;
            showDialogCallback.Invoke(profile, dialog.message);
        }
    }

    [System.Serializable]
    private class Dialog {
        public int candleProfileId;
        public string message;
        public float delay;
    }
}
