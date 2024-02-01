using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameCanvas : MonoBehaviour
{
    [SerializeField] public int goldenBullets;
    [SerializeField] public int regularBullets;
    [Space(25)]
    [SerializeField] private Animator canvasAnimator;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Sprite goldenBulletSprite;
    [SerializeField] private Sprite regularBulletSprite;
    [SerializeField] private Sprite emptyBulletSprite;
    [Space(25)]
    [SerializeField] private Image[] bulletsImages;
    [SerializeField] private Image starImage1;
    [SerializeField] private Image starImage2;
    [SerializeField] private Image starImage3;

    private int zombiesNumber;
    private int shootsCounter = -1;
    private int startRegularBulletsNumber;
    private int startGoldenBulletsNumber;
    public static UnityEvent pauseEvent = new UnityEvent();


    private void Start()
    {
        RefillBulletsMag();
        CountZombiesNumber();

        Noobik.shootEvent.AddListener(Shoot);
        Zombie.ZombieHitEvent.AddListener(HitZombie);
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
        SceneManager.LoadScene(0);

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




    private void RefillBulletsMag()
    {
        int goldenShoots = 0;

        for (int i = 0; i < bulletsImages.Length; i++)
        {
            if (goldenShoots != goldenBullets)
            {
                bulletsImages[i].sprite = goldenBulletSprite;
                goldenShoots++;
                startGoldenBulletsNumber++;
            }
            else
            {
                bulletsImages[i].sprite = regularBulletSprite;
                regularBullets++; 
                startRegularBulletsNumber++;
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
    }

    private void Shoot()
    {

        shootsCounter++;
        bulletsImages[shootsCounter].sprite = emptyBulletSprite;
    }
}