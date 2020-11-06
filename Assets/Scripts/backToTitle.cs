using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class backToTitle : MonoBehaviour
{
    public AudioSource bgMusic;
    private float duration = 2f;

    private void Start()
    {
        // Zähler usw. zurücksetzen
        GameState.coinsOwned = 0;
        GameState.playerHP = 0;
        GameState.playerStamina = 0;
        GameState.wizHP = 4;
    }

    public void TitleTransition()
    {
        StartCoroutine(MusicFadeOut());
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartCoroutine(MusicFadeOut());
        }
    }

    // "Mathf.Lerp(Start, Ende, dritter Parameter)" -> Linear Interpolation -> Berechnet eine Linerae zwischen Start und Ende und gibt einen Zwischenwert
    // auf dieser Lineare basierend auf der Quote des dritten Parameters zurück
    private IEnumerator MusicFadeOut()
    {
        float currentTime = 0;
        float start = bgMusic.volume;
        Text[] allTexts = GameObject.FindObjectsOfType<Text>();

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            bgMusic.volume = Mathf.Lerp(start, 0, currentTime / duration);
            yield return null;

            // Alle Texte ausfaden
            foreach (Text thisText in allTexts)
            {
                thisText.color = new Color(thisText.color.r, thisText.color.g, thisText.color.b, Mathf.Lerp(1, 0, currentTime / duration));
            }
        }
        SceneManager.LoadScene("Title");
        yield break;
    }
}
