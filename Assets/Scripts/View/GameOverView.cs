using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverView : MonoBehaviour {
    [SerializeField] private RectTransform _uiContainer;
    [SerializeField] private TMP_Text _labelTXT;
    [SerializeField] private Button _continueBTN;
    [SerializeField] private Button _menuBTN;

    private void Start() {
        ServiceLocator.Instance.Get<EventBus>().Subscribe<GameOverSignal>(OnGameOver);
    }

    private void OnGameOver(GameOverSignal signal) {
        _uiContainer.gameObject.SetActive(true);
        var levelController = ServiceLocator.Instance.Get<LevelController>();

        _labelTXT.text = signal.IsGameWin ? "Level Completed!" : "Time is left";

        _continueBTN.onClick.RemoveAllListeners();
        if(signal.IsGameWin) {
            _continueBTN.transform.GetComponentInChildren<TMP_Text>().text = "Next level";
            _continueBTN.onClick.AddListener(() => levelController.LoadNextLevel());
        }
        else {
            _continueBTN.transform.GetComponentInChildren<TMP_Text>().text = "Try again";
            _continueBTN.onClick.AddListener(() => levelController.RestartLevel());
        }

        _menuBTN.onClick.RemoveAllListeners();
        _menuBTN.onClick.AddListener(() => ServiceLocator.Instance.Get<SceneLoader>().LoadScene(SceneLoader.SceneType.Menu));
    }

    private void OnDestroy() {
        if (ServiceLocator.IsAlive) {
            ServiceLocator.Instance.Get<EventBus>().Unsubscribe<GameOverSignal>(OnGameOver);
        }
    }
}
