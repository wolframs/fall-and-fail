using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SwordSlash : Ability
{
    public bool inProgress
    {
        get { return GameState.slashInProgress; }
        set { GameState.slashInProgress = value; }
    }
    private bool grounded
    {
        get { return GameState.playerGrounded; }
        set { GameState.playerGrounded = value; }
    }

    private void Awake()
    {
        inProgress = false;
        this._player = GameObject.Find("Player");

        // Ability Properties
        this.abilityName = "Sword Slash";
        this.type = AbilityClass.Physical;
        this.inertia = Inertia.Instant;
        this.target = Target.Enemy;
        this.healthChange = 20;
        this.animationTime = 0.15f;
        this.staminaCost = 2;

        // Alle Kollisionen mit der Tilemap ignorieren
        Physics2D.IgnoreCollision(GameObject.Find("SwordCollider").GetComponent<Collider2D>(),
            GameObject.Find("Tilemap").GetComponent<Collider2D>(),
            true);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Attack"))
        {
            Use();
        }
    }

    public override void Use()
    {
        // Exit wenn wir bereits attackieren
        if (inProgress)
            return;

        StartCoroutine(PerformSlash());
    }

    IEnumerator PerformSlash()
    {
        Animate(true);
        yield return new WaitForSeconds(this.animationTime);
        Animate(false);
    }

    private void Animate(bool attacks)
    {
        this.GetComponent<Animator>().SetBool("Attacks", attacks);
    }
}
