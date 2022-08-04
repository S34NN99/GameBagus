using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public abstract class GameEventBase<UiDisplay> : MonoBehaviour where UiDisplay : MonoBehaviour {
    [SerializeField] protected ProjectEventTrigger trigger;

    [Header("Content")]
    [SerializeField] [RuntimeString] protected string _title;
    public string Title => ObservableVariable.ConvertToRuntimeText(_title);

    [SerializeField] [RuntimeString] protected string _mainBody;
    public string MainBody => ObservableVariable.ConvertToRuntimeText(_mainBody);

    [SerializeField] [RuntimeString] protected string _closing;
    public string Closing => ObservableVariable.ConvertToRuntimeText(_closing);

    [Space]
    [SerializeField] protected UiDisplay eventDisplay;

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
            DisplayEvent(eventDisplay);
        }
    }

    protected abstract void DisplayEvent(UiDisplay display);
}
