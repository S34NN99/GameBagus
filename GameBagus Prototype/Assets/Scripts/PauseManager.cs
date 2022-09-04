using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PauseManager : MonoBehaviour {
    private bool isPaused;

    public void Pause() {
        Time.timeScale = 0;
        isPaused = true;
    }

    public void Resume() {
        Time.timeScale = 1;
        isPaused = false;
    }

    public void TogglePause() {
        if (isPaused) {
            Resume();
        } else {
            Pause();
        }
    }

    public void ShowPauseMenu() {

    }
}
