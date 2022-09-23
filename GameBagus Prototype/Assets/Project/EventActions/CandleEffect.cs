using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public abstract class CandleEffect : ScriptableObject {
    [SerializeField] protected int[] _affectedCandlesId = { 1, 2, 3, 4 };
    public int[] AffectedCandlesId => _affectedCandlesId;

    public virtual void ApplyToCandles(CandleManager cm) {
        IReadOnlyList<Candle> candleSlots = cm.CandleSlots;

        foreach (var candleId in AffectedCandlesId) {
            Candle candle = candleSlots[candleId - 1];
            if (candle != null) {
                cm.StartCoroutine(AffectCandleCoroutine(candle));
            }
        }
    }

    protected abstract IEnumerator AffectCandleCoroutine(Candle candle);
}
