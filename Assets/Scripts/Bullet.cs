using System;
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
            /*
            EnemyHealth health = other.GetComponent<EnemyHealth>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }

            Destroy(gameObject); //Destroy bullet on impact
            /*
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
            Destroy(gameObject); //Destroy bullet on impact*/
        }
    }

    
}
