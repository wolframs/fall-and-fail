using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Player : MonoBehaviour
{
    public Rigidbody2D rb2;
    public float faktor = 10;

    // Start is called before the first frame update
    void Start()
    {
        init();
    }

    // Update is called once per frame
    void Update()
    {
        control();
    }

    void control()
    {
        // Hier wird bei jedem Frame entsprechend dem Keyboardinput die neue Position berechnet
        float xInput = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump"))
        {
            rb2.AddForce(new Vector2(0f, 200f));
        }
        float newpositionX = transform.position.x + xInput * Time.deltaTime * faktor;

        transform.position = new Vector3(newpositionX, transform.position.y, 0);
    }



    void init()
    {
        // Hier wird initial die Position des Spielers gesetzt
        
        //TODO:
    }
}