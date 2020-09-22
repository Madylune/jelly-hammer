using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private MyPlayer myPlayer;
    private Animator animator;

    private void Start()
    {
        myPlayer = gameObject.GetComponent<MyPlayer>();
        animator = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        HandleInputs();
    }

    private void FixedUpdate()
    {
        Walk();
    }

    private void HandleInputs()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            myPlayer.IsAttacking = true;
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


        if (!animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            myPlayer.MyRb.MovePosition(myPlayer.MyRb.position + movement * Time.fixedDeltaTime);
        }

        myPlayer.Flip(movement.x);
        myPlayer.AnimateWalk(movement);
    }
}
