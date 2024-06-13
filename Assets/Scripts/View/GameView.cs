using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour {
    [SerializeField] private TMP_Text _lvlText;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private Button _addTimeBTN;
    [SerializeField] private Button _pauseBTN;

    public void SetLevel(int level) {
        _lvlText.text = $"lvl {level}";
    }

    public void SetScore(int score, int maxScore) {
        _scoreText.text = $"{score}/{maxScore}";
    }

    private void Start() {
        var eventBus = ServiceLocator.Instance.Get<EventBus>();
        eventBus.Subscribe<LevelStartSignal>(OnLevelStart);
        eventBus.Subscribe<NewHiddenObjectFoundSignal>(OnHiddenObjectFound);
        _pauseBTN.onClick.AddListener(() => eventBus.Invoke(new PauseButtonClickSignal()));
    }

    private void OnHiddenObjectFound(NewHiddenObjectFoundSignal signal) {
        var hiddenObjects = ServiceLocator.Instance.Get<GameController>().HiddenObjectsStatus;
        int objectsFound = hiddenObjects.Where(obj => obj.Value).ToArray().Length;
        SetScore(objectsFound, hiddenObjects.Count);
    }

    private void OnLevelStart(LevelStartSignal signal) {
        SetLevel(signal.Level);
        SetScore(0, signal.HiddenObjectsCount);
    }

    private void OnDestroy() {
        if (ServiceLocator.IsAlive) {
            ServiceLocator.Instance.Get<EventBus>().Unsubscribe<LevelStartSignal>(OnLevelStart);
            ServiceLocator.Instance.Get<EventBus>().Unsubscribe<NewHiddenObjectFoundSignal>(OnHiddenObjectFound);
        }
    }
}
