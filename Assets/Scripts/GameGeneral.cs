using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameGeneral : MonoBehaviour
{
    private void Update()
    {
        CheckSelfDestroy();
    }

    IEnumerator SoundTimeoutGameOver()
    {
        FindObjectOfType<AudioManager>().Play("PlayerDeath");
        GameObject.Find("spaceship_2").SetActive(false);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("GameOver");
    }

    public void EndGame()
    {
        PlayerPrefs.SetInt("success", 0);
        StartCoroutine(SoundTimeoutGameOver());
    }

    public void EndGameFromSuccess()
    {
        PlayerPrefs.SetInt("success", 1);
        SceneManager.LoadScene("GameOver");
    }

    public void CheckSelfDestroy()
    {
        // Zum Debuggen ayayayaya
        if (Input.GetKeyDown(KeyCode.F10))
        {
            EndGame();
        }
        if (Input.GetKeyDown(KeyCode.F11))
        {
            EndGameFromSuccess();
        }
    }
}
