using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public Collider2D Trigger;

    private AudioManager audioManager;
    private Animator thisAnimator;
    public int animationTimeoutFrames;
    private int frameTimer = 0;
    private bool animationTimeout = false;

    private bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        thisAnimator = this.GetComponent<Animator>();
        audioManager = GameObject.Find("AudioMan").GetComponent<AudioManager>();

        // Coin Collider ignoren
        Physics2D.IgnoreLayerCollision(8, 9); // Layer 8: Coins | Layer 9: Enemies
    }

    private void OnTriggerExit2D(Collider2D colli)
    {

        if (colli.gameObject.name.Equals("Tilemap"))
        {
            speed *= -1;
            float newpositionX = transform.localScale.x * -1;
            transform.localScale = new Vector3(newpositionX, transform.localScale.y, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (animationTimeout)
        {
            // Sword Collider in dieser Zeit ignorieren
            Physics2D.IgnoreLayerCollision(9, 10, true);

            frameTimer += 1;
            if (frameTimer >= animationTimeoutFrames)
            {
                animationTimeout = false;
                thisAnimator.SetBool("takesDamage", false);
                frameTimer = 0;
                Physics2D.IgnoreLayerCollision(9, 10, false);
            }
        }
        else if (!isDead)
        {
            float newpositionX = transform.position.x + speed * Time.deltaTime;
            transform.position = new Vector3(newpositionX, transform.position.y, 0);
        };
    }

    public void TakeDamage()
    {
        thisAnimator.SetBool("takesDamage", true);
        animationTimeout = true;
        audioManager.Play("WizzoTakesDamage");
    }

    public void DieAHorribleDeath()
    {
        isDead = true;

        // Collider deaktivieren
        Physics2D.IgnoreLayerCollision(0, 9); // Layer 0: Default | Layer 9: Enemies

        // Sound abspielen
        audioManager.PlayWithDelay("WizzoDies", 0.55f);

        // Death Animation
        thisAnimator.SetTrigger("dies");

        // Transform korrigieren
        transform.position = new Vector2(transform.position.x, transform.position.y - 0.04f);
    }
}
