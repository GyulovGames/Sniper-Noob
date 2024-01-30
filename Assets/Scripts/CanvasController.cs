using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Events;

public class CanvasController : MonoBehaviour
{
    [SerializeField] private Animator canvasAnimator;
    [SerializeField] private AudioSource audioSource;

    public static UnityEvent pauseEvent = new UnityEvent();



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
    public void LoadLevelBtn() 
    {
        audioSource.Play();
        canvasAnimator.SetTrigger("LoadLevel");
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




    public void PauseBtn()
    {
        audioSource.Play();
        canvasAnimator.SetTrigger("Pause");

        pauseEvent.Invoke();
    }

    public void ResumeBtn()
    {
        audioSource.Play();
        canvasAnimator.SetTrigger("Resume");
    }

    public void RestartBtn()
    {
        audioSource.Play();
        canvasAnimator.SetTrigger("Restart");

        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex);
    }

    public void HomeBut()
    {
        audioSource.Play();
        canvasAnimator.SetTrigger("Home");
    }




    public void ResultMenuResumeBtn()
    {
        audioSource.Play();
        canvasAnimator.SetTrigger("ResultNextLevel");
    }

    public void ResultMenuRestartBtn() 
    {
        audioSource.Play();
        canvasAnimator.SetTrigger("ResultRestartLevel");
    }

    public void ResultMenuHomeBtn()
    {
        audioSource.Play();
        canvasAnimator.SetTrigger("ResultHome");
    }
}