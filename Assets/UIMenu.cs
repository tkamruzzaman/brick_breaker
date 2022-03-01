using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class UIMenu : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _exitButton;

    void Awake()
    {
        _startButton.onClick.AddListener(() => { LoadMain(); });
        _exitButton.onClick.AddListener(() => { Exit(); });
    }

    private void LoadMain()
    {
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
