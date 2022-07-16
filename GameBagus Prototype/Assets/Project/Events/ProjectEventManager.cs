using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class EventManager : MonoBehaviour {
    [SerializeField] private ProjectEvent[] events;
    [SerializeField] private ProjectEventPanel eventPanel;

    public HashSet<string> AttributeList { get; private set; } = new();

    private void Update() {
        // todo implement a queue system.
        for (int i = 0; i < events.Length; i++) {
            if (events[i].ReadyToFire) {
                events[i].FireEvent(eventPanel);
            }
        }
    }

    public void AddAttribute(string attribute) {
        AttributeList.Add(attribute);
    }

    public void RemoveAttribute(string attribute) {
        AttributeList.Remove(attribute);
    }

}
