using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int score = 0;
    public TextMeshProUGUI scoreText; //Reference to UI text element for score display

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject); //Ensure only one instance of GameManager exists
    }

    public void AddScore(int points)
    {
        score += points;
        if (scoreText != null) scoreText.text = "Score:" + score; //Update score display UI
    }
}
