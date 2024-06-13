using UnityEngine;

public class SceneLoader : MonoBehaviour, IService {
    public enum SceneType {
        Menu,
        Game
    }

    private string GetSceneName(SceneType type) {
        switch (type) {
            case SceneType.Menu:
                return "Menu";
            case SceneType.Game:
                return "Game";
        }
        return "";
    }

    public void LoadScene(SceneType sceneType) => LoadScene(GetSceneName(sceneType));

    public void LoadScene(string name) {
        UnityEngine.SceneManagement.SceneManager.LoadScene(name);
    }

    public AsyncOperation LoadSceneAsync(SceneType sceneType) {
        return UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(GetSceneName(sceneType));
    }
}
