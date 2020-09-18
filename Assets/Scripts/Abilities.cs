using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour
{

    private void Awake()
    {
        GameObject.Find("Player").AddComponent<DoubleJump>();
    }
}