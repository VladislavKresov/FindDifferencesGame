using System;
using System.Collections;
using UnityEngine;

public class CountDown : MonoBehaviour, IService {
    public Action<float> TimerTick;
    public Action TimeLeft;

    private Coroutine _coroutine;
    private float _millisecondsLeft;
    public float MillisecondsLeft => _millisecondsLeft;

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
            _millisecondsLeft -= Time.deltaTime;
            TimerTick?.Invoke(_millisecondsLeft);
            yield return null;
        }
        ResetTimer();
        TimeLeft?.Invoke();
    }
}
