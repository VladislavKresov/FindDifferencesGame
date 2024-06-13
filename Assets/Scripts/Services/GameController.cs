using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour, IService {
    private Dictionary<string, bool> _hiddenObjectsStatus = new Dictionary<string, bool>();
    public Dictionary<string, bool> HiddenObjectsStatus => _hiddenObjectsStatus;

    private void Start() {
        ServiceLocator.Instance.Get<EventBus>().Subscribe<LevelCreatedSignal>(OnLevelCreated);
        ServiceLocator.Instance.Get<EventBus>().Subscribe<TimeIsLeftSignal>(OnLeftTime);
        ServiceLocator.Instance.Get<EventBus>().Subscribe<HiddenObjectClickSignal>(OnHiddenObjectClick);
    }

    private void OnHiddenObjectClick(HiddenObjectClickSignal signal) {
        var name = signal.GameObj.name;
        if (!_hiddenObjectsStatus[name]) {
            _hiddenObjectsStatus[name] = true;
            ServiceLocator.Instance.Get<EventBus>().Invoke(new NewHiddenObjectFoundSignal(name));
            if (_hiddenObjectsStatus.Where(obj => obj.Value).ToArray().Length == _hiddenObjectsStatus.Count) {
                ServiceLocator.Instance.Get<EventBus>().Invoke(new GamePauseSignal());
                ServiceLocator.Instance.Get<EventBus>().Invoke(new GameOverSignal(true));
            }
        }
    }

    private void OnLeftTime(TimeIsLeftSignal signal) {
        ServiceLocator.Instance.Get<EventBus>().Invoke(new GamePauseSignal());
        ServiceLocator.Instance.Get<EventBus>().Invoke(new GameOverSignal(false));
    }

    private void OnLevelCreated(LevelCreatedSignal signal) {
        _hiddenObjectsStatus.Clear();
        foreach (var name in signal.HiddenObjectsNames) {
            _hiddenObjectsStatus.Add(name, false);
        }
    }

    private void OnDestroy() {
        if (ServiceLocator.IsAlive) {
            ServiceLocator.Instance.Get<EventBus>().Unsubscribe<LevelCreatedSignal>(OnLevelCreated);
            ServiceLocator.Instance.Get<EventBus>().Unsubscribe<TimeIsLeftSignal>(OnLeftTime);
            ServiceLocator.Instance.Get<EventBus>().Unsubscribe<HiddenObjectClickSignal>(OnHiddenObjectClick);
        }
    }
}
