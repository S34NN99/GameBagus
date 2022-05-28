using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;

public class ActionDropHandler : MonoBehaviour, IDropHandler {
    [SerializeField] private Candle targetCandle;

    public void OnDrop(PointerEventData eventData) {
        print(targetCandle.name);
        if (eventData.selectedObject.TryGetComponent(out BaseAction action)) {
            action.ActOn(targetCandle);
        }
    }
}
