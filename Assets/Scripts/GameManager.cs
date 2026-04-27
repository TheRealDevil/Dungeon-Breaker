using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int score = 0;
    public int savedPlayerHealth = -1;
    public TextMeshProUGUI scoreText; //Reference to UI text element for score display
    public CharacterData selectedCharacter;



    [Header("Global entities")]
    public GameObject globalBossPrefab;
    public GameObject globalPortalPrefab;

    void Awake()
    {
        Debug.Log("SUCCES: GameManager has awakened");
        if (Instance == null)
        {
            Instance = this;

            DontDestroyOnLoad(gameObject);

            //Tell unity to run OnSceneLoaded ever time a new floor starts
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else Destroy(gameObject); //Ensure only one instance of GameManager exists
    }

    //Relink the ui when the new floor loads
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //Search the new scene for the text object
        GameObject textObj = GameObject.Find("ScoreText");

        if (textObj != null)
        {
            scoreText = textObj.GetComponent<TextMeshProUGUI>();
            UpdateScoreUI();
        }
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (scoreText != null) scoreText.text = "Score: " + score;
        else Debug.LogWarning("Manager has points but the scoreText slot it empty");
    }

    void OnDestroy()
    {
        //Cleaup the event listener if the game is completely closed
        if (Instance == this) SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
