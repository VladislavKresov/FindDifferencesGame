using System.IO;
using UnityEngine;

public class SavesSystem : MonoBehaviour, IService {
    private string _path;
    private SavesModel _savesData;
    public SavesModel SavesData => _savesData;

    private void Start() {
        _path = $"{Application.persistentDataPath}/saves.json";
        ReadSaves();
        ServiceLocator.Instance.Get<EventBus>().Subscribe<NewLevelReachedSignal>(OnNewLevelReached);
    }

    private void OnNewLevelReached(NewLevelReachedSignal signal) {
        _savesData.Level = signal.Level;
        WriteSaves();
    }

    public void ReadSaves() {
        if(File.Exists(_path)) {
            _savesData = JsonUtility.FromJson<SavesModel>(File.ReadAllText(_path));
        }
        else {
            _savesData = new SavesModel();
        }
    }

    public void WriteSaves() {
        File.WriteAllText(_path, JsonUtility.ToJson(_savesData));
    }
}
