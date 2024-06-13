using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuView : MonoBehaviour {
    [SerializeField] private TMP_Text _labelText;
    [SerializeField] private Button _playBTN;

    private void Start() {
        int currentLevel = ServiceLocator.Instance.Get<SavesSystem>().SavesData.Level;
        _labelText.text = $"Start Game\nlevel {currentLevel}";
        _playBTN.onClick.AddListener(()=>ServiceLocator.Instance.Get<LevelController>().LoadLevel(currentLevel));
    }
}
