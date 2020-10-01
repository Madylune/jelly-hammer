using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public abstract class Character : MonoBehaviourPun
{
    [SerializeField]
    private string name;

    [SerializeField]
    private Sprite sprite;

    int maxHealth = 100;

    [SerializeField]
    private int currentHealth;

    protected Animator animator;

    private SpriteRenderer spriteRenderer;

    public bool isDead;

    protected Vector3 CheckPointPosition;

    public string MyName { get => name; set => name = value; }
    public Sprite MySprite { get => sprite; }
    public SpriteRenderer MySpriteRenderer { get => spriteRenderer; set => spriteRenderer = value; }
    public int MyCurrentHealth { get => currentHealth; set => currentHealth = value; }

    private PhotonView _photonView;

    public virtual void Start()
    {
        animator = GetComponent<Animator>();
        MySpriteRenderer = GetComponent<SpriteRenderer>();
        _photonView = GetComponent<PhotonView>();

        CheckPointPosition = transform.position;
        MyCurrentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        MyCurrentHealth -= damage;

        animator.SetTrigger("Hurt");

        if (MyCurrentHealth <= 0)
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

        yield return new WaitForSeconds(2f);

        Destroy(gameObject);
        PhotonNetwork.Destroy(this._photonView);
    }

    public void Flip(float velocity)
    {
        if (velocity > 0.1f)
        {
            MySpriteRenderer.flipX = false;
        }
        else if (velocity < -0.1f)
        {
            MySpriteRenderer.flipX = true;
        }
    }
}
