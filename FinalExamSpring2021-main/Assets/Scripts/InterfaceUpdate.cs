using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class InterfaceUpdate : MonoBehaviour
{
    public Text playerNameText;
    public Text scoreText;
    public Text livesText;
    public Text timeRemainingText;
    public Button livesUp;
    public Button livesDown;
    public Button scoreUp;
    public Button scoreDown;
    public Button donePlaying;

    public static int scoreValue;

    // Start is called before the first frame update
    void Start()
    {
        scoreValue = 0;
    }

    // Update is called once per frame
    void Update()
    {
        playerNameText.text = "Currently playing: " + IntroSettings.playerName;
        scoreText.text = scoreValue.ToString();
        livesText.text = IntroSettings.livesValue.ToString();
        if(IntroSettings.timerValue > 0)
        {
            IntroSettings.timerValue -= Time.deltaTime;
        }
        timeRemainingText.text = IntroSettings.timerValue.ToString("00");
        if(IntroSettings.timerValue <= 0)
        {
            NextScene();
        }
    }

    public void IncreaseScore()
    {
        scoreValue++;
    }

    public void DecreaseScore()
    {
        if (scoreValue > 0)
        {
            scoreValue--;
        }
    }

    public void IncreaseLives()
    {
        IntroSettings.livesValue++;
    }

    public void DecreaseLives()
    {
        if(IntroSettings.livesValue > 0)
        {
            IntroSettings.livesValue--;
        }
    }

    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
