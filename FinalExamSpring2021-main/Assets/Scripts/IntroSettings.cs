using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroSettings : MonoBehaviour
{
    public InputField playerNameInput;
    public Slider timerValueSlider;
    public Dropdown livesCountDropdown;
    public Button nextSceneButton;

    public static string playerName;
    public static float timerValue;
    public static int livesValue;

    private void Start()
    {
        playerName = playerNameInput.text;
        timerValue = 60;
        livesValue = 0;
    }

    public void NextScene()
    {
        timerValue = timerValueSlider.value;
        SceneManager.LoadScene(1);
    }

    public void GetPlayerName()
    {
        playerName = playerNameInput.text;
        Debug.Log(playerName);
    }

    public void SetTimerValue()
    {
        timerValue = timerValueSlider.value;
        Debug.Log(timerValue);
    }

    public void SetLivesValue()
    {
        livesValue = livesCountDropdown.value;
        Debug.Log(livesValue);
    }
}
