using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 20;
    private int currentHealth;

    [Header("Visuals")]
    private SpriteRenderer sprite;
    public float flickerDuration = 0.1f;
    private float flickerTimer;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        flickerTimer = flickerDuration;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Update()
    {
        if (flickerTimer > 0)
        {
            flickerTimer -= Time.deltaTime;
            bool isRed = Mathf.Sin(Time.time * 40) > 0;
            sprite.color = isRed ? Color.red : Color.white;
        }
        else sprite.color = Color.white;
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