using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 40;
    private int currentHealth;
    public Slider healthBar; //Reference to UI health bar slider

    void Start()
    {
        currentHealth = maxHealth;
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth; //Initialize health bar UI
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Player took " + damage + " damage! Current health: " + currentHealth);

        if (healthBar != null)
        {
            healthBar.value = currentHealth; //Update health bar UI
        }
        if (currentHealth <= 0) Die();
    }

    void Die()
    {
        //Add death effects here later
        Debug.Log("Player has died!");
        //For now restart the current floor
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
