using System;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour, IService {
    private int _currentLevel;
    private float _levelTimeLimit = 2 * 60 * 1000 + 1000;
    private Dictionary<string, bool> _hiddenObjectsStatus = new Dictionary<string, bool>();

    private void Start() {
        var savesData = ServiceLocator.Instance.Get<SavesSystem>().SavesData;
        var adressableLevelLoader = ServiceLocator.Instance.Get<AdressableLevelLoader>();
        ServiceLocator.Instance.Get<EventBus>().Subscribe<LevelCreatedSignal>(OnLevelCreated);
        ServiceLocator.Instance.Get<EventBus>().Subscribe<TimeIsLeftSignal>(OnLeftTime);
        ServiceLocator.Instance.Get<EventBus>().Subscribe<HiddenObjectClickSignal>(OnHiddenObjectClick);
        adressableLevelLoader.LoadLevel(savesData.Level);
    }

    private void OnHiddenObjectClick(HiddenObjectClickSignal signal) {
        var name = signal.GameObj.name;
        if (!_hiddenObjectsStatus[name]) {
            _hiddenObjectsStatus[name] = true;
            Debug.Log($"{name} is found!");
        }
    }

    private void OnLeftTime(TimeIsLeftSignal signal) {
        Debug.Log("Time is left");
    }

    private void OnLevelCreated(LevelCreatedSignal signal) {
        _hiddenObjectsStatus.Clear();
        foreach (var name in signal.HiddenObjectsNames) {
            _hiddenObjectsStatus.Add(name, false);
        }
        ServiceLocator.Instance.Get<EventBus>().Invoke(new LevelStartSignal(_currentLevel, _levelTimeLimit));
    }

    private void OnDestroy() {
        if (ServiceLocator.IsAlive) {
            ServiceLocator.Instance.Get<EventBus>().Unsubscribe<LevelCreatedSignal>(OnLevelCreated);
            ServiceLocator.Instance.Get<EventBus>().Unsubscribe<TimeIsLeftSignal>(OnLeftTime);
            ServiceLocator.Instance.Get<EventBus>().Unsubscribe<HiddenObjectClickSignal>(OnHiddenObjectClick);
        }
    }
}
