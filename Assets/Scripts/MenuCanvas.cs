using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuCanvas : MonoBehaviour
{
    [SerializeField] private Animator canvasAnimator;
    [SerializeField] private AudioSource audioSource;

    public void StartGameBtn()
    {
        audioSource.Play();
        canvasAnimator.SetTrigger("StartGame");
    }

    public void LevelsBtn()
    {
        audioSource.Play();
        canvasAnimator.SetTrigger("Levels");
    }

    public void CloseLevelsBtn()
    {
        audioSource.Play();
        canvasAnimator.SetTrigger("CloseLevels");
    }
    public void LoadLevelBtn(int levelIndex)
    {
        audioSource.Play();
        canvasAnimator.SetTrigger("LoadLevel");

        SceneManager.LoadScene(levelIndex);
    }




    public void AllGamesBtn()
    {
        audioSource.Play();
        canvasAnimator.SetTrigger("AllGames");
    }

    public void AllGamesYesBtn()
    {
        audioSource.Play();
        canvasAnimator.SetTrigger("CloseAllGames");
    }

    public void AllGamesNoBtn()
    {
        audioSource.Play();
        canvasAnimator.SetTrigger("CloseAllGames");
    }
}
