using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AdressableLevelLoader : MonoBehaviour, IService {
    [SerializeField] private AssetReferenceGameObject[] _levelAssets;
    public Action<GameObject> AddressableLevelLoaded;

    public void LoadLevel(int level) {
        if (level < 0 || _levelAssets.Length <= level) {
            Debug.LogError($"Attempted to load unregistered level '{level}'");
            return;
        }
        _levelAssets[level].LoadAssetAsync().Completed += OnAddressableLoaded;
    }

    private void OnAddressableLoaded(AsyncOperationHandle<GameObject> handle) {
        if (handle.Status == AsyncOperationStatus.Succeeded) {
            AddressableLevelLoaded?.Invoke(handle.Result);
        } else {
            Debug.LogError($"Addressable level loading failed");
        }
    }
}
