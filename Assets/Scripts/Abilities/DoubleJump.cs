using UnityEngine;

public class DoubleJump : Ability
{
    private AudioManager audioManager;
    private int halfCost = 0;

    public bool inProgress
    {
        get { return GameState.jumpInProgress; }
        set { GameState.jumpInProgress = value; }
    }
    private bool grounded {
        get { return GameState.playerGrounded; }
        set { GameState.playerGrounded = value; }
    }
    public bool allowDoublejump = true;

    public void Start()
    {
        inProgress = false;
        this._player = GameObject.Find("Player");

        // Ability Properties
        this.abilityName = "Double Jump";
        this.inertia = Inertia.Instant;
        this.target = Target.Self;
        this.type = AbilityClass.Movement;
        if (GameState.challenging)
        {
            this.staminaCost = 9;
            halfCost = 3;
        }
        else
            this.staminaCost = 6;

        audioManager = GameObject.Find("AudioMan").GetComponent<AudioManager>();
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
            audioManager.Play("Jump");
            GameState.playerStamina -= halfCost;
        }
        else if (!grounded && !inProgress && allowDoublejump)
        {
            // Velocity resetten, bevor die Force vom zweiten Jump hinzugefügt wird
            _player.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
            inProgress = true;
            _player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, GameState.jumpForceY));
            GameState.playerStamina -= staminaCost;
            audioManager.Play("Jump");
        }
        Animate(0, !grounded);
    }

    private void Animate(int xInput, bool jump)
    {
        this.GetComponent<Animator>().SetFloat("Speed", Mathf.Abs(xInput));
        this.GetComponent<Animator>().SetBool("Jumps", !grounded);
    }
}
