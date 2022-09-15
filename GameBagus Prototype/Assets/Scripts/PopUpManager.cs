using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PopUpManager : MonoBehaviour {
    [SerializeField] private GameObject backgroundPanel;

    [SerializeField] private SequentialActionLoop _currentActionLoop;
    public SequentialActionLoop CurrentActionLoop { get => _currentActionLoop; set => _currentActionLoop = value; }

    public void FireNextActionIfAny() {
        if (CurrentActionLoop != null) {
            CurrentActionLoop.FireNextAction();
        }
    }

    public void ClearActionLoop() {
        CurrentActionLoop = null;
    }

    public void ShowPopup(GameObject popup) {
        popup.gameObject.SetActive(true);
        popup.gameObject.TryGetComponent(out _currentActionLoop);

        backgroundPanel.SetActive(true);

    }
}
