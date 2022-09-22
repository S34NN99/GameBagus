using System.Collections.Generic;
using System.Linq;

using TMPro;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Candle : MonoBehaviour, IEntity {
    public ParticleSystem firePs;

    [SerializeField] private CandleStats _stats;
    public CandleStats Stats => _stats;

    public StateMachine SM { get; private set; }
    public Candle currCandle { get; set; }

    [SerializeField] private CandleSkin _skin;
    public CandleSkin Skin {
        get => _skin;
        set {
            _skin = value;
            UpdateWickImgCallback.Invoke(_skin.Wick);
            UpdateCandleImgCallback.Invoke(_skin.GetFacialExpression(SM.moodState));
        }
    }

    [SerializeField] private CandleProfile _profile;
    public CandleProfile Profile {
        get => _profile;
        set {
            _profile = value;
            UpdateStandImgCallback.Invoke(_profile.Style.CandleStand);
            UpdateNameCallback.Invoke(_profile.ProfileName);
        }
    }

    [SerializeField] private UnityEvent<Sprite> _updateWickImgCallback;
    public UnityEvent<Sprite> UpdateWickImgCallback => _updateWickImgCallback;

    [SerializeField] private UnityEvent<Sprite> _updateCandleImgCallback;
    public UnityEvent<Sprite> UpdateCandleImgCallback => _updateCandleImgCallback;

    [SerializeField] private UnityEvent<Sprite> _updateStandImgCallback;
    public UnityEvent<Sprite> UpdateStandImgCallback => _updateStandImgCallback;

    [SerializeField] private UnityEvent<string> _updateNameCallback;
    public UnityEvent<string> UpdateNameCallback => _updateNameCallback;

    [SerializeField] private UnityEvent<Candle> onDeath;
    [SerializeField] private UnityEvent<CandleProfile, string> showDialogCallback;

    [SerializeField] private UnityEvent<float> updateCandleDecay;

    private void Awake() {
        currCandle = this;
        SM = new(this);
        SM.owner = this;

        SM.SetWorkingState(new W_Working());
        SM.SetMoodState(new M_Happy());
        Stats.HpProp.Value = Stats.MaxHp;
    }

    private void Start() {
    }

    public void Update() {
        updateCandleDecay.Invoke(Mathf.InverseLerp(0, Stats.MaxHp, Stats.HpProp.Value));
    }

    public void Decay() {
        Stats.HpProp.Value -= Stats.CalculateDecay() * Time.deltaTime;
    }
    public void CrunchDecay() {
        Stats.HpProp.Value -= Stats.CalculateDecay() * Time.deltaTime;
    }

    public void Work(Project pb) {
        // moodstate is null
        pb.ProgressProp.Value += Stats.CalculateWorkDone() * Time.deltaTime;
    }

    public void CrunchWork(Project pb) {
        pb.ProgressProp.Value += Stats.CalculateWorkDone() * Time.deltaTime;
    }

    public void Regeneration() {
        if (Stats.HpProp.Value < Stats.MaxHp) {
            Stats.HpProp.Value -= Stats.CalculateDecay() * Time.deltaTime;
        }
    }

    public void Death() {
        // should be delayed so its not gonna affect anything
        Destroy(gameObject);

        GeneralEventManager.Instance.BroadcastEvent(AudioManager.OnCandleBurnoutEvent);
        onDeath.Invoke(this);
    }

    public void SetFireDetails(float speed, float size) {
        var main = firePs.main;
        main.simulationSpeed = speed;
        main.startSize = size;
    }

    public void ShowDialog(string dialogText) {
        showDialogCallback.Invoke(Profile, dialogText);
    }
}

[System.Serializable]
public class CandleStats {
    [SerializeField] private float _maxHp = 600;
    public float MaxHp {
        get => _maxHp;
        set {
            _maxHp = value;
            HpProp.Value = _maxHp;
        }
    }

    [SerializeField] private FloatProperty _hpProp;
    public FloatProperty HpProp => _hpProp;

    [SerializeField] private List<Modifier> powerMods;
    [SerializeField] private List<Modifier> decayMods;

    [SerializeField] private List<int> _moodThreshold;
    public IReadOnlyList<int> MoodThreshold => _moodThreshold;

    public UnityEvent<string> updateNameCallback;

    public float CalculateWorkDone() => CalculateModifierEffect(powerMods);
    public float CalculateDecay() => CalculateModifierEffect(decayMods);

    private static float CalculateModifierEffect(List<Modifier> modList) {
        if (!modList.Any()) return 0;

        float finalEffect = 0;
        int maxPriority = modList.Min(x => x.Priority);
        IEnumerable<Modifier> modifiers = modList.Where(x => x.Priority == maxPriority);
        foreach (var mod in modifiers) {
            if (mod.ModType == Modifier.Type.constant) {
                finalEffect += mod.Strength;
            }
        }
        foreach (var mod in modifiers) {
            if (mod.ModType == Modifier.Type.multiply) {
                finalEffect *= mod.Strength;
            }
        }

        return finalEffect;
    }

    public Modifier AddPowerModifier(int priority, Modifier.Type modType, float strength) => AddModifier(powerMods, priority, modType, strength);
    public Modifier AddDecayModifier(int priority, Modifier.Type modType, float strength) => AddModifier(decayMods, priority, modType, strength);

    public void RemovePowerModifier(Modifier powerMod) => powerMods.Remove(powerMod);
    public void RemoveDecayModifier(Modifier decayMod) => decayMods.Remove(decayMod);

    private static Modifier AddModifier(List<Modifier> modList, int priority, Modifier.Type modType, float strength) {
        Modifier modifier = new() {
            Priority = priority,
            ModType = modType,
            Strength = strength,
        };
        modList.Add(modifier);
        modifier.UnsubscribeSelf = () => modList.Remove(modifier);

        return modifier;
    }

    [System.Serializable]
    public class Modifier {
        public enum Type {
            constant,
            multiply
        }

        [SerializeField] private int _priority;
        public int Priority { get => _priority; set => _priority = value; }

        [SerializeField] private Type _modType;
        public Type ModType { get => _modType; set => _modType = value; }

        [SerializeField] private float _strength;
        public float Strength { get => _strength; set => _strength = value; }

        public System.Action UnsubscribeSelf { get; set; }
    }
}