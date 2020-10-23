using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    public string abilityName;

    public Target target;
    public AbilityClass type;
    public Inertia inertia;
    public int inertiaTickCount;
    public float inertiaTickRate;
    public float animationTime;

    public int healthChange;
    public int manaCost;

    [HideInInspector]
    public GameObject _player;

    public enum Target
    {
        None,
        Enemy,
        Self
    }

    public enum AbilityClass
    {
        Movement,
        Physical,
        Magic
    }
    public enum Inertia
    {
        Instant,
        DamageOverTime
    }
    public virtual void Use()
    {

    }
}
