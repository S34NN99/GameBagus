using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(menuName = "Ending/Mix and Match")]
public class MixAndMatchEnding : ScriptableObject {
    [SerializeField] private string _numStateName;
    public string NumStateName => _numStateName;

    [SerializeField] private StateTrigger[] _triggers;
    public StateTrigger[] Triggers => _triggers;

    public IList<string> FindPages(MultipleEndingsSystem mes) {
        List<string> pages = new();
        foreach (var trigger in Triggers) {
            int stateVal = mes.GetState(NumStateName);
            if (stateVal >= trigger.StartRange && stateVal < trigger.EndRange) {
                pages.AddRange(trigger.Pages);
            }
        }

        return pages;
    }

    [System.Serializable]
    public class StateTrigger {
        [Tooltip("Inclusive lower bound.")]
        [SerializeField] private int _startRange;
        /// <summary>
        /// Inclusive lower bound.
        /// </summary>
        public int StartRange => _startRange;

        [Tooltip("Inclusive upper bound.")]
        [SerializeField] private int _endRange;
        /// <summary>
        /// Exclusive upper bound.
        /// </summary>
        public int EndRange => _endRange;

        [TextArea(3, 20)]
        [SerializeField] private string[] _pages;
        public string[] Pages => _pages;
    }
}
