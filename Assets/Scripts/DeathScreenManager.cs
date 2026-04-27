using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreenManager : MonoBehaviour
{
    public void GoToMenu()
    {
        if (GameManager.Instance != null)
        {
            //Reset health and score for the next run
            GameManager.Instance.savedPlayerHealth = -1;
            GameManager.Instance.score = 0;
        }

        //Load the MainMenu scene
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Debug.Log("Game Exiting...");
        Application.Quit();
    }
}
