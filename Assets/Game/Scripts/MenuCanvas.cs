using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MenuCanvas : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private FadeController fadeController;
    [Space(10)]
    [SerializeField] private CanvasGroup mainMenu;
    [SerializeField] private CanvasGroup allGamesMenu;
    [SerializeField] private CanvasGroup levelsMenu;
    [SerializeField] private CanvasGroup smoothTransition;



    private void Start()
    {
        fadeController.Disappear(smoothTransition);
    }


    public void BtnStartGame()
    {
        audioSource.Play();
        fadeController.Disappear(mainMenu);
    }

    public void BtnOpenLevels()
    {
        audioSource.Play();
        fadeController.Appear(levelsMenu);
        fadeController.Disappear(mainMenu);
    }

    public void BtnCloseLevels()
    {
        audioSource.Play();
        fadeController.Appear(mainMenu);
        fadeController.Disappear(levelsMenu);
    }

    public void BtnSounds()
    {
        audioSource.Play();
    }

    public void BtnMusic()
    {
        audioSource.Play();

    }
    public void BtnLoadLevel(int levelIndex)
    {
        audioSource.Play();
        StartCoroutine(Delay(levelIndex));
        fadeController.Appear(smoothTransition);
    }


    public void BtnOpenAllGames()
    {
        audioSource.Play();
        fadeController.Appear(allGamesMenu);
        fadeController.Disappear(mainMenu);
    }

    public void BtnCloseAllGames()
    {
        audioSource.Play();
        fadeController.Disappear(allGamesMenu);
        fadeController.Appear(mainMenu);
    }

    private IEnumerator Delay(int levelIndex)
    {
        yield return new WaitForSeconds(1f);
        StopCoroutine(Delay(levelIndex));
        SceneManager.LoadScene(levelIndex);
    }
}