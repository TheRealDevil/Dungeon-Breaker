using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header("Stats")]
    public int maxHealth;
    public int currentHealth; // Don't worry about the number in the Inspector

    [Header("UI & Visuals")]
    public Slider healthBar;
    private SpriteRenderer sprite;
    
    public float iFrameDuration = 1f;
    private float iFrameTimer;

    public CharacterData myData;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();

        if (GameManager.Instance != null && GameManager.Instance.selectedCharacter != null)
        {
            myData = GameManager.Instance.selectedCharacter;
        }

        if (myData != null)
        {
            maxHealth = myData.maxHealth;
            sprite.sprite = myData.characterSprite; //Automatical changes the look of the character
        }

        //Ensure the GameManager has a starting health
        if (GameManager.Instance != null && GameManager.Instance.savedPlayerHealth == -1)
        {
            GameManager.Instance.savedPlayerHealth = maxHealth;
        }

        //Find the bar on the new floor
        healthBar = FindAnyObjectByType<Slider>();
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
        }
    }

    void Update()
    {
        //This prevents "Inspector Defaults" or other scripts from resetting it.
        if (GameManager.Instance != null)
        {
            currentHealth = GameManager.Instance.savedPlayerHealth;
        }

        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }

        //Invincibility flicker logic
        if (iFrameTimer > 0)
        {
            iFrameTimer -= Time.deltaTime;
            float flicker = Mathf.Sin(Time.time * 20) > 0 ? 1f : 0.3f;
            sprite.color = new Color(1, 1, 1, flicker);
        }
        else
        {
            sprite.color = new Color(1, 1, 1, 1);
        }
    }

    public void TakeDamage(int damage)
    {
        if (iFrameTimer > 0) return;

        if (GameManager.Instance != null)
        {
            GameManager.Instance.savedPlayerHealth -= damage;
            currentHealth = GameManager.Instance.savedPlayerHealth;
        }

        iFrameTimer = iFrameDuration;

        if (currentHealth <= 0) Die();
    }

    void Die()
    {
        if (GameManager.Instance != null) 
        {
            GameManager.Instance.savedPlayerHealth = -1; //Reset for a fresh game
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}