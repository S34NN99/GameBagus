using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class CandlePowerAffector : MonoBehaviour {
    [SerializeField] private int reducedPower = 1;
    [SerializeField] private float duration = 5f;
    [SerializeField] private CandleManager CM;

    public void StartPowerAffector() {
        StartCoroutine(PowerAffector(CM.RandomizeCandle()));
    }

    private IEnumerator PowerAffector(Candle candle) {
        //CalculatePower(candleStats.)
        CandleStats.Modifier mod = candle.Stats.AddPowerModifier(10, CandleStats.Modifier.Type.constant, -reducedPower);
        yield return new WaitForSeconds(duration);
        candle.Stats.RemovePowerModifier(mod);

    }

    private int CalculatePower(int candlePower, int amount) {
        return candlePower - amount;
    }
}
