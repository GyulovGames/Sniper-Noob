using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameCanvas : MonoBehaviour
{
    [SerializeField] public int goldenBullets;
    [Space(20)]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private FadeController fadeController;
    [Space(10)]
    [SerializeField] private Sprite goldenBulletSprite;
    [SerializeField] private Sprite regularBulletSprite;
    [SerializeField] private Sprite emptyBulletSprite;
    [Space(10)]
    [SerializeField] private Image starImage1;
    [SerializeField] private Image starImage2;
    [SerializeField] private Image starImage3;
    [Space(10)]
    [SerializeField] private CanvasGroup resultMenu;
    [SerializeField] private CanvasGroup pauseMenu;
    [SerializeField] private CanvasGroup gameMenu;
    [SerializeField] private CanvasGroup smoothTransition;
    [Space(20)]
    [SerializeField] private Text levelIndexIndicator;
    [Space(20)]
    [SerializeField] private Image[] bulletsCells;

    private int zombiesNumber;
    private int bulletsNumber;
    private int regularBullets;
    private int shootsCounter = -1;
    private int startRegularBulletsNumber;

    public static UnityEvent pauseEvent = new UnityEvent();


    private void Start()
    {
        UpdateOnStart();
        RefillBulletsMag();
        CountZombiesNumber();

        Noobik.ShootEvent.AddListener(Shoot);
        Zombie.ZombieHitEvent.AddListener(Hit);
    }

    public void PauseBtn()
    {
        audioSource.Play();
        pauseEvent.Invoke();
        fadeController.Appear(pauseMenu);
        fadeController.Disappear(gameMenu);
    }

    public void ResumeBtn()
    {
        audioSource.Play();
        pauseEvent.Invoke();
        fadeController.Appear(gameMenu);
        fadeController.Disappear(pauseMenu);
    }

    public void RestartBtn()
    {
        audioSource.Play();

        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(Delay(sceneIndex));
        fadeController.Appear(smoothTransition);
    }

    public void NexLevelBtN()
    {

    }

    public void HomeBut()
    {
        audioSource.Play();
        SceneManager.LoadScene(0);
        fadeController.Disappear(pauseMenu);
    }

    public void ResultMenuResumeBtn()
    {
        audioSource.Play();
    }

    public void ResultMenuRestartBtn() 
    {
        audioSource.Play();
    }

    public void ResultMenuHomeBtn()
    {
        audioSource.Play();
    }

    private void UpdateOnStart()
    {
        fadeController.Disappear(smoothTransition);

        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        levelIndexIndicator.text = "Level:" + sceneIndex;
    }

    private IEnumerator Delay(int levelIndex)
    {
        yield return new WaitForSeconds(1f);
        StopCoroutine(Delay(levelIndex));
        SceneManager.LoadScene(levelIndex);
    }

    private void RefillBulletsMag()
    {
        int goldenShoots = 0;

        for (int i = 0; i < bulletsCells.Length; i++)
        {
            if (goldenShoots != goldenBullets)
            {
                bulletsCells[i].sprite = goldenBulletSprite;
                goldenShoots++;
            }
            else
            {
                bulletsCells[i].sprite = regularBulletSprite;
                regularBullets++; 
                startRegularBulletsNumber++;
            }

            bulletsNumber++;
        }
    }

    private void CountZombiesNumber()
    {
        zombiesNumber = GameObject.FindGameObjectsWithTag("Zombie").Length;

    }



    private void Hit()
    {
        zombiesNumber--;
        Result();
    }

    private void Shoot()
    {
        shootsCounter++;
        bulletsNumber--;

        if (goldenBullets > 0)
        {
            goldenBullets --;
            bulletsCells[shootsCounter].sprite = emptyBulletSprite;
            Result();
        }
        else if (regularBullets > 0)
        {          
            regularBullets --;
            bulletsCells[shootsCounter].sprite = emptyBulletSprite;
            Result();
        }
    }

    private void Result()
    {
        if (zombiesNumber <= 0 || bulletsNumber <= 0)
        {
           Invoke("LoadResultMenu", 1.6f);
            fadeController.Disappear(gameMenu);
        }
        else if(bulletsNumber <= 0 && zombiesNumber <= 0)
        {
            Invoke("LoadResultMenu", 1.6f);
            fadeController.Disappear(gameMenu);
        }
    }

    private void LoadResultMenu()
    {
        if(bulletsNumber <= 0 && zombiesNumber > 0)
        {
            fadeController.Appear(resultMenu);
        }
        else if (startRegularBulletsNumber == 0)
        {
            if (goldenBullets >= 0)
            {
                starImage1.enabled = true;
                starImage2.enabled = true;
                starImage3.enabled = true;
                fadeController.Appear(resultMenu);
            }
        }
        else if (startRegularBulletsNumber > 0)
        {
            if (goldenBullets >= 0 && regularBullets == startRegularBulletsNumber)
            {
                starImage1.enabled = true;
                starImage2.enabled = true;
                starImage3.enabled = true;
                fadeController.Appear(resultMenu);
            }
            else if (goldenBullets == 0 && regularBullets < startRegularBulletsNumber && regularBullets >= 2)
            {
                starImage1.enabled = true;
                starImage2.enabled = true;
                fadeController.Appear(resultMenu);
            }
            else if (goldenBullets == 0 && regularBullets < 2)
            {
                starImage1.enabled = true;
                fadeController.Appear(resultMenu);
            }
        }
    }
}