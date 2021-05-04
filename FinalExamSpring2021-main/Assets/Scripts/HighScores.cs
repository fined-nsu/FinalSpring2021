using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HighScores : MonoBehaviour
{
    public string[] usernameRecords;
    public int[] scoreRecords;
    public int[] livesRecords;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < scoreRecords.Length; i++)
        {
            if(scoreRecords == null)
            {
                scoreRecords[i] = InterfaceUpdate.scoreValue;
            }
            else if(InterfaceUpdate.scoreValue > scoreRecords[i])
            {

            }
        }
    }
    private Transform entryContainer;
    private Transform entryTemplate;
    private List<Transform> highscoreEntryTransformList;
    private Highscores highscores;

    void Awake()
    {
        entryContainer = transform.Find("highscoreEntryContainer");
        entryTemplate = entryContainer.Find("highscoreEntryTemplate");

        entryTemplate.gameObject.SetActive(false);

        ReloadList();
        //string jsonString = PlayerPrefs.GetString("highscoreTable");
        //highscores = JsonUtility.FromJson<Highscores>(jsonString);
        if (highscores == null)
        {
            InitializeHighscores();
        }
        else if (highscores.highscoreEntryList.Count < 10)
        {
            AddHighscoreEntry(InterfaceUpdate.scoreValue, IntroSettings.playerName);
        }
        bool greaterScore = false;
        for (int i = 0; i < highscores.highscoreEntryList.Count; i++)
        {
            if (InterfaceUpdate.scoreValue > highscores.highscoreEntryList[i].score)
            {
                greaterScore = true;
            }
        }
        if (greaterScore)
        {
            AddHighscoreEntry(InterfaceUpdate.scoreValue, IntroSettings.playerName);
        }
        //AddHighscoreEntry(4, "a");
        //AddHighscoreEntry(3, "e");
        //AddHighscoreEntry(1, "w");

        ReloadList();
        //jsonString = PlayerPrefs.GetString("highscoreTable");
        //highscores = JsonUtility.FromJson<Highscores>(jsonString);

        // Sort entry list by Score 
        for (int i = 0; i < highscores.highscoreEntryList.Count; i++)
        {
            for (int j = i + 1; j < highscores.highscoreEntryList.Count; j++)
            {
                if (highscores.highscoreEntryList[j].score > highscores.highscoreEntryList[i].score)
                {
                    // Swap
                    HighscoreEntry tmp = highscores.highscoreEntryList[i];
                    highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                    highscores.highscoreEntryList[j] = tmp;
                }
            }
        }

        for (int i = 10; i < highscores.highscoreEntryList.Count; i++)
        {
            highscores.highscoreEntryList.RemoveAt(i);
        }

        highscoreEntryTransformList = new List<Transform>();
        int k = 0;
        foreach (HighscoreEntry highscoreEntry in highscores.highscoreEntryList)
        {
            if (k < 10)
                CreateHighscoreEntryTransform(highscoreEntry, entryContainer, highscoreEntryTransformList);
        }
        ReloadList();
    }

    private void InitializeHighscores()
    {
        // There's no stored table, initialize
        Debug.Log("Initializing table with default values...");
        AddHighscoreEntry(0, "");
        AddHighscoreEntry(0, "");
        AddHighscoreEntry(0, "");
        AddHighscoreEntry(0, "");
        AddHighscoreEntry(0, "");
        AddHighscoreEntry(0, "");
        AddHighscoreEntry(0, "");
        AddHighscoreEntry(0, "");
        AddHighscoreEntry(0, "");
        AddHighscoreEntry(0, "");
    }

    private void ReloadList()
    {
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        highscores = JsonUtility.FromJson<Highscores>(jsonString);
    }


    private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList)
    {
        float templateHeight = 25f;
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString;
        switch (rank)
        {
            default:
                rankString = rank + "TH"; break;

            case 1: rankString = "1ST"; break;
            case 2: rankString = "2ND"; break;
            case 3: rankString = "3RD"; break;
        }

        entryTransform.Find("RankText").GetComponent<Text>().text = rankString;
        entryTransform.Find("ScoreText").GetComponent<Text>().text = highscoreEntry.score.ToString();
        entryTransform.Find("PlayerText").GetComponent<Text>().text = highscoreEntry.name;

        if (rank < 10)
        {
            transformList.Add(entryTransform);
        }
    }

    private void AddHighscoreEntry(int score, string name)
    {
        // Create HighscoreEntry
        HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, name = name };

        // Load saved Highscores
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        if (highscores == null)
        {
            // There's no stored table, initialize
            highscores = new Highscores()
            {
                highscoreEntryList = new List<HighscoreEntry>()
            };
        }

        // Add new entry to Highscores
        highscores.highscoreEntryList.Add(highscoreEntry);

        // Save updated Highscores
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
    }

    private class Highscores
    {
        public List<HighscoreEntry> highscoreEntryList;
    }

    public void RemoveHighscores()
    {
        PlayerPrefs.DeleteKey("highscoreTable");
        InterfaceUpdate.scoreValue = 0;
        IntroSettings.playerName = "";
        InitializeHighscores();
        ReloadList();
        SceneManager.LoadScene(2);
    }

    /*
     * Represents a single High score entry
     * */
    [System.Serializable]
    private class HighscoreEntry
    {
        public int score;
        public string name;
    }
}
