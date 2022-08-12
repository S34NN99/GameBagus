using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(menuName = "Candle/Candle Skin")]
public class CandleSkin : ScriptableObject {
    [SerializeField] private Sprite _candleBase;
    public Sprite CandleBase => _candleBase;

    [SerializeField] private Sprite _Head_Happy;
    public Sprite Head_Happy => _Head_Happy;

    [SerializeField] private Sprite _Head_Neutral;
    public Sprite Head_Neutral => _Head_Neutral;

    [SerializeField] private Sprite _Head_Sad;
    public Sprite Head_Sad => _Head_Sad;

    public Sprite GetFacialExpression(MoodState state) {
        return state.Name switch {
            "Happy" => Head_Happy,
            "Neutral" => Head_Neutral,
            "Sad" => Head_Sad,
            _ => Head_Happy,
        };
    }
}
