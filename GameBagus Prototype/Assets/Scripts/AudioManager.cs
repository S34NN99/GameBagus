using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class AudioManager : MonoBehaviour {

    [System.Serializable]
    class LevelBG {
        public bool playFirstAudio;
        public AudioClip[] playlist;
    }

    public const string OnButtonPickedUp = "On Button Picked Up";
    public const string OnButtonDropped = "On Button Dropped";

    public const string OnCandleCrunchEvent = "On Candle Crunch";
    public const string NearingCandleBurnoutEvent = "Nearing Candle Burnout";
    public const string OnCandleBurnoutEvent = "On Candle Burnout";

    public const string NearingProjectFinishedEvent = "Nearing Project Finished";
    public const string OnProjectFinishedEvent = "On Project Finished";
    public const string OnProjectPrologue = "On Project Prologue";
    public const string OnProjectPrologueClosed = "On Project Prologue Closed";

    public const string OnProjectStart = "On Project Start";
    public const string OnCallEvent = "On Call Event";
    public const string OnCallEventEnded = "On Call Event Ended";

    public const string TypingEffect = "Typing Effect";
    public const string TypingEffectEnd = "Typing Effect End";

    public const string ToggleBgmEvent = "Toggle Bgm";
    public const string ToggleSfxEvent = "Toggle Sfx";

    [SerializeField] private bool isMainMenu;
    [SerializeField] private int currentLevel;
    public int CurrentLevel => currentLevel;

    [SerializeField] private List<LevelBG> bgMusicClips;

    [Header("Clips")]
    [Space()]
    [SerializeField] private AudioClip onButtonPickedUp;
    [SerializeField] private AudioClip onButtonDropped;

    [Space()]
    [SerializeField] private AudioClip onCandleCrunch;
    [SerializeField] private AudioClip nearingCandleBurnout;
    [SerializeField] private AudioClip[] onCandleSnuffedClips;

    [Space()]
    [SerializeField] private AudioClip nearingProjectFinished;
    [SerializeField] private AudioClip onProjectFinished;

    [Space()]
    [SerializeField] private AudioClip onPrologue;
    [SerializeField] private AudioClip[] onProjectClips;

    [Space()]
    [SerializeField] private AudioClip onCallEvent;

    [Space()]
    [SerializeField] private AudioClip[] typingEffects;

    [Space()]
    [Header("Audio Sources")]
    [SerializeField] private AudioSource bgMusicPlayer;
    [SerializeField] private AudioSource uiSfxPlayer;
    [SerializeField] private AudioSource candleSfxPlayer;
    [SerializeField] private AudioSource candleNearDeathSfxPlayer;
    [SerializeField] private AudioSource projectSfxPlayer;
    [SerializeField] private AudioSource prologueSfxPlayer;
    [SerializeField] private AudioSource ambientMusicPlayer;
    [SerializeField] private AudioSource callMusicPlayer;
    [SerializeField] private AudioSource typingPlayer;

    private bool firstCandleNearingBurnout = false;

    private void Start() {
        GeneralEventManager.Instance.StartListeningTo(OnButtonPickedUp, () => {
            uiSfxPlayer.clip = onButtonPickedUp;
            uiSfxPlayer.Play();
        });

        GeneralEventManager.Instance.StartListeningTo(OnButtonDropped, () => {
            uiSfxPlayer.clip = onButtonDropped;
            uiSfxPlayer.Play();
        });

        GeneralEventManager.Instance.StartListeningTo(OnCandleCrunchEvent, () => {
            candleSfxPlayer.clip = onCandleCrunch;
            candleSfxPlayer.Play();
        });

        GeneralEventManager.Instance.StartListeningTo(NearingCandleBurnoutEvent, () => {
            if (!firstCandleNearingBurnout) {
                candleNearDeathSfxPlayer.clip = nearingCandleBurnout;
                candleNearDeathSfxPlayer.Play();
                firstCandleNearingBurnout = true;
            }
        });

        GeneralEventManager.Instance.StartListeningTo(OnCandleBurnoutEvent, () => {
            candleSfxPlayer.clip = onCandleSnuffedClips[Random.Range(0, onCandleSnuffedClips.Length)];
            candleSfxPlayer.Play();
        });

        GeneralEventManager.Instance.StartListeningTo(NearingProjectFinishedEvent, () => {
            projectSfxPlayer.clip = nearingProjectFinished;
            projectSfxPlayer.loop = true;
            projectSfxPlayer.Play();
        });

        GeneralEventManager.Instance.StartListeningTo(OnProjectFinishedEvent, () => {
            projectSfxPlayer.clip = onProjectFinished;
            projectSfxPlayer.loop = false;
            projectSfxPlayer.Play();
        });

        GeneralEventManager.Instance.StartListeningTo(OnProjectPrologue, () => {
            prologueSfxPlayer.clip = onPrologue;
            prologueSfxPlayer.loop = true;
            prologueSfxPlayer.Play();
        });

        GeneralEventManager.Instance.StartListeningTo(OnProjectStart, () => {
            prologueSfxPlayer.Stop();

            ambientMusicPlayer.clip = onProjectClips[Random.Range(0, onProjectClips.Length)];
            ambientMusicPlayer.loop = true;
            ambientMusicPlayer.Play();
        });

        GeneralEventManager.Instance.StartListeningTo(OnCallEvent, () => {
            callMusicPlayer.clip = onCallEvent;
            callMusicPlayer.Play();
        });

        GeneralEventManager.Instance.StartListeningTo(OnCallEventEnded, () => {
            callMusicPlayer.Stop();
        });

        GeneralEventManager.Instance.StartListeningTo(TypingEffect, () => {
            typingPlayer.clip = typingEffects[Random.Range(0, typingEffects.Length)];
            typingPlayer.loop = true;
            typingPlayer.Play();
        });

        GeneralEventManager.Instance.StartListeningTo(TypingEffectEnd, () => {
            typingPlayer.Stop();
        });

        GeneralEventManager.Instance.StartListeningTo(ToggleBgmEvent, ToggleBgm);
        GeneralEventManager.Instance.StartListeningTo(ToggleSfxEvent, ToggleSfx);

        CheckBgmAndSfxMute();

        if (!isMainMenu) {
            GeneralEventManager.Instance.BroadcastEvent(TypingEffect);
            GeneralEventManager.Instance.BroadcastEvent(OnProjectPrologue);
        }
    }

    private void Update() {

        if (!bgMusicPlayer.isPlaying) {
            if (bgMusicClips[CurrentLevel].playFirstAudio) {
                bgMusicPlayer.clip = bgMusicClips[CurrentLevel].playlist[0];
            } else {
                int length = bgMusicClips[CurrentLevel].playlist.Length - 1;
                bgMusicPlayer.clip = bgMusicClips[CurrentLevel].playlist[Random.Range(1, length)];
            }

            bgMusicPlayer.Play();
        }
    }

    public void OnClickedSFX() {
        GeneralEventManager.Instance.BroadcastEvent(OnButtonPickedUp);
    }

    public void ToggleBgm() {
        print(PlayerPrefs.GetFloat("Bgm_Volume", 1));
        float volume = PlayerPrefs.GetFloat("Bgm_Volume", 1);
        if ( volume == 1f) {
            SetBgmVolume(0);
        } else {
            SetBgmVolume(1);
        }
    }

    public void ToggleSfx() {
        print(PlayerPrefs.GetFloat("Bgm_Volume", 1));
        float volume = PlayerPrefs.GetFloat("Sfx_Volume", 1);
        if ( volume == 1f) {
            SetSfxVolume(0);
        } else {
            SetSfxVolume(1);
        }
    }

    public void SetBgmVolume(float volume) {
        PlayerPrefs.SetFloat("Bgm_Volume", volume);
        CheckBgmAndSfxMute();
    }

    public void SetSfxVolume(float volume) {
        PlayerPrefs.SetFloat("Sfx_Volume", volume);
        CheckBgmAndSfxMute();
    }

    public void CheckBgmAndSfxMute() {
        float bgmVolume = PlayerPrefs.GetFloat("Bgm_Volume", 1);
        bgMusicPlayer.volume = bgmVolume;

        float sfxVolume = PlayerPrefs.GetFloat("Sfx_Volume", 1);
        uiSfxPlayer.volume = sfxVolume;
        candleSfxPlayer.volume = sfxVolume;
        candleNearDeathSfxPlayer.volume = sfxVolume;
        projectSfxPlayer.volume = sfxVolume;
        prologueSfxPlayer.volume = sfxVolume;
        ambientMusicPlayer.volume = sfxVolume;
        callMusicPlayer.volume = sfxVolume;
        typingPlayer.volume = sfxVolume;
    }
}
