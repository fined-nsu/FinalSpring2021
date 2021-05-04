using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class PauseMenu : MonoBehaviour
{
    public Button resumeButton;
    public Button saveButton;
    public Button loadButton;
    public Button saveJSONButton;
    public Button newGameButton;
    public GameObject panel;
    public Toggle sound;
    public AudioSource audioplayer;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            panel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
        panel.SetActive(false);
    }

    private Save CreateSaveGame()
    {
        Save save = new Save();
        save.lives = IntroSettings.livesValue;
        save.score = InterfaceUpdate.scoreValue;
        save.time = IntroSettings.timerValue;
        save.username = IntroSettings.playerName;

        return save;
    }


    public void SaveGame()
    {
        Save save = CreateSaveGame();

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        bf.Serialize(file, save);
        file.Close();

        Debug.Log("Game Saved");
    }

    [System.Serializable]
    public class Save
    {
        public int score;
        public int lives;
        public float time;
        public string username;
    }

    public void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
            Save save = (Save)bf.Deserialize(file);
            file.Close();
            IntroSettings.livesValue = save.lives;
            InterfaceUpdate.scoreValue = save.score;
            IntroSettings.timerValue = save.time;
            IntroSettings.playerName = save.username;

            Debug.Log("Save Loaded");
        }
        else
        {
            Debug.Log("No save file found!");
        }

    }

    public void SaveAsJSON()
    {
        Save save = CreateSaveGame();
        string json = JsonUtility.ToJson(save);

        Debug.Log("Saving as JSON: " + json);
    }

    public void ToggleSound()
    {
        if (sound.isOn)
        {
            audioplayer.enabled = true;
        }
        else
        {
            audioplayer.enabled = false;
        }
    }

    public void ExitToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}

