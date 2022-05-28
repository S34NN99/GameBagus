using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(menuName = "Candle Dialogs")]
public class CandleDialog : ScriptableObject {
    [SerializeField] private List<string> _workingHappy;
    public List<string> WorkingHappy => _workingHappy;

    public string RandomDialogWhenWorkingHappy => WorkingHappy[Random.Range(0, WorkingHappy.Count)];

    [SerializeField] private List<string> _workingNeutral;
    public List<string> WorkingNeutral => _workingNeutral;

    public string RandomDialogWhenWorkingNeutral => WorkingHappy[Random.Range(0, WorkingHappy.Count)];

    [SerializeField] private List<string> _workingSad;
    public List<string> WorkingSad => _workingSad;

    public string RandomDialogWhenWorkingSad => WorkingHappy[Random.Range(0, WorkingHappy.Count)];

    [SerializeField] private List<string> _crunching;
    public List<string> Crunching => _crunching;

    public string RandomDialogWhenCrunching => WorkingHappy[Random.Range(0, WorkingHappy.Count)];

    [SerializeField] private List<string> _onVacation;
    public List<string> OnVacation => _onVacation;

    public string RandomDialogWhenOnVacation => WorkingHappy[Random.Range(0, WorkingHappy.Count)];

    public string GetDialogFromCandleState(string workingState, string moodState) {
        return workingState switch {
            "Active" => moodState switch {
                "Happy" => RandomDialogWhenWorkingHappy,
                "Neutral" => RandomDialogWhenWorkingNeutral,
                "Sad" => RandomDialogWhenWorkingSad,
                _ => "",
            },
            "Crunching" => RandomDialogWhenCrunching,
            "OnVacation" => RandomDialogWhenOnVacation,
            _ => "",
        };
    }
}
