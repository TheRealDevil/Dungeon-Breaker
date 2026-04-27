using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public TextMeshProUGUI highScoreDisplay;

    [Header("Panels")]
    public GameObject mainPanel;
    public GameObject characterPanel;

    [Header("CharacterData")]
    public CharacterData maskData;
    public CharacterData sniperData;


    void Start()
    {
        ShowMainPanel();
        UpdateHighScoreDisplay();
    }

    void UpdateHighScoreDisplay()
    {
        if (highScoreDisplay != null)
        {
            //Fetch the saved score form the PlayerPrefs
            int highScore = PlayerPrefs.GetInt("HighScore", 0);
            highScoreDisplay.text = "Best Run: " + highScore.ToString();
        }
    }

    //--Panel Nav--
    public void ShowCharacterPanel()
    {
        mainPanel.SetActive(false);
        characterPanel.SetActive(true);
    }

    public void ShowMainPanel()
    {
        mainPanel.SetActive(true);
        characterPanel.SetActive(false);
    }

    //Selection logic
    public void SelectMask()
    {
        SetCharacterAndStart(maskData);
    }

    public void SelectSniper()
    {
        SetCharacterAndStart(sniperData);
    }

    private void SetCharacterAndStart(CharacterData data)
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.selectedCharacter = data;

            GameManager.Instance.savedPlayerHealth = -1;

            SceneManager.LoadScene("Main");
        }
    }

    public void QuitGame()
    {
        Debug.Log("Game Exiting...");
        Application.Quit();
    }
}