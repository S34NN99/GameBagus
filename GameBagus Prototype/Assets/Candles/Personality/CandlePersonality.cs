using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(menuName = "Candle Personality")]
public class CandlePersonality : ScriptableObject {
    [SerializeField] private string[] _storyQuotes;
    public string[] StoryQuotes => _storyQuotes;

}
