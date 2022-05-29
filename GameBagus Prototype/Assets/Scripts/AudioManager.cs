using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class AudioManager : MonoBehaviour {
    public const string NearingCandleBurnoutEvent = "Nearing Candle Burnout";
    public const string OnCandleBurnoutEvent = "On Candle Burnout";
    public const string OnProjectFinishedEvent = "On Project Finished";

    [Header("Clips")]
    [SerializeField] private AudioClip nearingCandleBurnout;
    [SerializeField] private AudioClip onCandleBurnout;
    [SerializeField] private AudioClip onProjectFinished;

    [SerializeField] private AudioSource audioPlayer;

    private void Start() {
        GameEventManager.Instance.CreateNewEvent(NearingCandleBurnoutEvent);
        GameEventManager.Instance.SubscribeToEvent(NearingCandleBurnoutEvent, () => {
            audioPlayer.clip = nearingCandleBurnout;
            audioPlayer.Play();
        });

        GameEventManager.Instance.CreateNewEvent(OnCandleBurnoutEvent);
        GameEventManager.Instance.SubscribeToEvent(OnCandleBurnoutEvent, () => {
            audioPlayer.clip = onCandleBurnout;
            audioPlayer.Play();
        });

        GameEventManager.Instance.CreateNewEvent(OnProjectFinishedEvent);
        GameEventManager.Instance.SubscribeToEvent(OnProjectFinishedEvent, () => {
            audioPlayer.clip = onProjectFinished;
            audioPlayer.Play();
        });
    }
}
