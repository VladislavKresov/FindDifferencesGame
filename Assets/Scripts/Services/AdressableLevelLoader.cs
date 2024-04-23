using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AdressableLevelLoader : MonoBehaviour, IService {
    public AssetReferenceGameObject[] LevelAssets;

    public void LoadLevel(int level) {
        if (level < 0 || LevelAssets.Length <= level) {
            Debug.LogError($"Attempted to load unregistered level '{level}'");
            return;
        }
        LevelAssets[level].LoadAssetAsync().Completed += OnAddressableLoaded;
    }

    private void OnAddressableLoaded(AsyncOperationHandle<GameObject> handle) {
        if (handle.Status == AsyncOperationStatus.Succeeded) {
            ServiceLocator.Instance.Get<EventBus>().Invoke(new LevelPrefabLoadedSignal(handle.Result));
        } else {
            Debug.LogError($"Addressable level loading failed");
        }
    }
}
