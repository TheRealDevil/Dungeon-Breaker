using NUnit.Framework;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    //public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Animator anim;
    private bool isFacingRight;

    [Header("Character blueprit")]
    public CharacterData myData;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isFacingRight = false;

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        if (GameManager.Instance != null && GameManager.Instance.selectedCharacter != null)
        {
            myData = GameManager.Instance.selectedCharacter;
        }
        if (myData != null)
        {
            if (anim != null && myData.animatorController != null)
            {
                anim.runtimeAnimatorController = myData.animatorController;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Get input from WASD keys
        Vector2 input = Vector2.zero;

        if (Keyboard.current.wKey.isPressed) input.y += 1;
        if (Keyboard.current.sKey.isPressed) input.y -= 1;
        if (Keyboard.current.aKey.isPressed) input.x -= 1;
        if (Keyboard.current.dKey.isPressed) input.x += 1;
        moveInput = input.normalized; // Normalize to prevent faster diagonal movement

        if (anim != null)
        {
            //If moveInput.magnitude > 0, we are moving
            bool isMoving = moveInput.magnitude > 0;
            anim.SetBool("isMoving", isMoving);
        }

        if (moveInput.x > 0.1f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (moveInput.x < -0.1f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    void FixedUpdate()
    {
        // Move the player
        rb.MovePosition(rb.position + moveInput * myData.moveSpeed * Time.fixedDeltaTime);
    }
}
