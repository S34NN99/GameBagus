using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour {
    private bool isPaused;
    private bool isBgmMuted;
    private bool isSfxMuted;

    [SerializeField] private bool hasMenu;

    [SerializeField] private GameObject _muteBgmBtn;
    public GameObject MuteBgmBtn => _muteBgmBtn;

    [SerializeField] private GameObject _muteSfxBtn;
    public GameObject MuteSfxBtn => _muteSfxBtn;

    private void Awake() {

        //GeneralEventManager.Instance.StartListeningTo(AudioManager.ToggleBgmEvent, CheckButtons);
        //GeneralEventManager.Instance.StartListeningTo(AudioManager.ToggleSfxEvent, CheckButtons);
    }

    public void Pause() {
        Time.timeScale = 0;
        isPaused = true;
    }

    public void Resume() {
        Time.timeScale = 1;
        isPaused = false;
    }

    public void CheckButtons() {
        if (hasMenu) {
            float bgmVolume = PlayerPrefs.GetFloat("Bgm_Volume", 1);
            if (bgmVolume != 1) {
                MuteBgmBtn.GetComponent<Button>().onClick.Invoke();
            }

            float sfxVolume = PlayerPrefs.GetFloat("Sfx_Volume", 1);
            if (sfxVolume != 1) {
                MuteSfxBtn.GetComponent<Button>().onClick.Invoke();
            }
        }
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
        GeneralEventManager.Instance.BroadcastEvent(AudioManager.ToggleBgmEvent);
    }

    public void ToggleSfxMute() {
        isSfxMuted = !isSfxMuted;
        GeneralEventManager.Instance.BroadcastEvent(AudioManager.ToggleSfxEvent);
    }

    public void ReturnToMainMenu() {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
