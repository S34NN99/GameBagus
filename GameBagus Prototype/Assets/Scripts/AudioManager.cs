using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class AudioManager : MonoBehaviour {
    public const string OnButtonPickedUp = "On Button Picked Up";
    public const string OnButtonDropped = "On Button Dropped";

    public const string OnCandleCrunchEvent = "On Candle Crunch";
    public const string NearingCandleBurnoutEvent = "Nearing Candle Burnout";
    public const string OnCandleBurnoutEvent = "On Candle Burnout";

    public const string NearingProjectFinishedEvent = "Nearing Project Finished";
    public const string OnProjectFinishedEvent = "On Project Finished";

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
    [SerializeField] private AudioClip[] bgMusicClips;

    [Space()]
    [Header("Audio Sources")]
    [SerializeField] private AudioSource bgMusicPlayer;
    [SerializeField] private AudioSource uiSfxPlayer;
    [SerializeField] private AudioSource candleSfxPlayer;
    [SerializeField] private AudioSource candleNearDeathSfxPlayer;
    [SerializeField] private AudioSource projectSfxPlayer;

    private bool firstCandleNearingBurnout = false;

    private void Start() {
        GameEventManager.Instance.CreateNewEvent(OnButtonPickedUp);
        GameEventManager.Instance.SubscribeToEvent(OnButtonPickedUp, () => {
            uiSfxPlayer.clip = onButtonPickedUp;
            uiSfxPlayer.Play();
        });

        GameEventManager.Instance.CreateNewEvent(OnButtonDropped);
        GameEventManager.Instance.SubscribeToEvent(OnButtonDropped, () => {
            uiSfxPlayer.clip = onButtonDropped;
            uiSfxPlayer.Play();
        });




        GameEventManager.Instance.CreateNewEvent(OnCandleCrunchEvent);
        GameEventManager.Instance.SubscribeToEvent(OnCandleCrunchEvent, () => {
            candleSfxPlayer.clip = onCandleCrunch;
            candleSfxPlayer.Play();
        });

        GameEventManager.Instance.CreateNewEvent(NearingCandleBurnoutEvent);
        GameEventManager.Instance.SubscribeToEvent(NearingCandleBurnoutEvent, () => {
            if (!firstCandleNearingBurnout) {
                candleNearDeathSfxPlayer.clip = nearingCandleBurnout;
                candleNearDeathSfxPlayer.Play();
                firstCandleNearingBurnout = true;
            }
        });

        GameEventManager.Instance.CreateNewEvent(OnCandleBurnoutEvent);
        GameEventManager.Instance.SubscribeToEvent(OnCandleBurnoutEvent, () => {
            candleSfxPlayer.clip = onCandleSnuffedClips[Random.Range(0, onCandleSnuffedClips.Length)];
            candleSfxPlayer.Play();
        });




        GameEventManager.Instance.CreateNewEvent(NearingProjectFinishedEvent);
        GameEventManager.Instance.SubscribeToEvent(NearingProjectFinishedEvent, () => {
            projectSfxPlayer.clip = nearingProjectFinished;
            projectSfxPlayer.loop = true;
            projectSfxPlayer.Play();
        });

        GameEventManager.Instance.CreateNewEvent(OnProjectFinishedEvent);
        GameEventManager.Instance.SubscribeToEvent(OnProjectFinishedEvent, () => {
            projectSfxPlayer.clip = onProjectFinished;
            projectSfxPlayer.loop = false;
            projectSfxPlayer.Play();
        });
    }

    private void Update() {
        //if (!bgMusicPlayer.isPlaying) {
        //    bgMusicPlayer.clip = bgMusicClips[Random.Range(0, bgMusicClips.Length)];
        //    bgMusicPlayer.Play();
        //}
    }
}
