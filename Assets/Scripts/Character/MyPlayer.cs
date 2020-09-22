using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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
    public Vector3 MySmoothMove { get => smoothMove; set => smoothMove = value; }
    public bool IsAttacking { get => isAttacking; set => isAttacking = value; }
    public Rigidbody2D MyRb { get => rb; set => rb = value; }
    public float MyMoveSpeed { get => moveSpeed; set => moveSpeed = value; }

    [SerializeField]
    private float moveSpeed = 3f;

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

    [SerializeField]
    private Text usernameText;

    private bool isAttacking;

    private Vector3 smoothMove;

    public override void Start()
    {
        attackHitBox.SetActive(false);
        base.Start();
    }

    private void Update()
    {
        usernameText.text = MyName;
    }

    private void FixedUpdate()
    {
        StartCoroutine(Attack());
        ResetValues();
    }

    private IEnumerator Attack()
    {
        if (isAttacking && !animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            animator.SetTrigger("Attack");
            MyRb.velocity = Vector2.zero;

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

    public void SmoothMovement()
    {
        transform.position = Vector3.Lerp(transform.position, MySmoothMove, Time.deltaTime * 10);
        Flip(MySmoothMove.x);
        AnimateWalk(MySmoothMove);
    }

    public void AnimateWalk(Vector2 move)
    {
        if (move.x == 0 && move.y == 0)
        {
            animator.SetBool("IsWalking", false);
        }
        else
        {
            animator.SetBool("IsWalking", true);
        }
    }

    public void DropBomb()
    {
        Vector2 position = spriteRenderer.flipX ? new Vector2((transform.position.x + 1), transform.position.y) : new Vector2((transform.position.x - 1), transform.position.y);
        Instantiate(dropBomb, position, Quaternion.identity);
        bombs--;
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
