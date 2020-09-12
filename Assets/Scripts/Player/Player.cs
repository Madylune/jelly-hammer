using UnityEngine;

public class Player : Character
{
    private static Player instance;

    public static Player MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<Player>();
            }
            return instance;
        }
    }

    [SerializeField]
    private Rigidbody2D rb;

    private bool isAttacking;

    public override void Start()
    {
        base.Start();
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
}
