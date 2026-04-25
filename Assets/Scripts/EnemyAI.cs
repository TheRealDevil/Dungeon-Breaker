using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float detectionRange = 5f;
    private Transform player;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //Find the player by tag
        GameObject playerObj = GameObject.FindGameObjectsWithTag("Player")[0];
        if (playerObj != null) player = playerObj.transform;

    }

    void FixedUpdate()
    {
        if (player != null) return; //If player is not found, do nothing

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            //Calculate distance towards player
            Vector2 direction = (player.position - transform.position).normalized;
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
        }
    }
}
