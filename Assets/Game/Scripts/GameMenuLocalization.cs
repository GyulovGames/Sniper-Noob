using UnityEngine;
using UnityEngine.UI;

public class GameMenuLocalization : MonoBehaviour
{
    [SerializeField] private Text gameLevel;
    [SerializeField] private Text pause;
    [SerializeField] private Text resultMenuTitle;
    [SerializeField] private Text resultLevel;


    private void Start()
    {
        string currentLanguage = Application.systemLanguage.ToString();

        if (currentLanguage == "Russian")
        {
            gameLevel.text = "Уровень:";
            pause.text = "ПАУЗА";
            resultMenuTitle.text = "Великолепно!";
            resultLevel.text = "Уровень:";
        }
        else
        {
            gameLevel.text = "Level:";
            pause.text = "PAUSE";
            resultMenuTitle.text = "Awesome!";
            resultLevel.text = "Level:";
        }
    }
}