
using UnityEngine;

[CreateAssetMenu(menuName = "Chat Profile")]
public class ChatProfile : ScriptableObject {
    [SerializeField] private string _profileName;
    public string ProfileName => _profileName;

    [SerializeField] private Sprite _profilePic;
    public Sprite ProfilePic => _profilePic;

}
