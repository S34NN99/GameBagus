using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

public class GeneralEventManager : EventManagerBase {
    private static GeneralEventManager _instance;
    public static GeneralEventManager Instance {
        get {
            return GetInstance(ref _instance);
        }
        private set {
            _instance = value;
        }
    }

    private Dictionary<string, UnityEvent> generalEvents = new();

    private void Awake() {
        InitManager(ref _instance, this);
    }
    public void StartListeningTo(string eventName, UnityAction action) => StartListeningTo(generalEvents, eventName, action);
    public void StopListeningTo(string eventName, UnityAction action) => StopListeningTo(generalEvents, eventName, action);
    public void RemoveEvent(string eventName) => RemoveEvent(generalEvents, eventName);
    public void BroadcastEvent(string eventName) => BroadcastEvent(generalEvents, eventName);
}
