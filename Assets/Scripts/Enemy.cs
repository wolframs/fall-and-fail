using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public Collider2D Trigger;

    // Start is called before the first frame update
    void Start()
    {
        
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
    }
}
