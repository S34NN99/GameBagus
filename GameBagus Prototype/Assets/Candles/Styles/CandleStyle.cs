
using UnityEngine;

[CreateAssetMenu(menuName = "Candle/Style")]
public class CandleStyle : ScriptableObject {
    [SerializeField] private Sprite _candleStand;
    public Sprite CandleStand => _candleStand;

    [SerializeField] private Color _profilePicTint;
    public Color ProfilePicTint => _profilePicTint;
}
