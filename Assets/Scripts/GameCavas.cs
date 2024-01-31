using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;

public class GameCanvas : MonoBehaviour
{
    [SerializeField] private int goldenShoots;
    [Space(25)]
    [SerializeField] private Animator canvasAnimator;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Sprite goldenBullet;
    [SerializeField] private Sprite sliverBullet;
    [SerializeField] private Sprite emptyBullet;
    [Space(25)]
    [SerializeField] private Image[] bulletsImages;
    [SerializeField] private Image starImage1;
    [SerializeField] private Image starImage2;
    [SerializeField] private Image starImage3;

    public static UnityEvent pauseEvent = new UnityEvent();

    private int shootsCounter = -1;
    private int zombiesNumber = 0;


    private void Start()
    {
        RefillBulletsMag();
        CountZombiesNumber();

        Noobik.shootEvent.AddListener(Shoot);
        Zombie.ZombieHitEvent.AddListener(HitZombie);
    }


    private void RefillBulletsMag()
    {
        int bullets = 0;

        for (int i = 0; i < bulletsImages.Length; i++)
        {
            if (bullets != goldenShoots)
            {
                bulletsImages[i].sprite = goldenBullet;
                bullets++;
            }
            else
            {
                bulletsImages[i].sprite = sliverBullet;
            }
        }
    }

    private void CountZombiesNumber()
    {
        zombiesNumber = GameObject.FindGameObjectsWithTag("Zombie").Length;
    }

    private void HitZombie()
    {
        zombiesNumber--;

        if(zombiesNumber <= 0)
        {
            canvasAnimator.SetTrigger("GameResult");
        }
    }


    private void Shoot()
    {
        shootsCounter++;
        goldenShoots--;

        bulletsImages[shootsCounter].sprite = emptyBullet;

        if (goldenShoots >= 0)
        {
            starImage1.enabled = true;
            starImage2.enabled = true;
            starImage3.enabled = true;
        }

        if(shootsCounter >= 9)
        {
            canvasAnimator.SetTrigger("GameResult");
        }
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

        pauseEvent.Invoke();
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

    private void OnLevelWasLoaded(int level)
    {
        
    }
}