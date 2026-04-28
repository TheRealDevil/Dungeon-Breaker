using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 20;
    private int currentHealth;
    private SpriteRenderer sprite;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        float flicker = Mathf.Sin(Time.time * 20) > 0 ? 1f : 0.3f;
        sprite.color = new Color(1, 1, 1, flicker);

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