using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
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

    // Coins
    public static Text coinDisplay = null;
    private static int _coinsOwned;
    public static int coinsOwned {
        get { return _coinsOwned; }
        set { 
            _coinsOwned = value;
            coinDisplay.text = value.ToString();
        }
    }

    // Wiz HP
    private static int _wizHP;
    private static GameObject[] hearts = null;
    public static int wizHP
    {
        get { return _wizHP; }
        set { 
            _wizHP = value;
            if (value > 0 && value < 5)
                hearts[_wizHP - 1].SetActive(false);
            if (_wizHP <= 0)
                Destroy(GameObject.Find("Wizard"));
        }
    }


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
    public static bool challenging;

    // Audio Mixers
    public AudioMixer musicMixer;
    public AudioMixer sfxMixer;

    private void Awake()
    {
        // Hier erstellen wir ggf. die öffentliche statische Instanz der Klasse, wenn das Script irgendwo in einem GameObject eingebunden ist
        if (instance == null)
        {
            instance = this;
        }
        // Ggf. verhindern, dass die Instanzen mehrfach erstellt werden, wenn das Script mehr als einmal in einer Szene eingebunden ist
        else if (instance != this)
            Destroy(gameObject);

        //DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
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

        // Coin Display initialisieren
        try
        {
            coinDisplay = GameObject.Find("coinText").GetComponent<Text>();
            coinDisplay.text = "0";
        }
        catch
        {
            Debug.LogWarning("Coin Display not found.");
        }

        // Wizard Herzen holen
        try
        {
            hearts = new GameObject[3];
            int i = -1;
            foreach (SpriteRenderer heart in GameObject.Find("Wizard").GetComponentsInChildren<SpriteRenderer>())
            {
                // ersten Sprite Renderer überspringen (sonst ist das der vom Wizard selbst)
                if (i > -1)
                    hearts[i] = heart.gameObject;
                i++;
            }
        }
        catch
        {
            Debug.LogError("Herzchen nicht gefunden :(");
        }

        // Wizard HP initialisieren
        wizHP = 4;

        // PlayerPfers holen
        musicVolume = PlayerPrefs.GetFloat("volumeMusic", -5f);
        if (musicVolume == 0)
            musicVolume = 0.8f;
        sfxVolume = PlayerPrefs.GetFloat("volumeSFX", -5f);
        challenging = PlayerPrefs.GetInt("difficulty", 0) != 0;

        // Difficulty gesteuerte Variablen anpassen
        if (challenging)
            jumpForceY = 120;

        Debug.Log("Player Prefs: musicVolume - " + musicVolume + " | sfxVolume - " + sfxVolume + " | challenging - " + challenging);

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
}
