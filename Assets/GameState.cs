using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    // Da eine "MovoBehaviour" derived class nicht static sein darf, ermöglichen wir hier die Erstellung einer öffentlichen statischen Instanz
    public static GameState instance = null;

    public static bool playerGrounded;
    public static bool jumpInProgress;
    public static float jumpForceY = 200;

    private void Awake()
    {
        // Hier erstellen wir ggf. die öffentliche statische Instanz der Klasse, wenn das Script irgendwo in einem GameObject eingebunden ist
        if (instance == null)
            instance = this;
        // Ggf. verhindern, dass die Instanzen mehrfach erstellt werden, wenn das Script mehr als einmal in einer Szene eingebunden ist
        else if (instance != this)
            Destroy(gameObject);

        // Das hier ist halt wichtig, stand so im Internetz, war uns jetzt nicht wichtig genug, um das zu googlen
        DontDestroyOnLoad(gameObject);
    }
}
