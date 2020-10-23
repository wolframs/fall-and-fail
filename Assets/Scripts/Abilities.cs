using UnityEngine;

public class Abilities : MonoBehaviour
{
    private void Awake()
    {
        // Fügt die zur Verfügung stehenden Abilities (aus dem Order Assets/Scripts/Abilities) als Komponenten zum Spieler hinzu
        GameObject.Find("Player").AddComponent<DoubleJump>();
        GameObject.Find("Player").AddComponent<SwordSlash>();
    }
}