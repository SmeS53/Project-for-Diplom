using UnityEngine;

// Класс для выигрышей в конечных ячейках
public class WinScore : MonoBehaviour
{
    // Переменная номера родителя
    int parentNum;
    // Переменная имени родителя
    string parentName;
    // Логическая переменная для проверки установки выигрышей
    public bool winScoreInstall;

    // Метод, запускающийся при появление объекта на сцене
    void Start()
    {
        // Присваивание новых значений переменным
        parentName = gameObject.transform.parent.name;
        winScoreInstall = false;
    }

    // Метод, запускающийся в каждом кадре
    void Update()
    {
        // Доступно, если выигрыши не установлены
        if (!winScoreInstall)
        {
            // Присваивание номера родителя
            // Если номер двузначный присваиваем другим способом
            if (parentName[parentName.Length - 2].Equals('_'))
            {
                parentNum = int.Parse(parentName[parentName.Length - 1] + "");
            }
            else
            {
                parentNum = int.Parse(parentName[parentName.Length - 2] + "" + parentName[parentName.Length - 1]);
            }

            // Поиск объекта на сцене
            GameProcess gameProcess = FindObjectOfType<GameProcess>();
            // Присваивание текстовому полю значения выигрыша через вызов метода в gamrProcess
            gameObject.GetComponent<TextMesh>().text = gameProcess.GetWin(parentNum);
            // Изменение значения переменной
            winScoreInstall = true;
        }
    }
}
