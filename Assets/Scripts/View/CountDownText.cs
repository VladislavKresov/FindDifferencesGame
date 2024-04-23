using System;
using TMPro;
using UnityEngine;

public class CountDownText : MonoBehaviour {
    [SerializeField] private TMP_Text _timerText;

    private void Start() {
        if(_timerText == null)
            return;

        ServiceLocator.Instance.Get<EventBus>().Subscribe<CountDownTickSignal>(OnTimerTick);
    }

    private void OnTimerTick(CountDownTickSignal signal) {
        _timerText.text = TimeSpan.FromMilliseconds(signal.MillisecondsLeft).ToString(@"mm\:ss");
    }

    private void OnDestroy() {
        if (ServiceLocator.IsAlive) {
            ServiceLocator.Instance.Get<EventBus>().Unsubscribe<CountDownTickSignal>(OnTimerTick);
        }
    }
}
