
using UnityEngine;

[CreateAssetMenu(menuName = "Game State Database")]
public class GameStateDatabase : ScriptableObject {

    [SerializeField] private string[] _boolStateNames;
    public string[] BoolStateNames => _boolStateNames;

    [SerializeField] private string[] _numStateNames;
    public string[] NumStateNames => _numStateNames;

    [SerializeField] private NumToBoolStateTrigger[] _numToBoolStateTriggers;
    public NumToBoolStateTrigger[] NumToBoolStateTriggers => _numToBoolStateTriggers;


    [Space(20)]
    [Header("For documentation purposes only")]
    [SerializeField] private string[] _otherUnsavedBoolStateNames;
    public string[] OtherUnsavedBoolStateNames => _otherUnsavedBoolStateNames;

    [SerializeField] private string[] _otherUnsavedNumStateNames;
    public string[] OtherUnsavedNumStateNames => _otherUnsavedNumStateNames;

    [System.Serializable]
    public class NumToBoolStateTrigger {
        [SerializeField] private string _numStateName;
        public string NumStateName => _numStateName;

        [SerializeField] private string _boolStateName;
        public string BoolStateName => _boolStateName;

        [SerializeField] private MyMaths.ComparisonEquation _equation;
        public MyMaths.ComparisonEquation Equation => _equation;

        [SerializeField] private int _targetNumVal;
        public int TargetNumVal => _targetNumVal;
    }
}