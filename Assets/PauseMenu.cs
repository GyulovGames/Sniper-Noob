using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject gameMenu;

    public void Resume()
    {
        gameObject.SetActive(false);
        gameMenu.SetActive(true);
        Time.timeScale = 1.0f;
    }

    public void Restart()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex);
        Time.timeScale = 1.0f;
    }
}