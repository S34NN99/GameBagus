using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;

public class DropBtn : MonoBehaviour, IDropHandler {
    [SerializeField] private string printText;

    public void OnDrop(PointerEventData eventData) {

        print("Hello, " + printText);
    }
}
