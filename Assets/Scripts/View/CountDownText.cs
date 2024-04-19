using System;
using TMPro;
using UnityEngine;

public class CountDownText : MonoBehaviour {
    [SerializeField] private TMP_Text _timerText;
    private CountDown _timer;

    private void Start() {
        if(_timerText == null)
            return;

        _timer = ServiceLocator.Instance.Get<CountDown>();
        _timer.TimerTick += OnTimerTick;
    }

    private void OnTimerTick(float millisecondsLeft) {
        _timerText.text = TimeSpan.FromMilliseconds(millisecondsLeft).ToString(@"mm\:ss");
    }

    private void OnDestroy() {
        if (_timerText != null) {
            _timer.TimerTick -= OnTimerTick;
        }
    }
}
