
using UnityEngine;
using System.Linq;

[CreateAssetMenu(menuName = "Candle/Randomiser")]
public class CandleRandomiser : ScriptableObject {
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

    [SerializeField] private Sprite[] profilePics;

    [SerializeField] private CandleStyle[] styles;
    [SerializeField] private CandleSkin[] skins;
    //[SerializeField] private CandlePersonality[] personalities;



    public void Randomise(Candle candle, System.Random prng) {
        string candleName = (prng.Next(0, 2) == 1 ? femaleNames : maleNames).Random(prng);

        //candle.candleStats.updateNameCallback.Invoke(candleName);
        //candle.Stats.updateNameCallback.Invoke(candleName);
        candle.Skin = skins.Random(prng);

        //CandleSpeech candleSpeech = candle.GetComponent<CandleSpeech>();
        //candleSpeech.ShowDialog("Hi, I'm " + candleName);

        //CandleStory candleStory = candle.GetComponent<CandleStory>();
        //candleStory.Personality = personalities.Random(prng);

        candle.Profile = new() {
            ProfileName = candleName,
            Initials = string.Join('.', candleName.Split(' ').Select(x => x.Substring(0, 1))),
            ProfilePic = profilePics.Random(prng),
            Style = styles.Random(prng),
        };
    }
}

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

public class MyRandom : System.Random {
    public override int Next(int maxValue) {
        return base.Next(maxValue);
    }
}