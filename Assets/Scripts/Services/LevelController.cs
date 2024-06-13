using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class LevelController : MonoBehaviour, IService {
    public AssetReferenceGameObject[] LevelAssets;
    private float _levelTimeLimit = 2 * 60 * 1000 + 1000;
    private int _currentLevel;
    public int CurrentLevel => _currentLevel;

    private void Start() {
        var saveData = ServiceLocator.Instance.Get<SavesSystem>().SavesData;
        _currentLevel = saveData.Level;
        var eventBus = ServiceLocator.Instance.Get<EventBus>();
        eventBus.Subscribe<LevelCreatedSignal>(OnLevelCreated);
        eventBus.Subscribe<GameOverSignal>(OnGameOver);
    }

    private void OnGameOver(GameOverSignal signal) {
        if(signal.IsGameWin) {
            ServiceLocator.Instance.Get<EventBus>().Invoke(new NewLevelReachedSignal(_currentLevel+1));
        }
    }

    private void OnLevelCreated(LevelCreatedSignal signal) {
        ServiceLocator.Instance.Get<EventBus>().Invoke(new LevelStartSignal(_currentLevel, _levelTimeLimit, signal.HiddenObjectsNames.Length));
    }

    public void RestartLevel() {
        LoadLevel(_currentLevel);
    }

    public void LoadNextLevel() {
        LoadLevel(++_currentLevel);
    }

    public void LoadLevel(int level) {
        if (level < 0 || LevelAssets.Length <= level) {
            level = LevelAssets.Length-1;
        }
        StartCoroutine(LoadGameScene(level));
    }

    private IEnumerator LoadGameScene(int level) {
        var sceneLoadingOperation = ServiceLocator.Instance.Get<SceneLoader>().LoadSceneAsync(SceneLoader.SceneType.Game);
        yield return sceneLoadingOperation;

        ServiceLocator.Instance.Get<AddressableLevelLoader>().LoadLevel(LevelAssets[level]);
    }
}
