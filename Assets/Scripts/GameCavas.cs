using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    [SerializeField] private CanvasGroup mainMenu;
    [SerializeField] private CanvasGroup allGamesMenu;
    [SerializeField] private CanvasGroup levelsMenu;
    [SerializeField] private CanvasGroup resultMenu;
    [SerializeField] private CanvasGroup pauseMenu;
    [SerializeField] private CanvasGroup gameMenu;
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
        RefillBulletsMag();
        CountZombiesNumber();
        UpdateLvIndexIndicator();

        Noobik.ShootEvent.AddListener(Shoot);
        Zombie.ZombieHitEvent.AddListener(Hit);
    }



    public void StartGameBtn()
    {
        audioSource.Play();
    }

    public void LevelsBtn()
    {
        audioSource.Play();
        fadeController.Appear(levelsMenu);
      //  fadeController.Desappear(mainMenu);
    }

    public void CloseLevelsBtn()
    {
        audioSource.Play();
      //  fadeController.Appear(mainMenu);
        fadeController.Desappear(levelsMenu);
    }
    public void LoadLevelBtn(int levelIndex)
    {
        audioSource.Play();

        SceneManager.LoadScene(levelIndex);
    }


    public void AllGamesBtn()
    {
        audioSource.Play();
        fadeController.Desappear(mainMenu);
        fadeController.Appear(allGamesMenu);
    }

    public void AllGamesYesBtn()
    {
        audioSource.Play();
    }

    public void AllGamesNoBtn()
    {
        audioSource.Play();
        fadeController.Desappear(allGamesMenu);
        fadeController.Appear(mainMenu);
    }


    public void PauseBtn()
    {
        audioSource.Play();
        pauseEvent.Invoke();
    }

    public void ResumeBtn()
    {
        audioSource.Play();

        pauseEvent.Invoke();
    }

    public void RestartBtn()
    {
        audioSource.Play();

        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex);
    }

    public void HomeBut()
    {
        audioSource.Play();
        SceneManager.LoadScene(0);

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



    private void UpdateLvIndexIndicator()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        levelIndexIndicator.text = "Level:" + sceneIndex;
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
        if(zombiesNumber <= 0 || bulletsNumber <= 0)
        {
          // Invoke("LoadResultMenu", 1.6f);
        }
        else if(bulletsNumber <= 0 && zombiesNumber <= 0)
        {
         //   Invoke("LoadResultMenu", 1.6f);
        }
    }



    private void LoadResultMenu()
    {
        if(bulletsNumber <= 0 && zombiesNumber > 0)
        {
           // canvasAnimator.SetTrigger("GameResult");
        }
        else if (startRegularBulletsNumber == 0)
        {
            if (goldenBullets >= 0)
            {
                starImage1.enabled = true;
                starImage2.enabled = true;
                starImage3.enabled = true;

              //  canvasAnimator.SetTrigger("GameResult");
            }
        }
        else if (startRegularBulletsNumber > 0)
        {
            if (goldenBullets >= 0 && regularBullets == startRegularBulletsNumber)
            {
                starImage1.enabled = true;
                starImage2.enabled = true;
                starImage3.enabled = true;

               // canvasAnimator.SetTrigger("GameResult");
            }
            else if (goldenBullets == 0 && regularBullets < startRegularBulletsNumber && regularBullets >= 2)
            {
                starImage1.enabled = true;
                starImage2.enabled = true;

              //  canvasAnimator.SetTrigger("GameResult");
            }
            else if (goldenBullets == 0 && regularBullets < 2)
            {
                starImage1.enabled = true;

              //  canvasAnimator.SetTrigger("GameResult");
            }
        }
    }
}