﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladderscript2 : MonoBehaviour
{
    bool isColliding;
    GameObject playerSprite;
    // Start is called before the first frame update

    private void Awake()
    {
        isColliding= false;
        playerSprite = GameObject.Find("Player");
    }

    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == playerSprite)
        {
            isColliding = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == playerSprite)
        {
            isColliding = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isColliding && Input.GetKey(KeyCode.UpArrow))
        {
            playerSprite.GetComponent<Rigidbody2D>().gravityScale = 0;
            playerSprite.GetComponent<Rigidbody2D>().velocity = new Vector3(playerSprite.GetComponent<Rigidbody2D>().velocity.x, 1.5f, 0f);
        }
        else
        {
            playerSprite.GetComponent<Rigidbody2D>().gravityScale = 1;
        }
    }
}
