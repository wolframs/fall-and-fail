using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour

    
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
         animator = this.GetComponentInParent<Animator>();
    }

    private void OnTriggerStay2D(Collider2D colli)
    {
        if (animator.GetBool("Attacks") && colli.gameObject.name.Equals("Wizard"))
        {
            Destroy(colli.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
