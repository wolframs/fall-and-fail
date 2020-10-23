using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameState : MonoBehaviour
{
    // Da eine "MovoBehaviour" derived class nicht static sein darf, ermöglichen wir hier die Erstellung einer öffentlichen statischen Instanz
    public static GameState instance = null;

    public static bool playerGrounded;
    public static bool jumpInProgress;
    public static bool slashInProgress;

    public static UnityEngine.UI.Image HPFiller = null;
    public static UnityEngine.UI.Image StaminaFiller = null;

    public static float jumpForceY = 200;
    public static int playerHP {
        set {
            HPFiller.fillAmount = (1 / value);
        }
    }
    public static int playerStamina {
        set
        {
            StaminaFiller.fillAmount = (1 / value);
        }
    }

    private void Awake()
    {
        // Hier erstellen wir ggf. die öffentliche statische Instanz der Klasse, wenn das Script irgendwo in einem GameObject eingebunden ist
        if (instance == null)
        {
            instance = this;
            HPFiller = GameObject.Find("HPFiller").GetComponent<UnityEngine.UI.Image>();
            StaminaFiller = GameObject.Find("StaminaFiller").GetComponent<UnityEngine.UI.Image>();

            playerHP = 100;
            playerStamina = 100;
        }
            
        // Ggf. verhindern, dass die Instanzen mehrfach erstellt werden, wenn das Script mehr als einmal in einer Szene eingebunden ist
        else if (instance != this)
            Destroy(gameObject);

        // Das hier ist halt wichtig, stand so im Internetz, war uns jetzt nicht wichtig genug, um das zu googlen
        DontDestroyOnLoad(gameObject);
    }
}
