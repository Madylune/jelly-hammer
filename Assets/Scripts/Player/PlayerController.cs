using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviour
{
    private MyPlayer myPlayer;
    private Animator animator;
    private PhotonView photonView;

    [SerializeField]
    private GameObject attackHitBox;

    private bool isAttacking;
    public bool IsAttacking { get => isAttacking; set => isAttacking = value; }

    private void Start()
    {
        myPlayer = GetComponent<MyPlayer>();
        animator = GetComponent<Animator>();
        photonView = GetComponent<PhotonView>();

        attackHitBox.SetActive(false);
    }

    private void Update()
    {
        HandleInputs();
    }

    private void FixedUpdate()
    {
        Walk();
        StartCoroutine(Attack());
        ResetValues();
    }

    private void HandleInputs()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            IsAttacking = true;
        }

        if (Input.GetKeyDown(KeyCode.B) && myPlayer.MyBombs > 0)
        {
            myPlayer.DropBomb();
        }
    }

    private void Walk()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        Vector2 inputVector = new Vector2(inputX, inputY);
        Vector2 movement = inputVector * myPlayer.MyMoveSpeed;

        if (photonView.IsMine)
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
            {
                myPlayer.MyRb.MovePosition(myPlayer.MyRb.position + movement * Time.fixedDeltaTime);
            }

            myPlayer.Flip(movement.x);
            myPlayer.AnimateWalk(movement);
        }
    }

    private IEnumerator Attack()
    {
        if (photonView.IsMine)
        {
            if (isAttacking && !animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
            {
                animator.SetTrigger("Attack");
                myPlayer.MyRb.velocity = Vector2.zero;

                float attackHitBoxY = attackHitBox.GetComponent<BoxCollider2D>().offset.y;
                if (GameManager.MyInstance.MyCharacter == "Panda")
                {
                    attackHitBox.GetComponent<BoxCollider2D>().offset = myPlayer.MySpriteRenderer.flipX ? new Vector2(-1.45f, attackHitBoxY) : new Vector2(1.45f, attackHitBoxY);
                }
                else if (GameManager.MyInstance.MyCharacter == "Raccoon")
                {
                    attackHitBox.GetComponent<BoxCollider2D>().offset = myPlayer.MySpriteRenderer.flipX ? new Vector2(-0.96f, attackHitBoxY) : new Vector2(0.96f, attackHitBoxY);
                }
                attackHitBox.SetActive(true);

                yield return new WaitForSeconds(.5f);

                attackHitBox.SetActive(false);
                isAttacking = false;
            }
        }
    }

    private void ResetValues()
    {
        isAttacking = false;
    }
}
