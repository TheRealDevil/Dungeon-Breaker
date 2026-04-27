using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //This restarts the scene, triggers the DungeonGenerator to build a new floor
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}