using UnityEngine;
using UnityEngine.AddressableAssets;

public class GameSceneServicesInitializer : MonoBehaviour {
    [SerializeField] private AssetReferenceGameObject[] _levelAssets;
    [SerializeField] private RectTransform _imagesContainer;
    private GameObject _servicesObject;

    private void Awake() {
        InitializeServices();
    }

    private void InitializeServices() {
        _servicesObject = new GameObject("Scene Services",
                            typeof(LevelCreator),
                            typeof(AdressableLevelLoader),
                            typeof(GameController),
                            typeof(CountDown));

        _servicesObject.GetComponent<LevelCreator>().ImagesContainer = _imagesContainer;
        _servicesObject.GetComponent<AdressableLevelLoader>().LevelAssets = _levelAssets;

        ServiceLocator.Instance.RegisterService(_servicesObject.GetComponent<LevelCreator>());
        ServiceLocator.Instance.RegisterService(_servicesObject.GetComponent<AdressableLevelLoader>());
        ServiceLocator.Instance.RegisterService(_servicesObject.GetComponent<GameController>());
        ServiceLocator.Instance.RegisterService(_servicesObject.GetComponent<CountDown>());
    }

    private void OnDestroy() {
        if (ServiceLocator.IsAlive) {
            ServiceLocator.Instance.UnregisterService(_servicesObject.GetComponent<LevelCreator>());
            ServiceLocator.Instance.UnregisterService(_servicesObject.GetComponent<AdressableLevelLoader>());
            ServiceLocator.Instance.UnregisterService(_servicesObject.GetComponent<GameController>());
            ServiceLocator.Instance.UnregisterService(_servicesObject.GetComponent<CountDown>());
        }
    }
}
