
using UnityEngine;

[CreateAssetMenu(menuName = "Ending/Special Case")]
public class SpecialCaseEnding : ScriptableObject {

    [SerializeField] private StateTrigger[] _triggers;
    public StateTrigger[] Triggers => _triggers;

    [TextArea(3, 10)]
    [SerializeField] private string[] _pages;
    public string[] Pages => _pages;

    public bool CheckIfEndingIsFired(MultipleEndingsSystem mes) {
        foreach (var trigger in Triggers) {
            if (!trigger.CheckStates(mes)) {
                return false;
            }
        }
        return true;
    }

    [System.Serializable]
    public class StateTrigger {
        [SerializeField] private string _numStateName;
        public string NumStateName => _numStateName;

        [SerializeField] private int _targetNumVal;
        public int TargetNumVal => _targetNumVal;

        [SerializeField] private MyMaths.ComparisonEquation _equation;
        public MyMaths.ComparisonEquation Equation => _equation;

        public bool CheckStates(MultipleEndingsSystem mes) {
            int stateVal = mes.GetState(NumStateName);

            return MyMaths.CompareIntegers(Equation, stateVal, TargetNumVal);
        }
    }
}
