using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandlePowerAffector : MonoBehaviour
{
    [SerializeField] private int powerAmount = 1;
    [SerializeField] private float duration = 5f;

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
