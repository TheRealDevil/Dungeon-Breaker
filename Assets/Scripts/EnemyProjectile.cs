using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float speed = 5f;
    public int damage = 10;
    void Start()
    {
        Destroy(gameObject, 3f); //Cleanup after 3 seconds
    }

    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * speed); //Move projectile forward
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Ignore collisions with other enemies or projectiles
        if (collision.CompareTag("Enemy") || collision.CompareTag("Projectile")) return;
        if (collision.gameObject.name.Contains("Door_North") || collision.gameObject.name.Contains("Door_South") || collision.gameObject.name.Contains("Door_East") || collision.gameObject.name.Contains("Door_West")) Destroy(gameObject);
        if (collision.isTrigger) return; //Ignore trigger colliders (e.g., for pickups)
        
        if (collision.CompareTag("Player"))
        {
            //Later call a method on the player's health script to apply damage
            PlayerHealth player = collision.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(5); //Example damage value, can be adjusted or made variable
            }
            Debug.Log("Player hit by enemy projectile for " + damage + " damage!");
            Destroy(gameObject); //Destroy projectile on impact
        }
        else if (collision.GetComponent<UnityEngine.Tilemaps.Tilemap>() != null)
        {
            if (collision.gameObject.name.Contains("Walls") || collision.gameObject.name.Contains("Door_North") || collision.gameObject.name.Contains("Door_South") || collision.gameObject.name.Contains("Door_East") || collision.gameObject.name.Contains("Door_West"))
            {
                //Optional: Add wall hit effects here (e.g., particles, sound)
                Debug.Log("Projectile hit a wall!");
                Destroy(gameObject); //Destroy projectile on impact with walls
            }
        }
    }
}
