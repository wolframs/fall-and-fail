using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    Animator playerAnimator;
    private bool timeout = false;
    private float timer = 0.0f;
    private float waitTime = 1.1f;

    void Start()
    {
         playerAnimator = this.GetComponentInParent<Animator>();
    }

    private void OnTriggerStay2D(Collider2D colli)
    {
        if (playerAnimator.GetBool("Attacks") && colli.gameObject.name.Equals("Wizard") && !timeout)
        {
            // Cooldown initiieren
            timeout = true;

            // HP abziehen, Hearts werden vom GameState Script gesteuert
            GameState.wizHP -= 1;
        }
    }

    void Update()
    {
        // Cooldown
        if (timeout)
        {
            timer += Time.deltaTime; // Zählt Sekunden seit dem letzten Frame
            if (timer >= waitTime)
            {
                timeout = false;
                timer = 0.0f;
            }
        }
    }
}
