using UnityEngine;
using UnityEngine.AddressableAssets;

public class GlobalServicesInitializer : MonoBehaviour {
    public AssetReferenceGameObject[] LevelAssets;

    private void Awake() {
        var servicesObject = new GameObject("Services",
            typeof(EventBus),
            typeof(SavesSystem),
            typeof(LevelController),
            typeof(SceneLoader)
            );
        DontDestroyOnLoad(servicesObject);

        var levelController = servicesObject.gameObject.GetComponent<LevelController>();
        levelController.LevelAssets = LevelAssets;

        ServiceLocator.Instance.RegisterService(servicesObject.gameObject.GetComponent<EventBus>());
        ServiceLocator.Instance.RegisterService(servicesObject.gameObject.GetComponent<SavesSystem>());
        ServiceLocator.Instance.RegisterService(levelController);
        ServiceLocator.Instance.RegisterService(servicesObject.gameObject.GetComponent<SceneLoader>());

        
        ServiceLocator.Instance.Get<SceneLoader>().LoadScene(SceneLoader.SceneType.Menu);
    }
}
