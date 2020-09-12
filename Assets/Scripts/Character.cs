using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
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
