using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float detectionRange = 5f;
    private Transform player;
    //private Rigidbody2D rb;

    void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
        //Find the player by tag
        GameObject playerObj = GameObject.FindGameObjectsWithTag("Player")[0];
        if (playerObj != null) player = playerObj.transform;

    }

    /*
    void FixedUpdate()
    {
        if (player != null) 
        {
            GameObject playerObj = GameObject.FindGameObjectsWithTag("Player")[0];
            if (playerObj != null) player = playerObj.transform;
            return; //If player is not found, do nothing
        }

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < detectionRange)
        {
            //Calculate distance towards player
            Vector2 direction = (player.position - transform.position).normalized;
            rb.linearVelocity = direction * moveSpeed * Time.fixedDeltaTime;
            //rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
        }
        else
        {
            rb.linearVelocity = Vector2.zero; //Stop moving if player is out of range
        }
    }*/

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Damage player here (e.g., call a method on the player's health script)
            Debug.Log("Enemy hit the player!");
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
        }
    }
}
