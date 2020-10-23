using UnityEngine;

public class DoubleJump : Ability
{
    public bool inProgress
    {
        get { return GameState.jumpInProgress; }
        set { GameState.jumpInProgress = value; }
    }
    private bool grounded {
        get { return GameState.playerGrounded; }
        set { GameState.playerGrounded = value; }
    }

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
        if (Input.GetButtonDown("Jump") && grounded)
        {
            _player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, GameState.jumpForceY));
            grounded = false;
        }
        else if (!grounded && !inProgress)
        {
            // Velocity resetten, bevor die Force vom zweiten Jump hinzugefügt wird
            _player.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
            inProgress = true;
            _player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, GameState.jumpForceY));
        }
        animate(0, !grounded);
    }

    void animate(int xInput, bool jump)
    {
        this.GetComponent<Animator>().SetFloat("Speed", Mathf.Abs(xInput));
        this.GetComponent<Animator>().SetBool("Jumps", !grounded);
    }
}
