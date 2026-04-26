using UnityEngine;

public class BossAI : MonoBehaviour
{
    public float moveSpeed = 2f;
    public int bossScoreValue = 1000;
    private Transform player;
    private Animator anim;

    //Reference to the RoomController so the boss can report its death
    [HideInInspector] public RoomController roomOwner;
    void Start()
    {
        player = GameObject.FindWithTag("Player")?.transform;
        anim = GetComponent<Animator>();

        //Make the boss look bigger than regulat enemies
        transform.localScale = new Vector3(2.5f, 2.5f, 1f);
    }

    void Update()
    {
        if (player != null)
        {
            //Simple chase logic
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;

            if (anim != null) anim.SetBool("isMoving", true);

            //Flip sprite
            transform.localScale = new Vector3(direction.x > 0 ? 2.5f : -2.5f, 2.5f, 1f);
        }
    }

    //When the boss is destroyed, it triggers the portal
    public void Die()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddScore(bossScoreValue);
        }

        //Tell the room its cleared so the portal spawns
        if (roomOwner != null)
        {
            roomOwner.RoomCleared();
        }

        Destroy(gameObject);
    }
}
