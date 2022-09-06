using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandlePowerAffector : MonoBehaviour
{
    [SerializeField] private int reducedPower = 1;
    [SerializeField] private float duration = 5f;
    [SerializeField] private CandleManager CM;

    public void StartPowerAffector()
    {
        StartCoroutine(PowerAffector(CM.RandomizeCandle().Stats));
    }

    private IEnumerator PowerAffector(CandleStats candleStats)
    {
        //CalculatePower(candleStats.)
        yield return new WaitForSeconds(duration);

    }

    private int CalculatePower(int candlePower, int amount)
    {
        return candlePower - amount;
    }
}
