using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class PopUpManager : MonoBehaviour {
    [SerializeField] private GameObject backgroundPanel;

    private SequentialActionLoop _currentActionLoop;
    public SequentialActionLoop CurrentActionLoop { get => _currentActionLoop; set => _currentActionLoop = value; }

    public void FireNextActionIfAny() {
        if (CurrentActionLoop != null) {
            CurrentActionLoop.FireNextAction();
        }
    }

    public void ClearActionLoop() {
        CurrentActionLoop = null;
    }

    public void Show() {
        backgroundPanel.SetActive(true);

        Time.timeScale = 0;
    }

    public void Hide() {
        ClearActionLoop();

        backgroundPanel.SetActive(false);

        Time.timeScale = 1;
    }
}
