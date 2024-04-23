using System;
using System.Collections.Generic;
using UnityEngine;

public class EventBus : MonoBehaviour, IService {
    private Dictionary<string, List<object>> _callbacks = new Dictionary<string, List<object>>();

    public void Subscribe<T>(Action<T> callback) {
        string key = typeof(T).Name;
        if(_callbacks.ContainsKey(key)) {
            _callbacks[key].Add(callback);
        }
        else {
            _callbacks.Add(key, new List<object>() { callback });
        }
    }

    public void Unsubscribe<T>(Action<T> callback) {
        string key = typeof(T).Name;
        if (_callbacks.ContainsKey(key)) {
            _callbacks[key].Remove(callback);
        }
        else {
            Debug.LogError("Trying to unsubscribe unregistered callback");
        }
    }

    public void Invoke<T>(T signal) {
        string key = typeof(T).Name;
        if(_callbacks.ContainsKey(key)) {
            foreach(object obj in _callbacks[key]) {
                var callback = obj as Action<T>;
                callback?.Invoke(signal);
            }
        }
    }
}
