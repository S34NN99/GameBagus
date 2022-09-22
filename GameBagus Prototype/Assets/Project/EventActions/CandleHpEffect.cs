using System.Collections;

using UnityEngine;

[CreateAssetMenu(menuName = "Effects/Candle Hp")]
public class CandleHpEffect : CandleEffect {
    [SerializeField] private float _duration = 5f;
    private float Duration => _duration;

    [SerializeField] private int _chanceInHp = 15;
    private int ChangeInHp => _chanceInHp;

    protected override IEnumerator AffectCandleCoroutine(Candle candle) {
        CalculateHP(candle.Stats.HpProp.Value, ChangeInHp);
        yield return new WaitForSeconds(Duration);
    }

    private float CalculateHP(float candleHP, float amount) {
        return candleHP + amount;
    }
}
