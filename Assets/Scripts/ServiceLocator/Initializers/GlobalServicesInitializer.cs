using UnityEngine;

public class GlobalServicesInitializer : MonoBehaviour {
    private void Awake() {
        var servicesObject = new GameObject("Services",
            typeof(SavesSystem),
            typeof(SceneLoader));
        DontDestroyOnLoad(servicesObject);

        ServiceLocator.Instance.RegisterService(servicesObject.gameObject.GetComponent<SavesSystem>());
        ServiceLocator.Instance.RegisterService(servicesObject.gameObject.GetComponent<SceneLoader>());
        
        ServiceLocator.Instance.Get<SceneLoader>().LoadScene(SceneLoader.SceneType.Menu);
    }
}
