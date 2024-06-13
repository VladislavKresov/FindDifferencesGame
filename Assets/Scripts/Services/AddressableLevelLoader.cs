using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressableLevelLoader : MonoBehaviour, IService {
    public void LoadLevel(AssetReferenceGameObject level) {
        if (level.OperationHandle.IsValid()) {
            ServiceLocator.Instance.Get<EventBus>().Invoke(new LevelPrefabLoadedSignal(level.OperationHandle.Convert<GameObject>().Result));
        }
        else {
            level.LoadAssetAsync().Completed += OnAddressableLoaded;
        }
    }

    private void OnAddressableLoaded(AsyncOperationHandle<GameObject> handle) {
        if (handle.Status == AsyncOperationStatus.Succeeded) {
            ServiceLocator.Instance.Get<EventBus>().Invoke(new LevelPrefabLoadedSignal(handle.Result));
        } else {
            Debug.LogError($"Addressable level loading failed");
        }
    }
}
