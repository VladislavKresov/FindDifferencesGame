using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PauseView : MonoBehaviour {
    [SerializeField] private RectTransform _uiContainer;
    [SerializeField] private Button _resumeBTN;
    [SerializeField] private Button _menuBTN;

    private void Start() {
        _uiContainer.gameObject.SetActive(true);
        var eventBus = ServiceLocator.Instance.Get<EventBus>();
        eventBus.Subscribe<PauseButtonClickSignal>(OnPauseButtonClick);
        _resumeBTN.onClick.AddListener(OnResumeButtonClick);
        _menuBTN.onClick.AddListener(() => ServiceLocator.Instance.Get<SceneLoader>().LoadScene(SceneLoader.SceneType.Menu));
        _uiContainer.gameObject.SetActive(false);
    }

    private void OnResumeButtonClick() {
        ServiceLocator.Instance.Get<EventBus>().Invoke(new GameResumeSignal());
        _uiContainer.gameObject.SetActive(false);
    }

    private void OnPauseButtonClick(PauseButtonClickSignal signal) {
        ServiceLocator.Instance.Get<EventBus>().Invoke(new GamePauseSignal());
        _uiContainer.gameObject.SetActive(true);
    }

    private void OnDestroy() {
        if (ServiceLocator.IsAlive) {
            ServiceLocator.Instance.Get<EventBus>().Unsubscribe<PauseButtonClickSignal>(OnPauseButtonClick);
        }
    }
}
