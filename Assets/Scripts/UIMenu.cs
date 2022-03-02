using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class UIMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text _bestScoreText;
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _exitButton;

    [SerializeField] private TMP_InputField _inputField;

    private string _playerName;
    private int _score;

    private void Awake()
    {
        _startButton.onClick.AddListener(() => { LoadMain(); });
        _exitButton.onClick.AddListener(() => { Exit(); });
        _inputField.onValueChanged.AddListener(ValueChangeCheck);
    }

    private void Start()
    {
        PlayerData playerData = DataManager.Instance.GetHighScoredPlayerData();
        _inputField.text = playerData.playerName;
        _bestScoreText.text = $"Best Score : {playerData.playerName} : {playerData.score}";
    }

    private void OnDestroy()
    {
        _startButton.onClick.RemoveListener(() => { LoadMain(); });
        _exitButton.onClick.RemoveListener(() => { Exit(); });
        _inputField.onValueChanged.RemoveListener(ValueChangeCheck);
    }

    private void ValueChangeCheck(string text)
    {
        _playerName = text;
        Debug.Log(_playerName);
    }

    private void LoadMain()
    {
        //Save name
        DataManager.Instance.currentPlayerName = _playerName;
        SceneManager.LoadScene(1);
    }

    private void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
