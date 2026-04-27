using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 20;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        BossAI boss = GetComponent<BossAI>();

        //Check if the enemy is actually a boss
        if (boss != null)
        {
            boss.Die();
            return;
        }

        //Regualar enemy death
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddScore(100); //100 Points per kill
        }
        else Debug.Log("Enemy dies but no GameManager was found to add points");
       
        Destroy(gameObject);
    }
}