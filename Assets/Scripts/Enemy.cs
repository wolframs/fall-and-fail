using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public Collider2D Trigger;

    private Animator thisAnimator;
    public int animationiTimeoutFrames = 49;
    private int frameTimer = 0;
    private bool animationTimeout = false;

    // Start is called before the first frame update
    void Start()
    {
        thisAnimator = this.GetComponent<Animator>();
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
        float newpositionX = transform.position.x + speed * Time.deltaTime;
        transform.position = new Vector3(newpositionX, transform.position.y, 0);

        if (animationTimeout)
        {
            frameTimer += 1;
            if (frameTimer >= animationiTimeoutFrames)
            {
                animationTimeout = false;
                thisAnimator.SetBool("takesDamage", false);
                frameTimer = 0;
            }
        }
    }

    public void TakeDamage()
    {
        thisAnimator.SetBool("takesDamage", true);
        animationTimeout = true;
    }
}
