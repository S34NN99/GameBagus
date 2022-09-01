using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ProjectEventAction {
    [SerializeField] [RuntimeString] private string _buttonTitleText;
    public string ButtonTitleText => _buttonTitleText;

    [SerializeField] [RuntimeString] private string _resultText;
    public string ResultText => _resultText;

    [SerializeField] private UnityEvent _eventCallback;
    public UnityEvent EventCallback => _eventCallback;

    [Space]
    [Tooltip("You guys can leave a comment about what the event should do")]
    [SerializeField] private string remarksForTech;

    public ProjectEventAction(string buttonTitleText, string resultText, UnityEvent eventCallback) {
        _buttonTitleText = buttonTitleText;
        _resultText = resultText;
        _eventCallback = eventCallback;
    }
}
