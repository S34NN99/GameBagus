
using UnityEngine;

using System.Collections.Generic;
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

    public State CreateState() => new(this);

    public void Randomise(Candle candle, State state) {
        string candleName = Random.Range(0, 2) == 1 ? state.NextFemaleName : state.NextMaleName;

        //candle.candleStats.updateNameCallback.Invoke(candleName);
        //candle.Stats.updateNameCallback.Invoke(candleName);
        candle.Skin = state.NextSkin;

        //CandleSpeech candleSpeech = candle.GetComponent<CandleSpeech>();
        //candleSpeech.ShowDialog("Hi, I'm " + candleName);

        //CandleStory candleStory = candle.GetComponent<CandleStory>();
        //candleStory.Personality = personalities.Random(prng);

        candle.Profile = new() {
            ProfileName = candleName,
            Initials = string.Join('.', candleName.Split(' ').Select(x => x.Substring(0, 1))),
            ProfilePic = state.NextProfilePic,
            Style = state.NextStyle,
        };
    }

    public class State {
        public List<int> FemaleNamePool { get; private set; }
        public List<int> MaleNamePool { get; private set; }
        public List<int> ProfilePicPool { get; private set; }
        public List<int> CandleStylePool { get; private set; }
        public List<int> CandleSkinPool { get; private set; }

        private CandleRandomiser randomiser;

        public State(CandleRandomiser randomiser) {
            this.randomiser = randomiser;

            FemaleNamePool = new List<int>(randomiser.femaleNames.Length);
            ResetPool(FemaleNamePool);

            MaleNamePool = new List<int>(randomiser.maleNames.Length);
            ResetPool(MaleNamePool);

            ProfilePicPool = new List<int>(randomiser.profilePics.Length);
            ResetPool(ProfilePicPool);

            CandleStylePool = new List<int>(randomiser.styles.Length);
            ResetPool(CandleStylePool);

            CandleSkinPool = new List<int>(randomiser.skins.Length);
            ResetPool(CandleSkinPool);

        }

        public string NextFemaleName => randomiser.femaleNames[GetRandomIndexFromPool(FemaleNamePool)];
        public string NextMaleName => randomiser.maleNames[GetRandomIndexFromPool(MaleNamePool)];

        public Sprite NextProfilePic => randomiser.profilePics[GetRandomIndexFromPool(ProfilePicPool)];
        public CandleStyle NextStyle => randomiser.styles[GetRandomIndexFromPool(CandleStylePool)];
        public CandleSkin NextSkin => randomiser.skins[GetRandomIndexFromPool(CandleSkinPool)];

        private int GetRandomIndexFromPool(List<int> pool) {
            if (!pool.Any()) {
                ResetPool(pool);
            }

            int selectedIndex = pool.Random();
            pool.Remove(selectedIndex);

            return selectedIndex;
        }

        private void ResetPool(List<int> pool) {
            for (int i = 0; i < pool.Capacity; i++) {
                pool.Add(i);
            }
        }
    }
}
