
using UnityEngine;

/// <summary>
/// Caller Id for candles in the group chat.
/// </summary>
[CreateAssetMenu(menuName = "Candle/Profile")]
public class CandleProfile : ScriptableObject {
    [SerializeField] private string _profileName;
    public string ProfileName => _profileName;

    [SerializeField] private Sprite _profilePic;
    public Sprite ProfilePic => _profilePic;

}
