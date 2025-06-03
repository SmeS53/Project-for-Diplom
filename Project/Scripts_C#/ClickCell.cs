using UnityEngine;

// Класс для действий с ячейкой игрового поля
public class ClickCell : MonoBehaviour
{
    // Переменная для GameProcess
    GameProcess gameProcess;

    // Переменная для родительного объекта текущей ячейки
    GameObject parentCurrentCell;

    // Переменная для ячейки в удержании перед установкой зоны
    HoldCellMove holdCell;

    // Переменная имени родителя ячейки
    string parentName;

    // Переменная размера игрового поля
    int sizeField;

    // Переменная типа зоны
    public int zone;

    // Переменные для определения вершины зоны
    public static int zNSet, zKSet;

    // Метод, запускающийся при появление объекта на сцене
    void Start()
    {
        // Присваивание переменным соответствующие объекты
        gameProcess = FindObjectOfType<GameProcess>();
        sizeField = gameProcess.sizeGameField;
    }

    // Методы установить и получить для n и k координат зоны
    public int Get_zNSet()
    {
        return zNSet;
    }
    public void Set_zNset(int zN)
    {
        zNSet = zN;
    }

    public int Get_zKSet()
    {
        return zKSet;
    }
    public void Set_zKset(int zK)
    {
        zKSet = zK;
    }

    // Метод, запускающийся при нажатии левой кнопкой мыши по текущему объекту
    private void OnMouseDown()
    {
        if (gameProcess.holdCellHasCreate)
        {
            // Запись координат вершины
            zNSet = int.Parse(parentName[parentName.Length - 2] + "");
            zKSet = int.Parse(parentName[parentName.Length - 1] + "");

            // Вызов метода с действием down
            ActionOfStructure("down");

            // Удаление ячейки на удержании
            Destroy(holdCell.gameObject);

            // Изменение переменных объекта gameProcess
            gameProcess.holdCellHasCreate = false;
            gameProcess.zone = 0;
        }

    }

    // Метод, запускающийся при наведении мыши на текущий объект
    private void OnMouseEnter()
    {
        // Вызов метода с действием enter
        ActionOfStructure("enter");
    }

    // Метод, запускающийся при отведении мыши от текущего объекта
    private void OnMouseExit()
    {
        // Вызов метода с действием exit
        ActionOfStructure("exit");
    }

    // Метод действий с объектом ячейки Структурой
    private void ActionOfStructure(string action)
    {
        // Действия доступны только при наличие на сцене ячейки на удержании
        if (gameProcess.holdCellHasCreate)
        {
            // Присваивание переменным соответствующие объекты
            parentCurrentCell = gameObject.transform.parent.gameObject;
            parentName = parentCurrentCell.name;

            // Локальные переменные для записи координат вершины
            int zN = int.Parse(parentName[parentName.Length - 2] + "");
            int zK = int.Parse(parentName[parentName.Length - 1] + "");

            // Поиск на сцене и присваивание ячейки на удержании
            holdCell = FindObjectOfType<HoldCellMove>();

            // Изменение поля для проверки положения ячейки на удержании внутри текущей ячейки
            holdCell.onCell = action.Equals("enter");

            // Изменение положения ячейки на удержании в центр текущей ячейки
            if (action.Equals("enter"))
            {
                holdCell.transform.position = parentCurrentCell.transform.position;
            }

            // Цикл перебора ячеек для наложения или установки зоны
            for (int n = 0; n < sizeField; n++)
            {
                for (int k = 0; k <= n; k++)
                {
                    // Проверка на нахождение ячейки в области зоны
                    if (k >= zK && zN - zK - n + k <= 0)
                    {
                        // Вызов методов объекта gameProcess для конкретных действий
                        if (action.Equals("enter"))
                        {
                            gameProcess.OverlayStructure(n, k);
                        }
                        else if (action.Equals("exit"))
                        {
                            gameProcess.DestroyOverlayStructure(n, k);
                        }
                        else if (action.Equals("down"))
                        {
                            gameProcess.ReinstallStructure(n, k);
                            gameProcess.DestroyOverlayStructure(n, k);
                        }
                    }
                }
            }
        }
    }
}
