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
    protected bool grounded = true;
    
    // Start is called before the first frame update
    void Start()
    {
        init();
    }
    void OnTriggerEnter2D()
    {
        Debug.Log("Bodenhaftung" + Time.time.ToString());
        grounded = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= -10)
            die();
        else
            control(); 
    }

    void control()
    {
       
        // Hier wird bei jedem Frame entsprechend dem Keyboardinput die neue Position berechnet
        float xInput = Input.GetAxis("Horizontal");
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
        if (Input.GetButtonDown("Jump") && grounded)
        {
            rb2.AddForce(new Vector2(0f, 200f));
            grounded = false;
        }
        animate(xInput, grounded);
        float newpositionX = transform.position.x + xInput * Time.deltaTime * faktor;
        transform.position = new Vector3(newpositionX, transform.position.y, 0);
    }



    void init()
    {
        // Hier wird initial die Position des Spielers gesetzt
        goesleft = false;
        //TODO:
    }

    void die()
    {
        SceneManager.LoadScene("Title");
    }

    void animate(float xInput, bool grounded)
    {
        this.GetComponent<Animator>().SetFloat("Speed", Mathf.Abs(xInput));
        this.GetComponent<Animator>().SetBool("Jumps", !grounded);
    }
}