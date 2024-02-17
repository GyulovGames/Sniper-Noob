using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using YG;
using UnityEditor;

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
    [SerializeField] private Text resultMenuLevelIndexIndicator;
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
        UpdateSoundsSettings();

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

    public void HomeBut()
    {
        audioSource.Play();
        StartCoroutine(Delay(0));
        fadeController.Appear(smoothTransition);
    }




    public void ResultMenuResumeBtn()
    {
        int i = SceneManager.GetActiveScene().buildIndex;

        if(i == 100)
        {
            audioSource.Play();
            int randomScene = Random.Range(1, 99);
            StartCoroutine(Delay(randomScene));
            fadeController.Appear(smoothTransition);
        }
        else
        {
            audioSource.Play();
            int sceneIndex = SceneManager.GetActiveScene().buildIndex;
            StartCoroutine(Delay(sceneIndex + 1));
            fadeController.Appear(smoothTransition);
        }
    }

    public void ResultMenuRestartBtn() 
    {
        audioSource.Play();
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(Delay(sceneIndex));
        fadeController.Appear(smoothTransition);
    }

    public void ResultMenuHomeBtn()
    {
        audioSource.Play();
        StartCoroutine(Delay(0));
        fadeController.Appear(smoothTransition);
    }



    private void UpdateSoundsSettings()
    {
        bool buttonsSounds = YandexGame.savesData.sounds;

        AudioSource[] audioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];

        if(buttonsSounds) 
        { 
            audioSource.volume = 1.0f;
        }
        else { audioSource.volume = 0.0f; }
    }
    private void UpdateOnStart()
    {
        fadeController.Disappear(smoothTransition);

        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        levelIndexIndicator.text   += sceneIndex;
        resultMenuLevelIndexIndicator.text  += sceneIndex;
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
        if (bulletsNumber <= 0 && zombiesNumber > 0)
        {
            fadeController.Appear(resultMenu);
            SaveLevelData(0);
        }
        else if (startRegularBulletsNumber == 0)
        {
            if (goldenBullets >= 0)
            {
                starImage1.enabled = true;
                starImage2.enabled = true;
                starImage3.enabled = true;
                fadeController.Appear(resultMenu);
                SaveLevelData(3);
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
                SaveLevelData(3);

            }
            else if (goldenBullets == 0 && regularBullets < startRegularBulletsNumber && regularBullets >= 2)
            {
                starImage1.enabled = true;
                starImage2.enabled = true;
                fadeController.Appear(resultMenu);
                SaveLevelData(2);
            }
            else if (goldenBullets == 0 && regularBullets < 2)
            {
                starImage1.enabled = true;
                SaveLevelData(1);
                fadeController.Appear(resultMenu);
            }
        }
    }

    private void SaveLevelData(int stars)
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        int completeLevels = YandexGame.savesData.completedLevels;


        if(sceneIndex >= completeLevels && sceneIndex != 100)
        {
            YandexGame.savesData.completedLevels = sceneIndex + 1;
        }
        else if(sceneIndex == 100)
        {
            YandexGame.savesData.completedLevels = sceneIndex;
        }

        if (sceneIndex == 1)
        {
            YandexGame.savesData.completedLevelsStars[0] = stars;
        }
        else
        {
            sceneIndex--;
            YandexGame.savesData.completedLevelsStars[sceneIndex] = stars;
        }

        YandexGame.SaveProgress();
    }
}