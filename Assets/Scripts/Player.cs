﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    public Rigidbody2D rb2;
    public Collider2D colli;
    public float faktor = 10;
    protected bool grounded = true;
    
    // Start is called before the first frame update
    void Start()
    {
        init();
    }
    void OnTriggerEnter2D()
    {
        Debug.Log("Bodenhaftung");
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
        if (Input.GetButtonDown("Jump") && grounded)
        {
            rb2.AddForce(new Vector2(0f, 500f));
            grounded = false;
        }
        float newpositionX = transform.position.x + xInput * Time.deltaTime * faktor;

        transform.position = new Vector3(newpositionX, transform.position.y, 0);
    }



    void init()
    {
        // Hier wird initial die Position des Spielers gesetzt
        
        //TODO:
    }

    void die()
    {
        SceneManager.LoadScene("Title");
    }
}