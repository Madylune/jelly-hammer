﻿using UnityEngine;

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
    private int attackDamage = 50;

    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private Transform attackPoint;

    [SerializeField]
    private float attackRangeX;

    [SerializeField]
    private float attackRangeY;

    [SerializeField]
    private LayerMask enemyLayers;

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

            Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(attackPoint.position, new Vector2(attackRangeX, attackRangeY), 0, enemyLayers);

            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPoint.position, new Vector3(attackRangeX, attackRangeY, 1));
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