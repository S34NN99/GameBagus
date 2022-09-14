
using UnityEngine;

[CreateAssetMenu(menuName = "Candle/Randomiser")]
public class CandleRandomiser : ScriptableObject {
    [SerializeField] private GameObject[] candleTemplates;
    [SerializeField] private CandleProfile[] candleProfiles;
    [SerializeField] private CandleSkin[] candleSkins;
    [SerializeField] private CandlePersonality[] candlePersonalities;



    [Space]
    [SerializeField]
    private string[] femaleNames = {
        "Elisa",
        "Ashley",
        "Nicole",
        "Sarah",
        "Nadia",
    };

    [SerializeField]
    private string[] maleNames = {
        "Joe",
        "Brian",
        "Adam",
        "Jason",
        "Steven",
    };

    public void Randomise(Candle candle, System.Random prng) {
        string candleName = (prng.Next(0, 2) == 1 ? femaleNames : maleNames).Random(prng);

        //candle.candleStats.updateNameCallback.Invoke(candleName);
        candle.Stats.updateNameCallback.Invoke(candleName);
        candle.Skin = candleSkins.Random(prng);

        CandleSpeech candleSpeech = candle.GetComponent<CandleSpeech>();
        //candleSpeech.ShowDialog("Hi, I'm " + candleName);

        CandleStory candleStory = candle.GetComponent<CandleStory>();
        candleStory.Personality = candlePersonalities.Random(prng);
    }

    [System.Serializable]
    public class CandleProfile {

        [SerializeField] private string _profileName;
        public string ProfileName {
            get => _profileName;
            set { _profileName = value; }
        }

        [SerializeField] private Sprite _profilePic;
        public Sprite ProfilePic => _profilePic;

        [SerializeField] private Color _profilePicTint;
        public Color ProfilePicTint => _profilePicTint;

        [SerializeField] private bool _overlayInitials;
        public bool OverlayInitials => _overlayInitials;

        [SerializeField] private string _initials;
        public string Initials {
            get => _initials;
            set { _initials = value; }
        }

        [SerializeField] private Color _color;
        public Color Color => _color;
    }
}
