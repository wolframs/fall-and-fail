﻿using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SwordSlash : Ability
{
    private Collider2D swordCollider = null;

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

        // Zugehörige Collider cachen
        swordCollider = GameObject.Find("SwordCollider").GetComponent<Collider2D>();

        // Ability Properties
        this.abilityName = "Sword Slash";
        this.type = AbilityClass.Physical;
        this.inertia = Inertia.Instant;
        this.target = Target.Enemy;
        this.healthChange = 20;
        this.animationTime = 0.25f;
        this.staminaCost = 2;

        // Alle Kollisionen mit der Tilemap ignorieren
        Physics2D.IgnoreCollision(swordCollider,
            GameObject.Find("Tilemap").GetComponent<Collider2D>(),
            true);

        // Kollisionen mit Coins ignorieren
        Physics2D.IgnoreLayerCollision(8, 10); // Layer 8: Coins | Layer 10: Sword
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
        if (!inProgress && grounded)
            StartCoroutine(PerformSlash());
    }

    IEnumerator PerformSlash()
    {
        // Start
        inProgress = true;
        Animate(true);

        // Movement anhalten
        _player.GetComponent<Player>().xInput = 0;

        // Sound
        GameObject.Find("AudioMan").GetComponent<AudioManager>().Play("SwordAir");

        // Timeout
        yield return new WaitForSeconds(this.animationTime);

        // Ende
        Animate(false);
        inProgress = false;
    }

    private void Animate(bool attacks)
    {
        this.GetComponent<Animator>().SetBool("Attacks", attacks);
    }
}
