using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJump : Ability
{
    public bool inProgress
    {
        get { return GameState.jumpInProgress; }
        set { GameState.jumpInProgress = value; }
    }
    private bool grounded;

    public void Awake()
    {
        inProgress = false;
        _player = GameObject.Find("Player");
    }

    public void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Use();
        }
    }

    public override void Use()
    {
        grounded = GameState.playerGrounded;
        if (Input.GetButtonDown("Jump") && grounded)
        {
            _player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, GameState.jumpForceY));
            GameState.playerGrounded = false;
        }
        else if (!grounded && !inProgress)
        {
            inProgress = true;
            _player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, GameState.jumpForceY));
        }
        animate(0, !GameState.playerGrounded);
    }

    void animate(int xInput, bool jump)
    {
        this.GetComponent<Animator>().SetFloat("Speed", Mathf.Abs(xInput));
        this.GetComponent<Animator>().SetBool("Jumps", !grounded);
    }
}
