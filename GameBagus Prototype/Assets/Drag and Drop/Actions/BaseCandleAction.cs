using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseCandleAction : MonoBehaviour, IEndDragHandler {
    [SerializeField] private Camera candleCamera;

    public void OnEndDrag(PointerEventData eventData) {
        Ray ray = candleCamera.ScreenPointToRay(eventData.position);

        if (Physics.Raycast(ray, out RaycastHit hit)) {
            if (hit.collider.TryGetComponent(out Candle candle)) {
                ActOn(candle);
            }
        }
    }

    public abstract void ActOn(Candle candle);
}