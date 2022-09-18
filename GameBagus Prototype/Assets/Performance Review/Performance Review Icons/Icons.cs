using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Paper/Icons")]
public class Icons : ScriptableObject
{
    [SerializeField] private Sprite _iconBase;
    public Sprite IconBase => _iconBase;

    [SerializeField] private Sprite _iconColour;
    public Sprite IconColour => _iconColour;
}
