using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(menuName = "Candle/Candle Skin")]
public class CandleSkin : ScriptableObject {
    [SerializeField] private Sprite _candleBase;
    public Sprite CandleBase => _candleBase;

    [SerializeField] private Sprite _wick;
    public Sprite Wick => _wick;

    [SerializeField] private Sprite _Head_Happy;
    public Sprite Img_Happy => _Head_Happy;

    [SerializeField] private Sprite _Head_Neutral;
    public Sprite Img_Neutral => _Head_Neutral;

    [SerializeField] private Sprite _Head_Sad;
    public Sprite Img_Sad => _Head_Sad;

    public Sprite GetFacialExpression(MoodState state) {
        return state.Name switch {
            "Happy" => Img_Happy,
            "Neutral" => Img_Neutral,
            "Sad" => Img_Sad,
            _ => Img_Happy,
        };
    }
}
