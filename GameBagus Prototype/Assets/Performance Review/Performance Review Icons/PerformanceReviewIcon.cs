using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PerformanceReview/Icons")]
public class PerformanceReviewIcon : ScriptableObject
{
    [SerializeField] private Sprite _iconBase;
    public Sprite IconBase => _iconBase;

    [SerializeField] private Sprite _iconColour;
    public Sprite IconColour => _iconColour;
}
