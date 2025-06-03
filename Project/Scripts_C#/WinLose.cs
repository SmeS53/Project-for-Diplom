using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Класс для сохранения статуса победы
// Сохраняется в сценах Game и Game over
public class WinLose : MonoBehaviour
{
    // Логическая переменная для проверки победы/проигрыша
    // true - победа
    // false - проигрыш
    private bool winLose;

    // Логическая переменная для проверки установки текста победы
    private bool textWinInstall;

    // Метод, запускающийся перед методом Старт в жизненном цикле проекта
    // Используется для реализации сохранения объекта в сценах
    private void Awake()
    {
        SetUpSingleton();
    }

    // Метод для сохранения объекта в сценах
    private void SetUpSingleton()
    {
        // Переменная количества объектов одного типа
        int numStartMoney = FindObjectsOfType<Level>().Length;
        // Если объектов больше одного, то они удаляются
        // В итоге остается только один объект данного класса
        if (numStartMoney > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Метод, запускающийся в каждом кадре
    private void Update()
    {
        // Доступно, когда текущая сцена Game over и поле победного текста не установлено
        if (SceneManager.GetActiveScene().name.Equals("Game over") && !textWinInstall)
        {
            UpdateTextWinLose();
        }
    }

    // Метод обновления текста победы/проигрыша
    private void UpdateTextWinLose()
    {
        // В зависимости от входного параметра меняется текст объекта на сцене
        if (winLose)
        {
            GameObject.Find("Text Win/Lose").GetComponent<Text>().text = "вы выиграли!";
        }
        else
        {
            GameObject.Find("Text Win/Lose").GetComponent<Text>().text = "увы, вы проиграли";
        }
        // Присваивание переменной нового значения
        textWinInstall = true;
    }

    // Метод установки значения переменной winLose
    public void SetWinLose(bool winLose)
    {
        // Присваивание переменной нового значения
        this.winLose = winLose;
        FindObjectOfType<Level>().LoadGameOver();
    }
}
