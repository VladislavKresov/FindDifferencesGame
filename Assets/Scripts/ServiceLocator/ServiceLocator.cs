using System;
using System.Collections.Generic;
using UnityEngine;

public class ServiceLocator : MonoBehaviour {
    private static ServiceLocator _instance;
    public static ServiceLocator Instance {
        get {
            if (!_instance) {
                _instance = new GameObject(typeof(ServiceLocator).ToString()).AddComponent<ServiceLocator>();
                DontDestroyOnLoad(_instance);
            }
            return _instance; 
        }
    }

    private static Dictionary<string, IService> _services = new Dictionary<string, IService>();

    public void RegisterService<T>(T service) where T : IService {
        string key = nameof(T);
        if(_services.ContainsKey(key)) {
            Debug.LogError("Attempted to register alredy registered service");
            return;
        }
        _services.Add(key, service);
    }

    public void UnregisterService<T>(T service) where T : IService {
        string key = nameof(T);
        if (!_services.ContainsKey(key)) {
            Debug.LogError("Attempted to unregister not registered service");
            return;
        }
        _services.Remove(key);
    }

    public T Get<T>() where T : IService {
        string key = nameof(T);
        if (!_services.ContainsKey(key)) {
            Debug.LogError($"{key} is not registered");
            throw new InvalidOperationException();
        }
        return (T)_services[key];
    }
}
