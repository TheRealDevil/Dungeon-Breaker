using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    //public Transform firePoint;
    public float nextFireTime = 0f;
    public float fireRate = 0.5f; //Time in seconds between shots

    void Update()
    {
        bool isShooting = Keyboard.current.spaceKey.wasPressedThisFrame || Mouse.current.leftButton.wasPressedThisFrame;

        //Get mouse position in screen coordinates
        Vector2 mousePosition = Mouse.current.position.ReadValue(); 

        //Convert to world coordinates (assuming z=10 for 2D)
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 10f)); 

        //Calculate direction from player to mouse
        Vector2 shootDirection = (worldMousePos - transform.position).normalized; 

        //Check for input
        if ((Keyboard.current.spaceKey.wasPressedThisFrame || Mouse.current.leftButton.wasPressedThisFrame) && Time.time >= nextFireTime)
        {
            Shoot(shootDirection);
            nextFireTime = Time.time + fireRate; //Update the next allowed fire time
        }
    }

    void Shoot(Vector2 direction)
    {
        //Calculate angle for bullet rotation in degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //Spawn bullet with correct rotation
        Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, angle));
    }
}
