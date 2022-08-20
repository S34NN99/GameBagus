using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public class CandleStats {
    public float MaxHP;
    public float HP;
    public float Power;
    public float RegenerateHP;
    public float Decay;

    [Header("Crunch")]
    public float AdditionalPower;
    public float AdditionalDecay;

    [Header("Mood")]
    // x = power, y = decay
    public List<Vector2Int> Multiplier;
    public List<int> MoodThreshold;

    [Header("Beta Testing")]
    public UnityEvent<string> updateNameCallback;
    public TextMeshProUGUI HPText;
    public TextMeshProUGUI CurrentMoodState;
    public TextMeshProUGUI CurrentWorkingState;

}

public class Candle : MonoBehaviour, IEntity {
    private const float burnoutCandleGraphicsPos = -192f;

    public ParticleSystem firePs;
    public CandleStats candleStats;

    public StateMachine SM { get; private set; }
    public Candle currCandle { get; set; }

    [SerializeField] private CandleSkin _skin;
    public CandleSkin Skin {
        get => _skin;
        set {
            _skin = value;
            UpdateHeadImageCallback.Invoke(_skin.GetFacialExpression(SM.moodState));
            UpdateBodyImageCallback.Invoke(_skin.CandleBase);
        }
    }

    [SerializeField] private CandleProfile _profile;
    public CandleProfile Profile { get => _profile; set => _profile = value; }

    [SerializeField] private RectTransform graphicsParentTransform;
    private float updateTime = 0;

    [SerializeField] private UnityEvent<Sprite> _updateHeadImageCallback;
    public UnityEvent<Sprite> UpdateHeadImageCallback => _updateHeadImageCallback;

    [SerializeField] private UnityEvent<Sprite> _updateBodyImageCallback;
    public UnityEvent<Sprite> UpdateBodyImageCallback => _updateBodyImageCallback;

    public UnityEvent<Candle> onDeath;

    private void Awake() {
        currCandle = this;
        SM = new StateMachine(this);
        SM.owner = this;

        SM.SetWorkingState(new W_Working());
        SM.SetMoodState(new M_Happy());
        candleStats.HP = candleStats.MaxHP;
    }

    public void Update() {
        //DisplayText();
        updateTime = Time.deltaTime / 1;
    }

    public void Decay() {
        candleStats.HP -= (candleStats.Decay + candleStats.Multiplier[SM.moodState.CurrentIndex].y) * updateTime;
    }
    public void CrunchDecay() {
        candleStats.HP -= (candleStats.Decay + candleStats.AdditionalDecay + candleStats.Multiplier[SM.moodState.CurrentIndex].y) * updateTime;
    }

    public void Work(Project pb) {
        // moodstate is null
        pb.currentProgress += (candleStats.Power + candleStats.Multiplier[SM.moodState.CurrentIndex].x) * updateTime;
        pb.UpdateVisuals();
    }

    public void CrunchWork(Project pb) {
        pb.currentProgress += (candleStats.Power + candleStats.AdditionalPower + candleStats.Multiplier[SM.moodState.CurrentIndex].x) * updateTime;
        pb.UpdateVisuals();
    }

    public void Regeneration() {
        if (candleStats.HP < candleStats.MaxHP)
            candleStats.HP += candleStats.RegenerateHP * updateTime;
    }

    public void Death() {
        // should be delayed so its not gonna affect anything
        Destroy(gameObject);

        GeneralEventManager.Instance.BroadcastEvent(AudioManager.OnCandleBurnoutEvent);
        onDeath.Invoke(this);
    }

    public void DisplayText() {
        Vector2 graphicsParentPos = graphicsParentTransform.anchoredPosition;
        graphicsParentPos.y = Mathf.Lerp(burnoutCandleGraphicsPos, 0, Mathf.InverseLerp(0, candleStats.MaxHP, candleStats.HP));
        graphicsParentTransform.anchoredPosition = graphicsParentPos;

        candleStats.HPText.text = candleStats.HP + "";
        candleStats.CurrentMoodState.text = SM.moodState.Name + " ";
        candleStats.CurrentWorkingState.text = SM.workingState.Name + " ";
    }

    public void SetFireSpeed(float speed) {
        var main = firePs.main;
        main.simulationSpeed = speed;
    }
}
