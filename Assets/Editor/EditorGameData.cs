using System.IO;
using UnityEditor;
using UnityEngine;


public class EditorGameData
{
    [MenuItem("My Game/Open Save File %,")]
    public static void OpenSave()
    {
        string dataFilePath_json = string.Format("{0}/{1}.json", Application.persistentDataPath, "my_game");

        if (File.Exists(dataFilePath_json))
        {
            Application.OpenURL(dataFilePath_json);
        }
    }

    [MenuItem("My Game/Clear Save %.")]
    public static void ClearSave()
    {
        File.Delete(Application.persistentDataPath + "/save.bin");

        string dataFilePath_json = string.Format("{0}/{1}.json", Application.persistentDataPath, "my_game");

        if (File.Exists(dataFilePath_json))
        {
            File.Delete(dataFilePath_json);
        }

        PlayerPrefs.DeleteAll();
    }
}
