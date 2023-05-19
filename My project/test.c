using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private SpriteRenderer sprite;
    private Animator anim;
    private float dirX = 0f;
    private bool isGrounded;
    [SerializeField] private float moveSpeed = 14f;
    [SerializeField] private float jumpForce = 14f;

    private DeathPanelManager deathPanelManager;

    private enum MovementState { idle, running, jumping, falling }

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        deathPanelManager = FindObjectOfType<DeathPanelManager>();
    }

    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector(dirX * moveSpeed * Time.deltaTime, 0f, 0f);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }

        UpdateAnimationState();
    }

    private void Jump()
    {
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
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

        if (GetComponent<Rigidbody2D>().velocity.y > 1f)
        {
            state = MovementState.jumping;
        }
        else if (GetComponent<Rigidbody2D>().velocity.y < -1f)
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
            deathPanelManager.ShowDeathPanel();
        }
    }

    private void OnCollisionExit2D(Collision2D collision
)
{
if (collision.gameObject.CompareTag("Ground"))
{
isGrounded = false;
}
}
private void OnTriggerEnter2D(Collider2D collision)
{
    if (collision.gameObject.CompareTag("Enemy"))
    {
        deathPanelManager.ShowDeathPanel();
    }
}}