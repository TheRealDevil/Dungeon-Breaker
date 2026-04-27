using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    private float nextFireTime;
    private PlayerHealth healthScript;


    void Start()
    {
        healthScript = GetComponent<PlayerHealth>();
    }

    void Update()
    {
        //Safety check to ensure the data is loaded before trying to shoot
        if (healthScript.myData == null) return;

        //Get mouse position in screen coordinates
        Vector2 mousePosition = Mouse.current.position.ReadValue(); 

        //Convert to world coordinates (assuming z=10 for 2D)
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 10f)); 

        //Calculate direction from player to mouse
        Vector2 shootDirection = (worldMousePos - transform.position).normalized; 

        bool isFiring = Keyboard.current.spaceKey.isPressed || Mouse.current.leftButton.isPressed;

        //Check for input
        if (isFiring && Time.time >= nextFireTime)
        {
            Shoot(shootDirection);
            nextFireTime = Time.time + healthScript.myData.fireRate; //Update the next allowed fire time
        }
    }

    void Shoot(Vector2 direction)
    {
        //Calculate angle for bullet rotation in degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //Spawn bullet with correct rotation
        Instantiate(healthScript.myData.bulletPrefab, transform.position, Quaternion.Euler(0, 0, angle));
    }
}
