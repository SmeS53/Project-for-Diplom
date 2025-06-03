using UnityEngine;
using UnityEngine.UI;

// Класс для начального капитала
// Сохраняется в начальной сцене и сцене игры
public class StartMoney : MonoBehaviour
{
    // Переменная текстового поля начального капитала
    Text moneyText;

    // Переменные минимального и максимального значения капитала
    static int moneyMin = 100;
    static int moneyMax = 10000;
    // Переменная начального значения капитала
    int startMoney = moneyMin;

    // Логическая переменная для определения сцены
    public bool startScene;
    
    // Метод, запускающийся перед методом Старт в жизненном цикле проекта
    // Используется для реализации сохранения объекта в сценах
    private void Awake()
    {
        SetUpSingleton();
    }

    // Метод, запускающийся при появление объекта на сцене
    void Start()
    {
        // Поиск и присваивание текстового компонента
        moneyText = GameObject.Find("Text Money").GetComponent<Text>();
        // Присваивание нового значение переменной
        startScene = true;

        UpdateStartMoneyText();
    }

    // Метод для сохранения объекта в сценах
    private void SetUpSingleton()
    {
        // Переменная количества объектов одного типа
        int numStartMoney = FindObjectsOfType<StartMoney>().Length;
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

    // Метод для увеличения начального капитала
    public void StartMoneyUp()
    {
        // Увеличение начального капитала доступно, когда текущий начальный капитал меньше максимального
        if (startMoney < moneyMax)
        {
            StartMoneyChange(1);
        }
    }

    // Метод для уменьшения начального капитала
    public void StartMoneyDown()
    {
        // Уменьшение начального капитала доступно, когда текущий начальный капитал больше минимального
        if (startMoney > moneyMin)
        {
            StartMoneyChange(-1);
        }
    }

    // Метод для изменения начального капитала
    private void StartMoneyChange(int sign)
    {
        // Изменение начального капитала на 100
        startMoney += sign * 100;
        // Обновление текстового поля
        UpdateStartMoneyText();
    }

    // Метод получения начального капитала
    public int GetStartMoney()
    {
        return startMoney;
    }

    // Метод обновления текстового поля начального капитала
    private void UpdateStartMoneyText()
    {
        moneyText.text = startMoney.ToString();
    }
} 
