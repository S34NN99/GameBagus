
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// <para> Base class to store generic logic for an Game Event System. </para>
/// <para> The Purpose of the generic logic is to enable parameters for the <see cref="UnityEvent"/>(s). Each type of event with a different parameter type needs it's own dictionary to be able to pass in the parameters WITH type checking. </para>
/// <para> Note : This base class SHOULD NOT contain any PUBLIC methods, the public methods are to be implemented in the child classes. </para>
/// </summary>
public abstract class EventManagerBase : MonoBehaviour {
    protected static EventManagerClass GetInstance<EventManagerClass>(ref EventManagerClass currentInstance) where EventManagerClass : EventManagerBase {
        if (currentInstance == null) {
            currentInstance = FindObjectOfType<EventManagerClass>();

            if (currentInstance == null) {
                Debug.Log($"Scene should have at least one {typeof(EventManagerClass)}! Adding new GameObject to attach the manager");
                GameObject eventManagerObj = new GameObject(typeof(EventManagerClass).ToString());
                currentInstance = eventManagerObj.AddComponent<EventManagerClass>();
            }
        }
        return currentInstance;
    }

    /// <summary>
    /// A generic function for <see cref="EventManagerBase"/>'s child classes to implement the singleton logic in Unity's Awake function.
    /// </summary>
    /// <typeparam name="EventManagerClass">The child class that inherits <see cref="EventManagerBase"/>.</typeparam>
    /// <param name="currentInstance">The current singleton instance if any.</param>
    /// <param name="awakingInstance">The instance of <typeparamref name="EventManagerClass"/> that is calling this method in its Awake function.</param>
    /// <param name="initDictionariesAction">Additional code to execute if <i><paramref name="currentInstance"/></i> was <see langword="null"/>, thus taking <i><paramref name="awakingInstance"/></i> as the new instance</param>
    protected static void InitManager<EventManagerClass>(ref EventManagerClass currentInstance, EventManagerClass awakingInstance, System.Action initDictionariesAction = null) where EventManagerClass : EventManagerBase {
        if (currentInstance == null) {
            currentInstance = awakingInstance;
            initDictionariesAction?.Invoke();
        } else if (awakingInstance != currentInstance) {
            Destroy(awakingInstance);
        }
    }

    #region Events with Parameters
    protected static void StartListeningTo<T>(Dictionary<string, UnityEvent<T>> eventDictionary, string eventName, UnityAction<T> action) {
        UnityEvent<T> thisEvent;
        if (eventDictionary.TryGetValue(eventName, out thisEvent)) {
            thisEvent.AddListener(action);
        } else {
            thisEvent = new UnityEvent<T>();
            thisEvent.AddListener(action);
            eventDictionary.Add(eventName, thisEvent);
        }
    }
    protected static void StopListeningTo<T>(Dictionary<string, UnityEvent<T>> eventDictionary, string eventName, UnityAction<T> action) {
        UnityEvent<T> thisEvent;
        if (eventDictionary.TryGetValue(eventName, out thisEvent)) {
            thisEvent.RemoveListener(action);
        } else {
            Debug.Log($"No such event exists : {eventName} <{typeof(T)}>");
        }
    }
    protected static void RemoveEvent<T>(Dictionary<string, UnityEvent<T>> eventDictionary, string eventName) {
        eventDictionary.Remove(eventName);
    }
    protected static void TriggerEvent<T>(Dictionary<string, UnityEvent<T>> eventDictionary, string eventName, T args) {
        UnityEvent<T> thisEvent;
        if (eventDictionary.TryGetValue(eventName, out thisEvent)) {
            thisEvent?.Invoke(args);
        }
    }
    #endregion

    #region Events without Parameters 
    protected static void StartListeningTo(Dictionary<string, UnityEvent> eventDictionary, string eventName, UnityAction action) {
        UnityEvent thisEvent;
        if (eventDictionary.TryGetValue(eventName, out thisEvent)) {
            thisEvent.AddListener(action);
        } else {
            thisEvent = new UnityEvent();
            thisEvent.AddListener(action);
            eventDictionary.Add(eventName, thisEvent);
        }
    }
    protected static void StopListeningTo(Dictionary<string, UnityEvent> eventDictionary, string eventName, UnityAction action) {
        UnityEvent thisEvent;
        if (eventDictionary.TryGetValue(eventName, out thisEvent)) {
            thisEvent.RemoveListener(action);
        } else {
            Debug.Log($"No such event exists : {eventName}");
        }
    }
    protected static void RemoveEvent(Dictionary<string, UnityEvent> eventDictionary, string eventName) {
        eventDictionary.Remove(eventName);
    }
    protected static void BroadcastEvent(Dictionary<string, UnityEvent> eventDictionary, string eventName) {
        UnityEvent thisEvent;
        if (eventDictionary.TryGetValue(eventName, out thisEvent)) {
            thisEvent?.Invoke();
        }
    }
    #endregion
}
