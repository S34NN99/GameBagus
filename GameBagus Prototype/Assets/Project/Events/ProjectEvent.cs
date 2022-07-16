
using UnityEngine;
using UnityEngine.Events;

using System.Collections.Generic;

[RequireComponent(typeof(ProjectEventTrigger))]
public class ProjectEvent : MonoBehaviour {
    [SerializeField] private ProjectEventTrigger trigger;

    [Header("Content")]
    [SerializeField] private string _title;
    public string Title => ObservableParameter.ConvertToRuntimeText(_title);

    [SerializeField] private string _mainBody;
    public string MainBody => ObservableParameter.ConvertToRuntimeText(_mainBody);

    [SerializeField] private string _closing;
    public string Closing => ObservableParameter.ConvertToRuntimeText(_closing);

    [SerializeField] private ProjectEventAction[] _availableActions;
    public IReadOnlyList<ProjectEventAction> AvailableActions => _availableActions;

    public bool ReadyToFire { get; private set; }

#if UNITY_EDITOR
    // auto assign trigger
    private void OnValidate() {
        if (trigger == null) {
            if (!TryGetComponent(out trigger)) {
                trigger = gameObject.AddComponent<ProjectEventTrigger>();
            }
        }
    }
#endif

    private void Update() {
        if (trigger.GetTrigger()) {
            ReadyToFire = true;
        }
    }

    public void FireEvent(ProjectEventPanel eventPanel) {
        ReadyToFire = false;


    }
}
