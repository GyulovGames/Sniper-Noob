using UnityEngine;
using UnityEngine.UI;

public class MainMenuLocalization : MonoBehaviour
{
    [SerializeField] private Image menuTitle;

    [SerializeField] private Sprite menuTitleEN;
    [SerializeField] private Sprite menuTitleRU;
    [Space(15)]
    [SerializeField] private Text startGameBtn;
    [SerializeField] private Text levelsBtn;
    [SerializeField] private Text otherGamesBtn;
    [SerializeField] private Text otherGamesMenuTxt;


    private void Start()
    {
        string currentLanguage = Application.systemLanguage.ToString();

        if(currentLanguage == "Russian")
        {
            menuTitle.sprite = menuTitleRU;
            startGameBtn.text = "Начать Игру";
            levelsBtn.text = "Уровни";
            otherGamesBtn.text = "Еще Игры";
            otherGamesMenuTxt.text = "Посмотреть другие иры разработчика??";
        }
        else
        {
            menuTitle.sprite = menuTitleEN;
            startGameBtn.text = "Start Game";
            levelsBtn.text = "Levels";
            otherGamesBtn.text = "Other Games";
            otherGamesMenuTxt.text = "View other games from the developer?";
        }
    }
}