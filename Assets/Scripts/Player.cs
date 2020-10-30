using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    public Rigidbody2D rb2;
    public Collider2D colli;
    public bool goesleft;
    public float faktor = 10;
    public float xInput;

    // Die "Death Threshold" Höhe gibt an, ab welcher Position der Spieler als "heruntergefallen" gilt
    private float deathThreshold;
    private GameState gameState;

    public bool grounded
    {
        get { return GameState.playerGrounded; }
        set { GameState.playerGrounded = value; }
    }

    public bool inProgress
    {
        get { return GameState.jumpInProgress; }
        set { GameState.jumpInProgress = value; }
    }
    private float jumpForceY;
    
    // Start is called before the first frame update
    void Start()
    {
        init();
    }
    void OnTriggerEnter2D()
    {
        // Hier wird die Kollision mit dem Boden registriert und der Doppelsprung zurückgesetzt
        Debug.Log("Bodenhaftung : " + Time.time.ToString());
        grounded = true;
        inProgress = false;
    }
    // Update is called once per frame
    void Update()
    {
        //if (transform.position.y <= -10)
        if (transform.position.y <= deathThreshold)
            die();
        else
            if (!GameState.slashInProgress)
                control(); 
    }

    void control()
    {
        xInput = Input.GetAxis("Horizontal");
        /* Hier wird bei jedem Frame entsprechend dem Keyboardinput die neue Position berechnet                     *
         * goesleft ermittelt ob der Spieler sich nach links oder rechts fortbewegt                                 *
         * Wenn sich der Zustand im Vergleich zum letzten Frame geändert hat, wird der Spielersprite gedreht        */
        if (xInput != 0)
        {
            bool xgoesleft = goesleft;
            if (xInput < 0 && !goesleft)
            {
                goesleft = true;
            }
            else if (xInput >= 0)
            {
                goesleft = false;
            }
            if (goesleft != xgoesleft)
            {
                transform.localScale = new Vector3((transform.localScale.x * (-1)), transform.localScale.y, transform.localScale.z);
            }
            
        }
        
        
        //if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        if (Input.GetKey("Horizontal"))
        {
            float newpositionX = transform.position.x + xInput * Time.deltaTime * faktor;
            transform.position = new Vector3(newpositionX, transform.position.y, 0);
            animate(xInput, !grounded);
        }
        else
        {
            animate(0, !grounded);
        }

    }



    void init()
    {
        // Hier wird initial die Ausrichtung des Spielers gesetzt
        goesleft = false;

        // Berechnung des "Death Thresholds", also der y-Position, ab der der Spieler als Gefallen gilt
        float initialHeight = transform.position.y;
        deathThreshold = initialHeight - 12;
    }

    void die()
    {
        SceneManager.LoadScene("GameOver");
    }
    void animate(float xInput, bool jump)
    {
        this.GetComponent<Animator>().SetFloat("Speed", Mathf.Abs(xInput));
        this.GetComponent<Animator>().SetBool("Jumps", !GameState.playerGrounded);
    }
}