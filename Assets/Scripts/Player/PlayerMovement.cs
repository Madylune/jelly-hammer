using UnityEngine;

public class PlayerMovement : MonoBehaviour 
{
	private float moveSpeed = 3f;

	[SerializeField]
	private Animator animator;

	[SerializeField]
	private Rigidbody2D rb;

	[SerializeField]
	private SpriteRenderer spriteRenderer;

	private Vector3 CheckPointPosition;

    private bool isAttacking;

	private bool isDead = false;

	private void Start ()
	{
		CheckPointPosition = transform.position;
	}

    private void Update()
    {
        HandleInput();
    }

    private void FixedUpdate()
    {
        Walk();
        Attack();
        ResetValues();
    }

    private void Walk()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        Vector2 inputVector = new Vector2(inputX, inputY);
        Vector2 movement = inputVector * moveSpeed;


        if (!animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);
        }

        Flip(movement.x);

        if (movement.x == 0 && movement.y == 0)
        {
            animator.SetBool("IsWalking", false);
        }
        else
        {
            animator.SetBool("IsWalking", true);
        }
    }

    private void Attack()
    {
        if (isAttacking && !animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            animator.SetTrigger("Attack");
            rb.velocity = Vector2.zero;
        }
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isAttacking = true;
        }
    }

    private void ResetValues()
    {
        isAttacking = false;
    }

    private void Flip(float velocity)
    {
        if (velocity > 0.1f)
        {
            spriteRenderer.flipX = false;
        }
        else if (velocity < -0.1f)
        {
            spriteRenderer.flipX = true;
        }
    }

    public void RespawnPlayerAtCheckpoint()
	{
		transform.position = CheckPointPosition;
		isDead = false;
	}

}
