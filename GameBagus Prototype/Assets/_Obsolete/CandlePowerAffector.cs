using System.Collections;

using UnityEngine;

public class CandlePowerAffector : MonoBehaviour {
    [SerializeField] private int reducedPower = 1;
    [SerializeField] private float duration = 5f;

    [SerializeField] private CandleManager _cm;
    private CandleManager CM => _cm;

    public void StartPowerAffector(int childNumber) {
        Candle candle = CM.CandleSlots[childNumber];
        if (candle != null) {
            StartCoroutine(PowerAffector(candle));
        }
    }

    private IEnumerator PowerAffector(Candle candle) {

        CandleStats.Modifier mod = candle.Stats.AddPowerModifier(10, CandleStats.Modifier.Type.constant, -reducedPower);
        yield return new WaitForSeconds(duration);
        candle.Stats.RemovePowerModifier(mod);

    }
}
