using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [Header("Combat settings")]
    public int damageAmount = 10;
    public bool destroyOnTouch = false; //Set this to true for bullets/projectiles if needed

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth health = collision.gameObject.GetComponent<PlayerHealth>();

            if (health != null)
            {
                health.TakeDamage(damageAmount);
                Debug.Log("Boss dealt" + damageAmount + "damage");
            }

            //if the boss uses projectiles
            if (destroyOnTouch) Destroy(gameObject);
        }
    }
}