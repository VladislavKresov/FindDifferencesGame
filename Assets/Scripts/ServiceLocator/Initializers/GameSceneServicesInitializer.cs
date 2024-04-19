using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneServicesInitializer : MonoBehaviour {
    [SerializeField] private RectTransform _imagesContainer;
    private GameObject _servicesObject;

    private void Awake() {
        InitializeServices();
    }

    private void InitializeServices() {
        _servicesObject = new GameObject("Scene Services",
                            typeof(PrefabToImages),
                            typeof(AdressableLevelLoader),
                            typeof(CountDown));

        _servicesObject.GetComponent<PrefabToImages>().ImagesContainer = _imagesContainer;

        ServiceLocator.Instance.RegisterService(_servicesObject.GetComponent<PrefabToImages>());
        ServiceLocator.Instance.RegisterService(_servicesObject.GetComponent<AdressableLevelLoader>());
        ServiceLocator.Instance.RegisterService(_servicesObject.GetComponent<CountDown>());
    }

    private void OnDestroy() {
        if (_servicesObject == null)
            return;

        ServiceLocator.Instance.UnregisterService(_servicesObject.GetComponent<PrefabToImages>());
        ServiceLocator.Instance.UnregisterService(_servicesObject.GetComponent<AdressableLevelLoader>());
        ServiceLocator.Instance.UnregisterService(_servicesObject.GetComponent<CountDown>());
    }
}
