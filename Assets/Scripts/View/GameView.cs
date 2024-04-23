using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour {
    [SerializeField] private TMP_Text _lvlText;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private Button _addTimeBTN;
    [SerializeField] private Button _pauseBTN;

    public void SetLevel(int level) {
        _lvlText.text = $"lvl {level}";
    }

    public void SetScore(int score, int maxScore) {
        _lvlText.text = $"{score}/{maxScore}";
    }

    private void Start() {
    }
}
