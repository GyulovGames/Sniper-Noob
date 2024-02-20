using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class MenuCanvas : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private FadeController fadeController;
    [Space(10)]
    [SerializeField] private Sprite soundsOnSprite;
    [SerializeField] private Sprite soundsOffSprite;
    [SerializeField] private Sprite musicOnSprite;
    [SerializeField] private Sprite musicOffSprite;
    [SerializeField] private Sprite starSprite;
    [Space(20)]
    [SerializeField] private Image soundsButtonImage;
    [SerializeField] private Image musicButtonImage;
    [Space(20)]
    [SerializeField] private CanvasGroup mainMenu;
    [SerializeField] private CanvasGroup allGamesMenu;
    [SerializeField] private CanvasGroup levelsMenu;
    [SerializeField] private CanvasGroup smoothTransition;
    [Space(10)]
    [SerializeField] private Button[] levelsButtons;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            YandexGame.ResetSaveProgress();
            YandexGame.SaveProgress();
        }
        else if(Input.GetKeyDown(KeyCode.U)) 
        {
            YandexGame.savesData.completedLevels = 129;
            YandexGame.SaveProgress();
        }
    }

    private void Start()
    {
        LoadCompletedLevels(YandexGame.savesData.completedLevels);
        LoadSoundsSettings(YandexGame.savesData.sounds);
        LoadMusicSettings(YandexGame.savesData.music);
        
        fadeController.Disappear(smoothTransition);

    }


    private void LoadCompletedLevels(int completedLevels)
    {
        for (int i = 0; i < completedLevels; i++)
        {
            levelsButtons[i].interactable = true;
            levelsButtons[i].transition = Selectable.Transition.Animation;

            int starsNumber = YandexGame.savesData.completedLevelsStars[i];
            Transform butTransform = levelsButtons[i].transform;

            switch (starsNumber)
            {
                case 1:
                    butTransform.GetChild(1).GetComponent<Image>().sprite = starSprite;
                    break;
                case 2:
                    butTransform.GetChild(1).GetComponent<Image>().sprite = starSprite;
                    butTransform.GetChild(2).GetComponent<Image>().sprite = starSprite;
                    break;
                case 3:
                    butTransform.GetChild(1).GetComponent<Image>().sprite = starSprite;
                    butTransform.GetChild(2).GetComponent<Image>().sprite = starSprite;
                    butTransform.GetChild(3).GetComponent<Image>().sprite = starSprite;
                    break;
            }
        }       
    }

    private void LoadSoundsSettings(bool sounds)
    {
        if (sounds == true)
        {
            audioSource.volume = 1f;
            soundsButtonImage.sprite = soundsOnSprite;         
        }
        else if(sounds == false)
        {
            audioSource.volume = 0f;
            soundsButtonImage.sprite = soundsOffSprite;
        }
    }

    private void LoadMusicSettings(bool music)
    {
        GameObject musicPlayer = GameObject.FindGameObjectWithTag("MusicPlayer");
        AudioSource aus = musicPlayer.GetComponent<AudioSource>();

        if(music == true)
        {
            musicButtonImage.sprite = musicOnSprite;
        }
        else if(music == false)
        {
            aus.Stop();
            musicButtonImage.sprite = musicOffSprite;
        }
    }

    

    // Main menu buttons functions
    public void BtnStartGame()
    {
        int completedLevels = YandexGame.savesData.completedLevels;

        if(completedLevels != 130)
        {
            audioSource.Play();
            int levelToLoad = YandexGame.savesData.completedLevels;
            StartCoroutine(Delay(levelToLoad));
            fadeController.Appear(smoothTransition);
        }
        else if (completedLevels == 130)
        {
            audioSource.Play();
            int levelToLoad = Random.Range(1, 129);
            StartCoroutine(Delay(levelToLoad));
            fadeController.Appear(smoothTransition);
        }
    }

    public void BtnOpenLevels()
    {
        audioSource.Play();
        fadeController.Appear(levelsMenu);
        fadeController.Disappear(mainMenu);
    }

    public void BtnOpenAllGames()
    {
        audioSource.Play();
        fadeController.Appear(allGamesMenu);
        fadeController.Disappear(mainMenu);
    }

    public void BtnSounds()
    {
        bool sounds = YandexGame.savesData.sounds;

        if (sounds == true)
        {
            audioSource.volume = 0f;
            soundsButtonImage.sprite = soundsOffSprite;
            YandexGame.savesData.sounds = false;
            YandexGame.SaveProgress();
        }
        else if(sounds == false)
        {
            audioSource.Play();
            audioSource.volume = 1f;
            soundsButtonImage.sprite = soundsOnSprite;
            YandexGame.savesData.sounds = true;
            YandexGame.SaveProgress();
        }
    }

    public void BtnMusic()
    {
        bool music = YandexGame.savesData.music;
        GameObject musicPlayer = GameObject.FindGameObjectWithTag("MusicPlayer");
        AudioSource aus = musicPlayer.GetComponent<AudioSource>();

        if (music == true)
        {
            aus.Pause();
            musicButtonImage.sprite = musicOffSprite;
            YandexGame.savesData.music = false;
            YandexGame.SaveProgress();
        }
        else if (music == false)
        {
            aus.Play();
            musicButtonImage.sprite=musicOnSprite;
            YandexGame.savesData.music = true;
            YandexGame.SaveProgress();
        }
    }

    public void BtnCloseLevels()
    {
        audioSource.Play();
        fadeController.Appear(mainMenu);
        fadeController.Disappear(levelsMenu);
    }

    public void BtnCloseAllGames()
    {
        audioSource.Play();
        fadeController.Disappear(allGamesMenu);
        fadeController.Appear(mainMenu);
    }

    public void BtnLoadLevel(int levelIndex)
    {
        audioSource.Play();
        StartCoroutine(Delay(levelIndex));
        fadeController.Appear(smoothTransition);
    }


    private IEnumerator Delay(int levelIndex)
    {
        yield return new WaitForSeconds(1f);
        StopCoroutine(Delay(levelIndex));
        SceneManager.LoadScene(levelIndex);
    }
}