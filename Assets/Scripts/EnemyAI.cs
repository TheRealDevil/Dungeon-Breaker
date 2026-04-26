using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float detectionRange = 5f;
    private Transform player;

    void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
        //Find the player by tag
        GameObject playerObj = GameObject.FindGameObjectsWithTag("Player")[0];
        if (playerObj != null) player = playerObj.transform;

    }

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
