using UnityEditor.Callbacks;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float detectionRange = 5f;
    private Transform player;
    private Animator anim;

    void Start()
    {

        //Find the player by tag
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogWarning("EnemyAI: Player object not found with tag 'Player'.");
        }

        anim = GetComponent<Animator>(); //Get animator component for later use
    }
    //private void OnCollisionEnter2D(Collision2D collision)
    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("Enemy collided with: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Player"))
        {
            //Damage player here (e.g., call a method on the player's health script)
            Debug.Log("Enemy hit the player!");
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(5); //Example damage value
            }
        }
    }

    void Update()
    {
        if (player != null)
        {
            Debug.DrawLine(transform.position, player.position, Color.red);
        }

        if (player == null ) return; //If player is not found, do nothing

        float distance = Vector2.Distance(transform.position, player.position);


        if (distance < detectionRange)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
/*
            //Flip the sprite so it face the player
            if (direction.x > 0) transform.localScale = new Vector3(1, 1, 1);
            else if (direction.x < 0) transform.localScale = new Vector3(-1, 1, 1);*/

            if (direction.x > 0.1f)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (direction.x < -0.1f)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }
}
