using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class SceneTransfer : MonoBehaviour
{
    public void GoToScene(string targetScene)
    {
        SceneManager.LoadScene(targetScene);
    }

    public void Quit()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
