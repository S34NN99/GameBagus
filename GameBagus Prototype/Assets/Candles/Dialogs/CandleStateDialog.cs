using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(menuName = "Candle/State Dialog")]
//todo rename to candle state dialog
public class CandleStateDialog : ScriptableObject {
    [SerializeField] private string[] workingDialogs_Happy;
    [SerializeField] private string[] workingDialogs_Neutral;
    [SerializeField] private string[] workingDialogs_Sad;
    [SerializeField] private string[] onCrunchingDialogs;
    [SerializeField] private string[] onVacationDialogs;

    [SerializeField] private string[] onDeathDialogs;

    public string GetDialogFromCandleState(string workingState, string moodState) {
        return workingState switch {
            "Working" => moodState switch {
                "Happy" => workingDialogs_Happy.Random(),
                "Neutral" => workingDialogs_Neutral.Random(),
                "Sad" => workingDialogs_Sad.Random(),
                _ => "",
            },
            "Crunching" => onCrunchingDialogs.Random(),
            "OnVacation" => onVacationDialogs.Random(),
            _ => "",
        };
    }

    [System.Serializable]
    // todo rename to RandomizedDialog
    public class DialogList {
        [SerializeField] private List<string> _dialogLines;
        public List<string> DialogLines => _dialogLines;

        public string RandomDialog() => DialogLines[Random.Range(0, _dialogLines.Count)];
    }
}


[CreateAssetMenu(menuName = "Candle/Scripted Dialog")]
public class CandleScriptedDialog : ScriptableObject {
    [SerializeField] private string[] workingDialogs_Happy;
    [SerializeField] private string[] workingDialogs_Neutral;
    [SerializeField] private string[] workingDialogs_Sad;
    [SerializeField] private string[] onCrunchingDialogs;
    [SerializeField] private string[] onVacationDialogs;

    [SerializeField] private string[] onDeathDialogs;

    public string GetDialogFromCandleState(string workingState, string moodState) {
        return workingState switch {
            "Working" => moodState switch {
                "Happy" => workingDialogs_Happy.Random(),
                "Neutral" => workingDialogs_Neutral.Random(),
                "Sad" => workingDialogs_Sad.Random(),
                _ => "",
            },
            "Crunching" => onCrunchingDialogs.Random(),
            "OnVacation" => onVacationDialogs.Random(),
            _ => "",
        };
    }
}
