using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class UIManager : MonoBehaviour
{
    public InputField playerName; // Khai báo static cho InputField
    public int Score;
    public static UIManager Instance;

    [System.Serializable]
    class SaveData
    {
        public string playerName;
        public int Score;
    }

    public void SaveBestTime()
    {
        SaveData data = new SaveData
        {
            playerName = playerName.text,
            Score = Score
        };

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadBestTime()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
       
                playerName.text = data.playerName; // Gán giá trị cho InputField
            
            Score = data.Score;
        }
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Tải dữ liệu từ tệp
        LoadBestTime();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
