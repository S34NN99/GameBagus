using UnityEngine;
using UnityEngine.Events;

public class ProjectEventTrigger : MonoBehaviour {
    [SerializeField] private ObservableProperty<float> monitoredProperty;

    [Range(0, 1f)]
    [SerializeField] private float _PropertyValueThreshold = 0.5f;
    public float PropertyValueThreshold => _PropertyValueThreshold;

    private bool hasPropertyValuePassedThreshold;

    private bool eventHasFired;

    private void Awake() {
        if (monitoredProperty != null) {
            monitoredProperty.OnValueUpdated.AddListener(checkTriggerAction);

            void checkTriggerAction(float oldVal, float newVal) {
                if (newVal >= PropertyValueThreshold) {
                    if (!hasPropertyValuePassedThreshold) {
                        hasPropertyValuePassedThreshold = true;
                        monitoredProperty.OnValueUpdated.RemoveListener(checkTriggerAction);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Allows the event's trigger to be delayed until the next time this function is called, regardless of when the value changed.
    /// </summary>
    /// <returns></returns>
    public bool GetTrigger() {
        if (hasPropertyValuePassedThreshold && !eventHasFired) {
            eventHasFired = true;
            return true;
        }

        return false;
    }
}
