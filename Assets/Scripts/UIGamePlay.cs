using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIGamePlay : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _bestScoreText;
    [SerializeField] private TMP_Text _gameOverText;

    [SerializeField] private Button _backButton;

    private MainManager _mainManager;

    private void Awake()
    {
        _backButton.onClick.AddListener(() => { BackToMenu(); });
        _gameOverText.gameObject.SetActive(false);
    }

    private void Start()
    {
        _mainManager = FindObjectOfType<MainManager>();
        if (_mainManager == null) { Debug.LogError("MainManager is NULL"); }

        _mainManager.OnScoreChanged += SetScoreText;
        _mainManager.OnBestScoreChanged += SetBestScoreText;
        _mainManager.OnGameOver += ActiveGameOverText;

        PlayerData playerData = DataManager.Instance.GetHighScoredPlayerData();
        SetBestScoreText(playerData.playerName, playerData.score);
    }

    void OnDestroy()
    {
        _backButton.onClick.RemoveListener(() => { BackToMenu(); });

        _mainManager.OnScoreChanged -= SetScoreText;
        _mainManager.OnBestScoreChanged -= SetBestScoreText;
        _mainManager.OnGameOver -= ActiveGameOverText;
    }

    private void BackToMenu() => SceneManager.LoadScene(0);

    private void SetScoreText(int score) => _scoreText.text = $"Score : {score}";
    private void SetBestScoreText(string name, int bestScore) => _bestScoreText.text = $"Best Score : {name} : {bestScore}";

    private void ActiveGameOverText(bool gameOver) => _gameOverText.gameObject.SetActive(gameOver);

}
