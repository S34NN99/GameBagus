using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour {
    private bool isPaused;
    private bool isBgmMuted;
    private bool isSfxMuted;

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

    public void ToggleBgmMute() {
        isBgmMuted = !isBgmMuted;
    }

    public void ToggleSfxMute() {
        isSfxMuted = !isSfxMuted;
    }

    public void ReturnToMainMenu() {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
