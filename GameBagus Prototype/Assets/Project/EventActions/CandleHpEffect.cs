using System.Collections;

using UnityEngine;

[CreateAssetMenu(menuName = "Effects/Candle Hp")]
public class CandleHpEffect : CandleEffect {
    [SerializeField] private float _duration = 5f;
    private float Duration => _duration;

    [SerializeField] private int _chanceInHp = 15;
    private int ChangeInHp => _chanceInHp;

    protected override IEnumerator AffectCandleCoroutine(Candle candle) {
        float elapsedTime = 0;
        float hpChangePerSec = ChangeInHp / Duration;

        while (elapsedTime < Duration) {
            elapsedTime += Time.deltaTime;
            candle.Stats.HpProp.Value += hpChangePerSec * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        };
    }

}
