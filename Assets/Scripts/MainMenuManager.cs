using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

public class MainMenuManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject mainPanel;
    public GameObject characterPanel;

    [Header("CharacterData")]
    public CharacterData maskData;
    public CharacterData sniperData;

    void Start()
    {
        ShowMainPanel();
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