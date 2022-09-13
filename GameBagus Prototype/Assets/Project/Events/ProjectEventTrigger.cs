using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;


public class ProjectEventTrigger : MonoBehaviour {
    [System.Serializable]
    public class TriggerCondition {
        [SerializeField] private BoolProperty condition;
        [SerializeField] private bool isNot;

        public bool Value => isNot ^ condition.Value;

    }

    [SerializeField] private TriggerCondition[] conditions;

    [SerializeField] private bool triggerOnce = true;

    [Space]
    [Tooltip("You guys can leave a comment about what the event should do")]
    [SerializeField] private string remarksForTech;

    private bool hasEventFired;

    /// <summary>
    /// Allows the event's trigger to be delayed until the next time this function is called, regardless of when the value changed.
    /// </summary>
    /// <returns></returns>
    public bool GetTrigger() {
        if (hasEventFired && triggerOnce) return false;

        bool allConditionsMet = true;
        foreach (var condition in conditions) {
            if (!condition.Value) {
                allConditionsMet = false;
                break;
            }
        }

        if (allConditionsMet) {
            hasEventFired = true;
            return true;
        }

        return false;
    }
}
