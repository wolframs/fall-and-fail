using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollection : MonoBehaviour
{
    public GameObject gameobject;
    public CircleCollider2D colli;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter2D()
    {
        Destroy(gameobject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
