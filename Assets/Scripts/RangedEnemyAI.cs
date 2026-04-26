using UnityEngine;

public class RangedEnemyAI : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float stopDistance = 3f; //Distance at which enemy stops moving towards player
    public float fireRate = 1f;
    private float nextFireTime;

    public GameObject bulletPrefab;
    private Transform player;

     void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0].transform;
    }

    void Update()
    {
        if (player == null) return; //If player is not found, do nothing
        
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance > stopDistance)
        {
            //Move towards player
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
        else if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate; //Update next fire time
        }
    }

    void Shoot()
    {
        Vector2 shootDirection = (player.position - transform.position).normalized;
        float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;

        //Spawn bullet with correct rotation
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, angle));
    }
}
