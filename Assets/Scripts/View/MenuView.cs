using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuView : MonoBehaviour {
    [SerializeField] private TMP_Text _labelText;
    [SerializeField] private Button _playBTN;

    private void Start() {
        _playBTN.onClick.AddListener(()=>ServiceLocator.Instance.Get<SceneLoader>().LoadScene(SceneLoader.SceneType.Game));
    }
}
