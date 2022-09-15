
using UnityEngine;

/// <summary>
/// Caller Id for candles in the group chat.
/// </summary>
[CreateAssetMenu(menuName = "Candle/Profile")]
public class CandleProfile_1 : ScriptableObject {
    [SerializeField] private string _profileName;
    public string ProfileName {
        get => _profileName;
        set { _profileName = value; }
    }

    [SerializeField] private Sprite _profilePic;
    public Sprite ProfilePic {
        get => _profilePic;
        set { _profilePic = value; }
    }

    [SerializeField] private string _initials;
    public string Initials {
        get => _initials;
        set { _initials = value; }
    }

    [SerializeField] private Color _profilePicTint;
    public Color ProfilePicTint {
        get => _profilePicTint;
        set { _profilePicTint = value; }
    }
}

