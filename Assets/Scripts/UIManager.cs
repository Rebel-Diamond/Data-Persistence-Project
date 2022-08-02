using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class UIManager : MonoBehaviour
{
    public string playerName;
    public TextMeshProUGUI nameField;
    public static UIManager Instance;
    public Button startButton;
    public static int hiScore;
    public static string HiScoreName;


    // Start is called before the first frame update
    void Awake()
    {
        LoadScores();
        
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene aScene, LoadSceneMode aMode)
    {
        if (GameObject.Find("NameField"))
        {
            nameField = GameObject.Find("NameField").GetComponent<TextMeshProUGUI>();
        }

        if (GameObject.Find("Start Button"))
        {
            startButton = GameObject.Find("Start Button").GetComponent<Button>();
            startButton.onClick.AddListener(StartGame);
        }
    }


    public void StartGame()
    {
        if (nameField.text != "Enter Name Here")
        {
            playerName = nameField.text;
            SceneManager.LoadScene(1);
        }
    }

    public void SaveScores()
    {
        SaveData save = new SaveData();

        save.hiScore = hiScore;
        save.HiScoreName = HiScoreName;

        string json = JsonUtility.ToJson(save);

        File.WriteAllText(Application.dataPath + "/save.json", json);
    }
    public void LoadScores()
    {
        string savePath = Application.dataPath + "/save.json";

        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);

            SaveData save = JsonUtility.FromJson<SaveData>(json);

            hiScore = save.hiScore;
            HiScoreName = save.HiScoreName;
        }
    }


    [System.Serializable]
    public class SaveData
    {
        public int hiScore;
        public string HiScoreName;
    }
}
