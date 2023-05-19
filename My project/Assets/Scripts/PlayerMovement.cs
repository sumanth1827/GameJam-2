using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;
    private CheckpointManager checkpointManager; // Reference to the CheckpointManager script
    private Vector3 defaultRespawnPosition;
    private float dirX = 0f;
    private bool isGrounded;
    private bool isAlive = true; // Flag to track player state
    [SerializeField] private float moveSpeed = 14f;
    [SerializeField] private float jumpForce = 14f;

    private DeathPanelManager deathPanelManager; // Reference to the DeathPanelManager script

    private pausemenu pauseMenu; // Reference to the pausemenu script

    private enum MovementState { idle, running, jumping, falling }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        // Get the DeathPanelManager component from the scene
        deathPanelManager = FindObjectOfType<DeathPanelManager>();
        checkpointManager = FindObjectOfType<CheckpointManager>();
        defaultRespawnPosition = transform.position;

        // Get the pausemenu component from the scene
        pauseMenu = FindObjectOfType<pausemenu>();
    }

    private void Update()
    {
        if (!pausemenu.GameIsPaused && isAlive) // Allow input only if the game is not paused and the player is alive
        {
            dirX = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }

        UpdateAnimationState();

        if (!isAlive && Input.GetKeyDown(KeyCode.Space))
        {
            deathPanelManager.RestartGame(); // Call the RestartGame method of the DeathPanelManager
        }
    }


    private void UpdateAnimationState()
    {
        MovementState state;

        if (dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > .8f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.8f)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("Death"))
        {
            isAlive = false; // Player is dead
            deathPanelManager.ShowDeathPanel(); // Call the ShowDeathPanel method of the DeathPanelManager
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

   private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Checkpoint"))
        {
            // Call the SetRespawnPosition method of the CheckpointManager and pass the current checkpoint position
            checkpointManager.SetRespawnPosition(collision.gameObject.transform.position);
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            isAlive = false; // Player is dead
            deathPanelManager.ShowDeathPanel(); // Call the ShowDeathPanel method of the DeathPanelManager
        }
    }
    public void Respawn(Vector3 position)
    {
        transform.position = position; // Move the player to the specified position
        isAlive = true; // Set the player to alive
    }

    public void Respawn()
    {
        if (checkpointManager != null)
        {
            checkpointManager.RespawnPlayer(defaultRespawnPosition); // Call the RespawnPlayer method of the CheckpointManager with the default respawn position
        }
        else
        {
            transform.position = defaultRespawnPosition; // Respawn at the default position if no checkpoint has been reached
            isAlive = true; // Set the player to alive
        }
    }
}

