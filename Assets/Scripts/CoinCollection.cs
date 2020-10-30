using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollection : MonoBehaviour
{
    public GameObject gameobject;
    public CircleCollider2D colli;

    void OnTriggerEnter2D()
    {
        Destroy(gameobject);
        GameObject.Find("AudioMan").GetComponent<AudioManager>().Play("CoinPickup");
    }
}
