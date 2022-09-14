﻿
using UnityEngine;
using UnityEngine.Events;

using UnityEngine.UI;
using TMPro;

public class ChatMessage : MonoBehaviour {
    [SerializeField] private UnityEvent<Sprite> updateProfilePicCallback;
    [SerializeField] private UnityEvent<string> updateProfileNameCallback;
    [SerializeField] private UnityEvent<Color> updateProfileTintCallback;
    [SerializeField] private UnityEvent<string> updateInitialsOverlayCallback;
    [SerializeField] private UnityEvent<string> updateMessageCallback;

    public void DisplayMessage(CandleProfile profile, string message) {
        updateProfilePicCallback.Invoke(profile.ProfilePic);
        updateProfileNameCallback.Invoke(profile.ProfileName);
        updateProfileTintCallback.Invoke(profile.Style.ProfilePicTint);
        updateMessageCallback.Invoke(message);
        updateInitialsOverlayCallback.Invoke(profile.Initials);
    }
}
