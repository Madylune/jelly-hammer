using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField]
    private string name;

    [SerializeField]
    private Sprite sprite;

    int maxHealth = 100;

    int currentHealth;

    protected Animator animator;

    protected SpriteRenderer spriteRenderer;

    protected float moveSpeed = 3f;

    public bool isDead;

    protected Vector3 CheckPointPosition;

    public string MyName { get => name; }
    public Sprite MySprite { get => sprite; }

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
            isDead = true;
            StartCoroutine(Die());
        }
    }

    public void Stun()
    {
        animator.SetTrigger("Stun");
    }

    private IEnumerator Die()
    {
        animator.SetBool("IsDead", true);

        if (this is Enemy)
        {
            this.GetComponent<Enemy>().GetLoots();
            MyPlayer.MyInstance.EarnPoints(((Enemy)this).MyPoints);
        }

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
}
