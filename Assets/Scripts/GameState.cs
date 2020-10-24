using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.UIElements;

/// <summary>
/// Diese Klasse funktioniert als zentraler "Informationsverteiler" für alle spielübergreifenden Einstellungen, Statuszustände und sonstige generelle, variable Optionen dar.
/// </summary>

public class GameState : MonoBehaviour
{
    // Da eine "MovoBehaviour" derived class nicht static sein darf, ermöglichen wir hier die Erstellung einer öffentlichen statischen Instanz
    public static GameState instance = null;

    // Spielerstatus
    public static bool playerGrounded;
    public static bool jumpInProgress;
    public static bool slashInProgress;

    // Jump Force
    public static float jumpForceY = 200;

    // HP & Stamina
    public static UnityEngine.UI.Image HPFiller = null;
    public static UnityEngine.UI.Image StaminaFiller = null;
    private static int _playerHP;
    private static int _playerStamina;
    public static int playerHP {
        set {
            _playerHP = value;
            HPFiller.fillAmount = (100 / value);
        }
        get { return _playerHP; }
    }
    public static int playerStamina {
        set
        {
            _playerStamina = value;
            StaminaFiller.fillAmount = (100 / value);
        }
        get { return _playerStamina; }
    }

    // Game Settings
    public static float musicVolume;
    public static float sfxVolume;
    public static int difficulty;

    // Audio Mixers
    public AudioMixer musicMixer;
    public AudioMixer sfxMixer;

    private void Awake()
    {
        // Hier erstellen wir ggf. die öffentliche statische Instanz der Klasse, wenn das Script irgendwo in einem GameObject eingebunden ist
        if (instance == null)
        {
            instance = this;

            // HP & Stamina initialisieren
            try
            {
                HPFiller = GameObject.Find("HPFiller").GetComponent<UnityEngine.UI.Image>();
                StaminaFiller = GameObject.Find("StaminaFiller").GetComponent<UnityEngine.UI.Image>();
                playerHP = 100;
                playerStamina = 100;
            }
            catch
            {
                Debug.LogWarning("HPFiller and StaminaFiller not found.");
            }

            // PlayerPfers holen
            musicVolume = PlayerPrefs.GetFloat("volumeMusic", -5f);
            if (musicVolume == 0)
                musicVolume = 0.8f;
            sfxVolume = PlayerPrefs.GetFloat("volumeSFX", -5f);
            difficulty = PlayerPrefs.GetInt("difficulty", 0);

            Debug.Log("Player Prefs: musicVolume - " + musicVolume + " | sfxVolume - " + sfxVolume + " | difficulty - " + difficulty);

            // Audio Mixer getten und Lautstärke setzen
            if (musicMixer != null && sfxMixer != null)
            {
                musicMixer.SetFloat("volume", musicVolume);
                sfxMixer.SetFloat("volume", sfxVolume);
            }
            else
            {
                Debug.LogError("Mixers not found!");
            }
        }
        // Ggf. verhindern, dass die Instanzen mehrfach erstellt werden, wenn das Script mehr als einmal in einer Szene eingebunden ist
        else if (instance != this)
            Destroy(gameObject);

        // Verhindert, dass das Script sich selbst abschießt, bevor es durch alle Eventualität gesteppt ist
        DontDestroyOnLoad(gameObject);
    }
}
