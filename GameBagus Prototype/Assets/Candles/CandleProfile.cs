
using UnityEngine;

[System.Serializable]
public class CandleProfile {
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

    [SerializeField] private CandleStyle _style;
    public CandleStyle Style {
        get => _style;
        set { _style = value; }
    }
}
