using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 5;
    public float lifetime = 2f;

    void Start()
    {
        Destroy(gameObject, lifetime); //Destroy bullet after its lifetime expires
    }
    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * speed); //Move bullet forward
    }

    void OnTriggerEnter2D(Collider2D other)
    {   
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyHealth>()?.TakeDamage(damage); //Damage enemy if it has EnemyHealth component
            Destroy(gameObject); //Destroy bullet on impact
        }
        //Ignore collisions with player, other bullets, and trigger colliders (e.g., for pickups)
        if (other.gameObject.name.Contains("Door_North") || other.gameObject.name.Contains("Door_South") || other.gameObject.name.Contains("Door_East") || other.gameObject.name.Contains("Door_West")) Destroy(gameObject); //Destroy bullet if it hits a door, since doors are not solid but we still want bullets to be destroyed on impact with them
        if (other.CompareTag("Player") || other.CompareTag("EnemyBullet") || other.isTrigger) return; 

        
        else if (other.GetComponent<UnityEngine.Tilemaps.Tilemap>() != null)
        {
            if (other.gameObject.name.Contains("Walls") || other.gameObject.name.Contains("Door_North") || other.gameObject.name.Contains("Door_South") || other.gameObject.name.Contains("Door_East") || other.gameObject.name.Contains("Door_West"))
            {
                Debug.Log("Bullet hit a wall!");
                Destroy(gameObject); //Destroy bullet on impact with walls
            }
        }
    }
}
