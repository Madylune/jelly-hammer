﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    int maxHealth = 100;

    int currentHealth;

    protected Animator animator;

    protected SpriteRenderer spriteRenderer;

    protected float moveSpeed = 3f;

    protected bool isDead = false;

    protected Vector3 CheckPointPosition;

    public virtual void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        CheckPointPosition = transform.position;
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            StartCoroutine(Die());
        }
    }

    private IEnumerator Die()
    {
        animator.SetBool("IsDead", true);

        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;

        yield return new WaitForSeconds(2);

        Destroy(gameObject);
    }

    public void Flip(float velocity)
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

    public void RespawnAtCheckpoint()
    {
        transform.position = CheckPointPosition;
        isDead = false;
    }
}
