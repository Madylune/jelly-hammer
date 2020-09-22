using System.Collections;
using UnityEngine;

public class MyPlayer : Character
{
    private static MyPlayer instance;

    public static MyPlayer MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<MyPlayer>();
            }
            return instance;
        }
    }

    public float MyScore { get => score; set => score = value; }
    public int MyDiamonds { get => diamonds; set => diamonds = value; }
    public int MyBombs { get => bombs; set => bombs = value; }
    public int MyAttackDamage { get => attackDamage;  }

    [SerializeField]
    private int attackDamage = 50;

    [SerializeField]
    private float score;

    [SerializeField]
    private int diamonds;

    [SerializeField]
    private int bombs;

    [SerializeField]
    private DropBomb dropBomb;

    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private GameObject attackHitBox;

    private bool isAttacking;

    public override void Start()
    {
        attackHitBox.SetActive(false);
        base.Start();
    }

    private void Update()
    {
        HandleInput();
    }

    private void FixedUpdate()
    {
        Walk();
        StartCoroutine(Attack());
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

    private IEnumerator Attack()
    {
        if (isAttacking && !animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            animator.SetTrigger("Attack");
            rb.velocity = Vector2.zero;

            float attackHitBoxY = GetComponent<BoxCollider2D>().offset.y;
            attackHitBox.GetComponent<BoxCollider2D>().offset = spriteRenderer.flipX ? new Vector2(-1.45f, attackHitBoxY) : new Vector2(1.45f, attackHitBoxY);
            attackHitBox.SetActive(true);

            yield return new WaitForSeconds(.5f);

            attackHitBox.SetActive(false);
            isAttacking = false;
        }
    }

    public void EarnPoints(float _points)
    {
        MyScore += _points;
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isAttacking = true;
        }

        if (Input.GetKeyDown(KeyCode.B) && bombs > 0)
        {
            DropBomb();
            bombs--;
        }
    }

    private void DropBomb()
    {
        Vector2 position = spriteRenderer.flipX ? new Vector2((transform.position.x + 1), transform.position.y) : new Vector2((transform.position.x - 1), transform.position.y);
        Instantiate(dropBomb, position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Loot")
        {
            string collisionName = collision.GetComponent<Loot>().lootName;
            if (collisionName == "Diamond")
            {
                MyDiamonds++;
            }
            if (collisionName == "Bomb")
            {
                MyBombs++;
            }
            Destroy(collision.gameObject);
        }
    }

    private void ResetValues()
    {
        isAttacking = false;
    }
}
