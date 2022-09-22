using System.Collections;

using UnityEngine;

[CreateAssetMenu(menuName = "Effects/Candle Power")]
public class CandlePowerEffect : CandleEffect {
    [SerializeField] private float _duration = 5f;
    public float Duration => _duration;

    [SerializeField] private int _changeInPower = 1;
    public int ChangeInPower => _changeInPower;

    protected override IEnumerator AffectCandleCoroutine(Candle candle) {
        CandleStats.Modifier mod = candle.Stats.AddPowerModifier(10, CandleStats.Modifier.Type.constant, ChangeInPower);
        yield return new WaitForSeconds(Duration);
        candle.Stats.RemovePowerModifier(mod);

    }
}
