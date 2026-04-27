using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelector : MonoBehaviour
{
    [Header("Character Blueprints")]
    public CharacterData maskData;
    public CharacterData sniperData;

    public void SelectMask()
    {
        if (GameManager.Instance != null)
        {
            //Store the choice in the manager
            GameManager.Instance.selectedCharacter = maskData;

            //Ensure a fresh game start with full health
            GameManager.Instance.savedPlayerHealth = -1;

            //Load the game scene
            SceneManager.LoadScene("Main");
        }
        else Debug.Log("Game manager not found");
    }

    public void SelectSniper()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.selectedCharacter = sniperData;
            GameManager.Instance.savedPlayerHealth = -1;
            SceneManager.LoadScene("Main");
        }
    }
}