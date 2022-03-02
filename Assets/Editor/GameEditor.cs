using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEditor : EditorWindow
{
    [MenuItem("My Game/GameEditor")]
    public static void ShowMissionControl()
    {
        GetWindow(typeof(GameEditor));
    }

    private void OnGUI()
    {

        ShowPlayStopButton();
        ShowResetDataButton();

        if (EditorApplication.isPlaying)
        {
            ShowGameDataLabel();
        }
    }

    private void ShowGameDataLabel()
    {
        GUILayout.Label("Game Data", EditorStyles.boldLabel);

        GUILayout.Label("Time Scale: " + Time.timeScale);

        Repaint();

    }

    private void ShowResetDataButton()
    {
        if (GUILayout.Button("Reset Data"))
        {
            File.Delete(Application.persistentDataPath + "/save.bin");

            string dataFilePath_json = string.Format("{0}/{1}.json", Application.persistentDataPath, "my_game");

            if (File.Exists(dataFilePath_json))
            {
                File.Delete(dataFilePath_json);
            }

            PlayerPrefs.DeleteAll();

            if (EditorApplication.isPlaying)
            {
                //EditorApplication.ExitPlaymode();
                ReloadLevel();
            }
        }
    }

    private void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void ShowPlayStopButton()
    {
        string buttonName;

        if (EditorApplication.isPlaying) { buttonName = "Stop"; }
        else { buttonName = "Play"; }

        if (GUILayout.Button(buttonName))
        {
            if (EditorApplication.isPlaying)
            {
                EditorApplication.isPlaying = false;
            }
            else
            {
                EditorApplication.isPlaying = true;
            }
        }
    }
}