using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class GameEventManager : MonoBehaviour {
    private static GameEventManager _instance;
    public static GameEventManager Instance {
        get {
            if (_instance == null) {
                _instance = new GameObject("Event Manager").AddComponent<GameEventManager>();
            }
            return _instance;
        }
    }

    private Dictionary<string, System.Action> events = new Dictionary<string, System.Action>();

    private void Awake() {
        if (_instance == null) {
            _instance = this;
        } else if (_instance != this) {
            Destroy(this);
        }

    }

    public void CreateNewEvent(string eventName) {
        if (!events.TryGetValue(eventName, out _)) {
            events.Add(eventName, () => { });
        }
    }

    public void RemoveEvent(string eventName) {
        events.Remove(eventName);
    }

    public void SubscribeToEvent(string eventName, System.Action action) {
        if (events.TryGetValue(eventName, out System.Action eventVal)) {
            eventVal += action;
            events[eventName] = eventVal;
        }
    }

    public void UnubscribeFromEvent(string eventName, System.Action action) {
        if (events.TryGetValue(eventName, out System.Action eventVal)) {
            eventVal -= action;
            events[eventName] = eventVal;
        }
    }

    public void BroadcastEvent(string eventName) {
        if (events.TryGetValue(eventName, out System.Action eventVal)) {
            eventVal.Invoke();
        }
    }
}
