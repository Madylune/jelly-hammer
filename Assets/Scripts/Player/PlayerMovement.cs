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

	private bool isDead = false;


	private void Start ()
	{
		CheckPointPosition = transform.position;
	}

    private void FixedUpdate()
    {
        Walk();
	}

    private void Walk()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        Vector2 inputVector = new Vector2(inputX, inputY);
        Vector2 movement = inputVector * moveSpeed;

        rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);

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
