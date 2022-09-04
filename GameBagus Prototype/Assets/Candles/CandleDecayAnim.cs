using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class CandleDecayAnim : MonoBehaviour {
    [SerializeField] private float minPos = 0;
    [SerializeField] private float maxPos = 2;

    public void UpdateDisplay(float percentageHealthRemaining) {
        Vector3 currentPos = transform.localPosition;
        currentPos.y = Mathf.Lerp(minPos, maxPos, percentageHealthRemaining);
        transform.localPosition = currentPos;
    }
}
