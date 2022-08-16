using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;


public class ProjectEventAction : MonoBehaviour {
    [SerializeField] [RuntimeString] private string _buttonTitleText;
    public string ButtonTitleText => _buttonTitleText;

    [SerializeField] [RuntimeString] private string _resultText;
    public string ResultText => _resultText;

    [SerializeField] private UnityEvent onActionSelected;

    public void Fire() {
        onActionSelected.Invoke();
    }
}
