using System;
using System.Collections;
using UnityEngine;

public class CountDown : MonoBehaviour, IService {
    private Coroutine _coroutine;
    private float _timeLimit;
    private float _millisecondsLeft;
    public float MillisecondsLeft => _millisecondsLeft;

    private void Start() {
        ServiceLocator.Instance.Get<EventBus>().Subscribe<LevelStartSignal>(OnLevelStart);
    }

    private void OnLevelStart(LevelStartSignal signal) {
        _timeLimit = signal.TimeLimit;
        ResetTimer();
        AddTime(_timeLimit);
        StartTimer();
    }

    public void AddTime(float milliseconds) {
        _millisecondsLeft += milliseconds;
    }

    public void ResetTimer() {
        StopTimer();
        _millisecondsLeft = 0;
    }

    public void StartTimer() {
        _coroutine = StartCoroutine(StartCountDown());
    }

    public void StopTimer() {
        if(_coroutine != null)
            StopCoroutine(_coroutine);
    }

    private IEnumerator StartCountDown() {
        while (_millisecondsLeft > 0) {
            _millisecondsLeft -= Time.deltaTime * 1000;
            ServiceLocator.Instance.Get<EventBus>().Invoke(new CountDownTickSignal(Time.deltaTime * 1000, _timeLimit, _millisecondsLeft));
            yield return null;
        }
        ResetTimer();
        ServiceLocator.Instance.Get<EventBus>().Invoke(new TimeIsLeftSignal(_timeLimit));
    }
}
