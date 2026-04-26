using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 40;
    private int currentHealth;
    public Slider healthBar; //Reference to UI health bar slider

    [Header("Invincibility Frames")]
    public float iFrameDuration = 1.0f; //Duration of invincibility after taking damage
    private float iFrameTimer;
    private SpriteRenderer sprite; //Reference to player's sprite renderer for visual feedback

    void Start()
    {
        currentHealth = maxHealth;
        sprite = GetComponent<SpriteRenderer>();
        if (healthBar != null)
        {
            healthBar.minValue = 0;
            healthBar.maxValue = maxHealth;
            healthBar.value = maxHealth; //Initialize health bar UI
        }
    }

    public void TakeDamage(int damage)
    {
        if (iFrameTimer > 0) return;

        currentHealth -= damage;
        iFrameTimer = iFrameDuration; //Start invincibility timer
        Debug.Log("Player took " + damage + " damage! Current health: " + currentHealth);

        if (healthBar != null)
        {
            healthBar.value = currentHealth; //Update health bar UI
        }
        if (currentHealth <= 0) Die();
    }

    void Update()
    {
        if (iFrameTimer > 0)
        {
            iFrameTimer -= Time.deltaTime;

            //Visual feedback: Flicer player sprite during invincibility frames
            float flicker = Mathf.Sin(Time.time * 20) > 0 ? 1f : 0.3f; //Flicker between normal and semi-transparent
            sprite.color = new Color(1, 1, 1, flicker);
        }
        else
        {
            //Reset sprite color when not invincible
            sprite.color = new Color(1, 1, 1, 1);
        }
    }

    void Die()
    {
        //Add death effects here later
        Debug.Log("Player has died!");
        //For now restart the current floor
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
