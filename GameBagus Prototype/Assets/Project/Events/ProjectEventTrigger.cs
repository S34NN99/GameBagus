using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditorInternal;

#endif

public class ProjectEventTrigger : MonoBehaviour {
    [System.Serializable]
    private class Trigger {
        [SerializeField] public BoolProperty condition;
        [SerializeField] public bool isNot;

        //public bool Value => isNot ? (!condition.Value) : condition.Value;
        public bool Value => isNot ^ condition.Value;
    }

    [SerializeField] private Trigger[] conditions;

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
        int counter = 0;
        foreach (var condition in conditions) {
            print(condition.Value + "   " + counter + "     " + condition.condition.Value + "     not  " + condition.isNot);
            counter++;
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
