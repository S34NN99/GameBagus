using System.Collections;
using System.Collections.Generic;

using UnityEngine;

/// <summary>
/// Has the Update and OnValidate functions implemented
/// </summary>
/// <typeparam name="UiDisplay"></typeparam>
public abstract class GameEventBase<UiDisplay> : MonoBehaviour where UiDisplay : MonoBehaviour {
    [SerializeField] protected ProjectEventTrigger trigger;

    [Space]
    [SerializeField] protected UiDisplay eventDisplay;

    [Space]
    [Tooltip("You guys can leave a comment about what the event should do")]
    [SerializeField] private string remarksForTech;

#if UNITY_EDITOR
    // auto assign trigger
    protected virtual void OnValidate() {
        if (trigger == null) {
            if (!TryGetComponent(out trigger)) {
                trigger = gameObject.AddComponent<ProjectEventTrigger>();
            }
        }
    }
#endif

    protected virtual void Update() {
        if (trigger.GetTrigger()) {
            TriggerEvent(eventDisplay);
        }
    }

    protected abstract void TriggerEvent(UiDisplay display);
}
