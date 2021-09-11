using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Assertions;


public class EventManager: MonoBehaviour {

    private Dictionary<string, UnityEvent> _eventDictionary;

    private void Initialize() {
        if (_eventDictionary == null) {
            _eventDictionary = new Dictionary<string, UnityEvent>();
        }
    }

    // Static members

    private static EventManager _instance;
    public static EventManager Instance { 
        get {
            if (!_instance) FindInstance();
            return _instance;
        }
    }

    private static void FindInstance() {
        EventManager[] eventManagers = FindObjectsOfType<EventManager>(true);
        Assert.IsTrue(eventManagers.Length == 1);
        _instance = eventManagers[0];
        _instance.Initialize();
    }

    public static UnityEvent GetEvent(string eventName) {
        UnityEvent thisEvent = null;
        Instance._eventDictionary.TryGetValue(eventName, out thisEvent);
        return thisEvent;
    }

    public static UnityEvent GetOrCreateEvent(string eventName) {
        UnityEvent thisEvent = GetEvent(eventName);
        if (thisEvent == null) {
            thisEvent = new UnityEvent();
            Instance._eventDictionary.Add(eventName, thisEvent);
        }
        return thisEvent;
    }

    public static void AddListener(string eventName, UnityAction listener) {
        UnityEvent thisEvent = GetOrCreateEvent(eventName);
        thisEvent.AddListener(listener);
    }

    public static void RemoveListener(string eventName, UnityAction listener) {
        UnityEvent thisEvent = GetEvent(eventName);
        if (thisEvent != null) {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void Invoke(string eventName) {
        UnityEvent thisEvent = GetEvent(eventName);
        if (thisEvent != null) {
            thisEvent.Invoke();
        }
    }
}
