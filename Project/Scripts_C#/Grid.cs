using UnityEngine;

// Класс сетки для расстановки объектов на сцене
public class Grid : MonoBehaviour
{
    // Префабы объектов
    [Header("Prefabs")]
    [SerializeField] GameObject cellPrefab;
    [SerializeField] GameObject finalCellPrefab;
    [SerializeField] GameObject[] barrierPrefabs = new GameObject[3];

    // Переменная размера сетки
    private int sizeGrid;
    
    // Массивы для новых объектов
    private GameObject[] cells;
    private GameObject[] finalCells;
    private GameObject[] barriers;

    // Логическое поле для проверки существования сетки
    private bool gridHasCreate = true;

    // Метод, запускающийся в каждом кадре
    void Update()
    {
        // Если сетка не создана, то создать
        if (!gridHasCreate)
        {
            GridCreate();
        }
    }

    // Метод установки сетки по размеру
    public void SetSizeGrid(int sizeGrid)
    {
        // Присваивание переменным новых значений
        this.sizeGrid = sizeGrid;
        gridHasCreate = false;
    }

    // Метод создания сетки
    private void GridCreate()
    {
        // Очистка созданной ранее сетки
        ClearGrid();

        // Создание массивов объектов на основе размера сетки
        cells = new GameObject[(sizeGrid + 1) * sizeGrid / 2];
        finalCells = new GameObject[sizeGrid + 1];
        barriers = new GameObject[2 * sizeGrid];
        gridHasCreate = true;

        // Заполнение объектами типа Ячейка
        // И присваивание соответствующего имени
        for (int n = 0; n < sizeGrid; n++)
        {
            for (int k = 0; k <= n; k++)
            {
                int num = k + n * (n + 1) / 2;
                cells[num] = Instantiate(
                    cellPrefab,
                    new Vector3((2 * k - n) * 24 / 16f, 12 * (sizeGrid - 2 * n) / 16f, 0),
                    Quaternion.identity
                    ) as GameObject;
                cells[num].name = "Cell_" + n + k;
            }
        }

        // Заполнение объектами типа Конечная Ячейка
        // И присваивание соответствующего имени
        for (int k = 0; k <= sizeGrid; k++)
        {
            finalCells[k] = Instantiate(
                finalCellPrefab,
                new Vector3((2 * k - sizeGrid) * 24 / 16f, (-12 * sizeGrid - 0.5f) / 16f, 0),
                Quaternion.identity
                ) as GameObject;
            finalCells[k].name = "Final_Cell_" + k;
        }

        // Заполнение объектов типа Барьер
        // И присваивание соответствующих имен
        for (int n = 0; n < barriers.Length; n++)
        {
            // Нижние барьеры устанавливаются в два первых поля массива
            // Далее, если размер сетки 2, то будут созданы еще и верхние барьеры
            // При размере сетки больше 2, создаются еще и средние барьеры с нумерацией снизу-вверх
            if (n <= 1)
            {
                barriers[n] = Instantiate(
                barrierPrefabs[2],
                new Vector3((24 * sizeGrid - 1) / 16f, 12 * (2 - sizeGrid) / 16f, 0),
                Quaternion.identity
                ) as GameObject;
                barriers[n].name = "Barrier_D";
            }
            else if (sizeGrid > 1 && n >= barriers.Length - 2)
            {
                barriers[n] = Instantiate(
                barrierPrefabs[0],
                new Vector3(25.5f / 16f, (12 * sizeGrid + 1.5f) / 16f, 0),
                Quaternion.identity
                ) as GameObject;
                barriers[n].name = "Barrier_U";
            }
            else
            {
                int k = n / 2 - 1;
                barriers[n] = Instantiate(
                barrierPrefabs[1],
                new Vector3(((2 * k + 4) * 12 + 2.5f) / 16f, ((sizeGrid - 2 * k) * 12 - 21) / 16f, 0),
                Quaternion.identity
                ) as GameObject;
                barriers[n].name = "Barrier_M" + (barriers.Length - 2 - n + 2 * (n % 2));
            }
        }

        // Изменение позиции и направления для всех Барьеров слева
        for (int n = 0; n < sizeGrid; n++)
        {
            Vector3 oldBarrierPos = barriers[2 * n].transform.position;
            Vector3 newBarrierPos = Vector3.Scale(oldBarrierPos, new Vector3(-1, 1, 1));

            barriers[2 * n].transform.position = newBarrierPos;
            barriers[2 * n].transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    // Метод очистки сетки
    private void ClearGrid()
    {
        if (cells != null)
        {
            DestroyArrayGO(cells);
            DestroyArrayGO(finalCells);
            DestroyArrayGO(barriers);
        }
    }

    // Метод уничтожения созданных массивов объектов
    private void DestroyArrayGO(GameObject[] gameObjects)
    {
        foreach (GameObject g in gameObjects)
        {
            Destroy(g);
        }
    }
}
