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
    }

    private void Update() {
        if (!bgMusicPlayer.isPlaying) {
            bgMusicPlayer.clip = bgMusicClips[Random.Range(0, bgMusicClips.Length)];
            bgMusicPlayer.Play();
        }
    }
}
