using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJump : Ability
{
    public bool inProgress;
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
        grounded = GameObject.Find("GameState").GetComponent<GameState>().playerGrounded;
        if (!grounded && !inProgress)
        {
            Debug.Log(_player);
            Rigidbody2D rb2 = _player.GetComponent<Rigidbody2D>();
            rb2.AddForce(new Vector2(0f, 200f));
        }
    }
}
