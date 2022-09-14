
using UnityEngine;

/// <summary>
/// Caller Id for candles in the group chat.
/// </summary>
[CreateAssetMenu(menuName = "Candle/Profile")]
public class CandleProfile : ScriptableObject {
    [SerializeField] private string _profileName;
    public string ProfileName
    {
        get => _profileName;
        set { _profileName = value; }
    }
    
    [SerializeField] private Sprite _profilePic;
    public Sprite ProfilePic => _profilePic;

    [SerializeField] private Color _profilePicTint;
    public Color ProfilePicTint => _profilePicTint;

    [SerializeField] private bool _overlayInitials;
    public bool OverlayInitials => _overlayInitials;

    [SerializeField] private string _initials;
    public string Initials
    {
        get => _initials;
        set { _initials = value; }
    }
}
