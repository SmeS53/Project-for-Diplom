using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts;

// Класс для основного объекта сцены, в котором реализованы многие методы для взаимодействия объектов
public class GameProcess : MonoBehaviour
{
    // Поля для префабов объектов
    [Header("Prefabs")]
    [SerializeField] GameObject gridPrefab;
    [SerializeField] GameObject chipPrefab;
    [SerializeField] PhysicsMaterial2D material;
    [SerializeField] GameObject[] holdCellPrefabs = new GameObject[2];
    [SerializeField] GameObject[] structurePrefabs = new GameObject[3];

    // Опция бесконечной игры для тестирования приложения
    // Не используется в самом приложении
    [Header("Game Options")]
    [SerializeField] bool infinityGame = false;

    // Min и Max размеры игрового поля
    static int sizeMin = 5;
    static int sizeMax = 10;

    // Рамер игрового поля
    // равен 5 по умолчанию
    public int sizeGameField = sizeMin;

    // Переменная для текстового поля размера
    public Text textSize;

    // Логическое поле для проверки существования фишки
    public bool chipHasCreate = false;

    // Логическое поле для проверки существования ячейки в удержании
    public bool holdCellHasCreate = false;

    // Переменные игрового поля, поля зоны и индексы для них
    private GameField gameField;
    private ZoneField zoneField;
    private int n, k;

    // Плата за бросок
    private int pay;

    // Переменная для текстового поля платы
    private Text textPay;

    // Капитал
    private int money;

    // Переменная для текстового поля капитала
    private Text textMoney;

    // Поле типа зоны
    public int zone;

    // Поле названий зон в удержании
    string[] holdCellNames = { "Green_Cell", "Red_Cell" };

    // Переменная для ячейки в координатах n, k
    GameObject cell_NK;

    // Переменная для определения типа зоны в ячейке n, k
    ClickCell cell_NKZone;

    // Метод, запускающийся при появление объекта на сцене
    void Start()
    {
        // Создание первой сетки при начале игры
        GameObject grid = Instantiate(
            gridPrefab,
            gridPrefab.transform.position,
            Quaternion.identity
            ) as GameObject;
        
        // Присваивание имени объекту
        grid.name = "Grid";

        // Поиск объекта на сцене по имени и присваивание в соответствующую переменную
        textSize = GameObject.Find("Text Size").GetComponent<Text>();
        textMoney = GameObject.Find("Money").transform.Find("Text").GetComponent<Text>();
        textPay = GameObject.Find("Pay").transform.Find("Text").GetComponent<Text>();
        
        // Вызов методов обновления полей
        UpdateTextSize();
        UpdateGameFieldSize();
        UpdateZoneFieldSize();
        UpdateMoney();
        UpdateGrid();
    }

    // Метод, запускающийся в каждом кадре
    void Update()
    {
        // Вызов методов класса
        UpdatePay();
        CheckMoney();
    }

    // Метод, реализующий бросок фишки
    public void Cast()
    {
        // Создание фишки как объекта на сцене с соблюдением условий:
        // фишка не была создана и ячейка на удержании не была создана
        if(!chipHasCreate && !holdCellHasCreate)
        {
            GameObject chip = Instantiate(
                chipPrefab,
                chipPrefab.transform.position,
                Quaternion.identity
                ) as GameObject;

            // Присваивание имени объекту
            chip.name = "Chip";
            // Присваивание значения для проверки существования фишки
            chipHasCreate = true;

            // Вызов метода с вычетом платы
            UpdateMoney(money - pay);
        }
    }

    // Метод для уничтожения фишки с использованием корутин
    public void DestroyChip(GameObject chip)
    {
        // Обнуление физического материала фишки
        chip.GetComponent<Rigidbody2D>().sharedMaterial = null;
        // Вызов метода через корутин
        StartCoroutine(ResetChip(chip));
    }

    // Метод для сброса позиции или уничтожения фишки
    // В зависимости от настройки режима бесконечной игры
    private IEnumerator ResetChip(GameObject chip)
    {
        // Ожидание двух секунд времени
        yield return new WaitForSeconds(2f);

        if (infinityGame)
        {
            // Сброс позиции, скорости и физического материала до начальных значений
            chip.transform.position = chipPrefab.transform.position;
            chip.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            chip.GetComponent<Rigidbody2D>().sharedMaterial = material;
        }
        else
        {
            // Уничтожение фишки
            Destroy(chip);
            // Присваивание значения для проверки существования фишки
            chipHasCreate = false;
        }
    }

    // Метод для увеличения размера игрового поля
    public void SizeUp()
    {
        // Увеличение размера доступно, когда текущий размер меньше максимального
        if (sizeGameField < sizeMax)
        {
            SizeChange(1);
        }
    }

    // Метод для уменьшения размера игрового поля
    public void SizeDown()
    {
        // Уменьшение размера доступно, когда текущий размер больше минимального
        if (sizeGameField > sizeMin)
        {
            SizeChange(-1);
        }
    }

    // Метод для изменения размера игрового поля
    private void SizeChange(int sign)
    {
        // Изменение размера доступно, когда фишки и ячейки на удержании не существует
        if (!chipHasCreate && !holdCellHasCreate)
        {
            // Изменение размера поля на единицу
            sizeGameField += sign;

            // Вызов методов обновления полей
            UpdateTextSize();
            UpdateGameFieldSize();
            UpdateZoneFieldSize();
            UpdateGrid();
        }
    }

    // Метод обновления текста размера игрового поля
    private void UpdateTextSize()
    {
        textSize.text = sizeGameField.ToString();
    }

    // Метод обновления игрового поля
    private void UpdateGameFieldSize()
    {
        gameField = new GameField(sizeGameField);
    }
    // Метод обновления поля зоны
    private void UpdateZoneFieldSize()
    {
        zoneField = new ZoneField(gameField);
    }
    // Метод обновления поля значения монет и его текста
    private void UpdateMoney()
    {
        // Поиск объекта, присваивание переменной и вызов перегрузки метода
        int startMoney = FindObjectOfType<StartMoney>().GetStartMoney();
        UpdateMoney(startMoney);
    }
    // Перегрузка метода обновления значения монет
    private void UpdateMoney(int money)
    {
        // Присваивание переменной класса нового значения
        this.money = money;
        // Изменение текста
        textMoney.text = this.money.ToString();
    }

    // Метод обновления поля платы и его текста
    private void UpdatePay()
    {
        // Присваивание переменной класса нового значения
        pay = zoneField.GetPay();
        // Изменение текста
        textPay.text = pay.ToString();
    }

    // Метод обновления выигрышей
    private void UpdateWinScore()
    {
        // Нахождение всех нужных объектов на сцене и изменение переменной для проверки
        WinScore[] winScores = FindObjectsOfType<WinScore>();
        foreach (WinScore winScore in winScores)
        {
            winScore.winScoreInstall = false;
        }
    }

    // Метод проверки значения капитала
    // При отрицательном значении игрок проигрывает
    // При значении больше или равному 10 000 игрок выигрывает
    private void CheckMoney()
    {
        if (money < 0)
        {
            FindObjectOfType<WinLose>().SetWinLose(false);
        }

        if (money >= 10000)
        {
            FindObjectOfType<WinLose>().SetWinLose(true);
        }
    }

    // Метод обновления сетки
    private void UpdateGrid()
    {
        // Нахождение объекта Grid на сцене и вызов его метода
        FindObjectOfType<Grid>().SetSizeGrid(sizeGameField);
    }

    // Метод выброса фишки из ячейки с использованием корутин
    public void MoveChip(GameObject triggerCell)
    {
        // Случайная величина
        double rand = new System.Random().NextDouble();

        // Имя родительского объекта ячейки, в которой находится фишка
        string parentName = triggerCell.transform.parent.name;

        // Активация дочернего объекта в текущей ячейке для показа знака вопроса
        Transform cell_Structure = triggerCell.transform.parent.transform.Find("Structure");
        cell_Structure.Find("Cell_BG").Find("Text?").gameObject.SetActive(true);

        // Присваивание координат текущей ячейки
        n = int.Parse(parentName[parentName.Length - 2] + "");
        k = int.Parse(parentName[parentName.Length - 1] + "");

        // Получение вероятности движения фишки в ячейке
        double valueNK = gameField.getToIndex(n, k);

        // Вызов метода выброса фишки в зависимости от вероятности движения в ячейке
        if (rand < valueNK)
        {
            // Выброс фишки влево и открытие/закрытие левой двери
            StartCoroutine(MoveChipOpenDoors(cell_Structure, -1, "Door_L"));
        }
        else
        {
            // Выброс фишки вправо и открытие/закрытие правой двери
            StartCoroutine(MoveChipOpenDoors(cell_Structure, 1, "Door_R"));
        }
    }

    // Метод выброса фишки и открывание/закрывание двери
    private IEnumerator MoveChipOpenDoors(Transform cell_Structure, int dir, string nameDoor)
    {
        // Деактивация объекта с ожиданием в секунду
        Transform cell_BG = cell_Structure.Find("Cell_BG");
        yield return new WaitForSeconds(1f);
        cell_BG.Find("Text?").gameObject.SetActive(false);

        // Массив строк для указания направления выброса
        string[] arrow = { "<", ">" };
        // Присваивание текста направления в зависимости от направления выброса
        cell_BG.Find("Text<>").GetComponent<TextMesh>().text = arrow[(1 + dir) / 2];

        // Открытие нужной двери
        cell_Structure.Find(nameDoor).gameObject.SetActive(false);
        // Вызов метода фишки в нужном направлении
        FindObjectOfType<Chip>().Move(dir);
        // Ожидание 0,2 секунды
        yield return new WaitForSeconds(0.2f);
        // Закрытие двери
        cell_Structure.Find(nameDoor).gameObject.SetActive(true);

        // Удаление текста у объекта
        cell_BG.Find("Text<>").GetComponent<TextMesh>().text = "";
    }

    // Метод получения значения выигрыша
    public string GetWin(int index)
    {
        return gameField._winList[index].ToString();
    }

    // Метод прибавление значения выигрыша к капиталу
    public void AddWin(int win)
    {
        UpdateMoney(money + win);
    }

    // Методы удержания объекта возле мыши
    // Для зеленой зоны
    public void HoldGreenCell()
    {
        CheckHoldCell(-1);
    }
    // Для красной зоны
    public void HoldRedCell()
    {
        CheckHoldCell(1);
    }

    // Метод изменения ячейки на удержании в зависимости от текущей
    // Если ничего нет, то создаем нужную
    // Если на удержании есть ячейка и клик на ту же зону сделан еще раз, то текущая удалится
    // Если на удержании есть ячейка и клик на другую зону, то ячейка заменится
    private void CheckHoldCell(int check)
    {
        if (!chipHasCreate)
        {
            if (zone == check)
            {
                GameObject cell = GameObject.Find(holdCellNames[(1 - check) / 2]);
                Destroy(cell);
                HoldCell(-check);
            }
            else if (zone == -check)
            {
                GameObject cell = GameObject.Find(holdCellNames[(1 + check) / 2]);
                Destroy(cell);
                zone = 0;
                holdCellHasCreate = false;
            }
            else if (!holdCellHasCreate)
            {
                HoldCell(-check);
            }
        }
    }

    // Метод создания ячейки на удержании
    public void HoldCell(int check)
    {
        GameObject holdCell = Instantiate(
                holdCellPrefabs[(1 - check) / 2],
                Input.mousePosition,
                Quaternion.identity
                ) as GameObject;
        holdCell.name = holdCellNames[(1 - check) / 2];
        
        // Присваивание переменным нового значения
        zone = check;
        holdCellHasCreate = true;
    }

    // Метод создания структур ячеек для наложения
    public void OverlayStructure(int n, int k)
    {
        cell_NK = GameObject.Find("Cell_" + n + k);
        NewStructureCreate("Overlay Structure");
    }

    // Метод уничтожения структур ячеек для наложения
    public void DestroyOverlayStructure(int n, int k)
    {
        cell_NK = GameObject.Find("Cell_" + n + k);
        Destroy(cell_NK.transform.Find("Overlay Structure").gameObject);
    }

    // Метод переустановки структур ячеек на структуры зон или начальную при пересечении зон
    public void ReinstallStructure(int n, int k)
    {
        // Нахождение объектов на сцене и присваивание их к переменным
        cell_NK = GameObject.Find("Cell_" + n + k);
        cell_NKZone = cell_NK.transform.Find("Game Field").GetComponent<ClickCell>();
        // Уничтожение дочернего объекта ячейки
        Destroy(cell_NK.transform.Find("Structure").gameObject);

        // Установка зоны в ее вершине
        if (n == cell_NKZone.Get_zNSet() && k == cell_NKZone.Get_zKSet())
        {
            // Вызов метода для установки зоны
            zoneField.setZone(n, k, zone);

            // Обновление выигрышей
            UpdateWinScore();
        }

        // Если в ячейке противоположная зона, то делаем сброс ячейки
        // Иначе меняем дочерний объект и устанавливаем текущее значение зоны
        if (cell_NKZone.zone == -zone)
        {
            ResetCell_NK();
        }
        else
        {
            NewStructureCreate("Structure");
            cell_NKZone.zone = zone;
        }
    }

    // Метод сброса структур ячеек
    public void ResetStructure()
    {
        // Будет выполнен, если фишки не существует
        if (!chipHasCreate)
        {
            for (int n = 0; n < sizeGameField; n++)
            {
                for (int k = 0; k <= n; k++)
                {
                    // Поиск объектов и их присваивание переменным
                    cell_NK = GameObject.Find("Cell_" + n + k);
                    cell_NKZone = cell_NK.transform.Find("Game Field").GetComponent<ClickCell>();
                    // Уничтожение дочернего объекта
                    Destroy(cell_NK.transform.Find("Structure").gameObject);

                    // Сброс ячейки n, k
                    ResetCell_NK();
                }
            }
            // Вызов методов обновления полей
            UpdateGameFieldSize();
            UpdateZoneFieldSize();
            UpdateWinScore();
        }
    }

    // Метод создания новой структуры ячейки
    private void NewStructureCreate(string action)
    {
        GameObject newStructure = Instantiate(
            structurePrefabs[(3 - zone) / 2],
            cell_NK.transform.position,
            Quaternion.identity
            ) as GameObject;
        // Установление родительского объекта
        newStructure.transform.SetParent(cell_NK.transform);
        // Присваивание имени объекта
        newStructure.name = action;

        // Если объект для наложения, то меняем позицию
        if (action.Equals("Overlay Structure"))
        {
            newStructure.transform.Translate(0, 0, -0.5f);
        }
    }

    // Метод сброса структуры ячейки n, k
    private void ResetCell_NK()
    {
        GameObject newStructure = Instantiate(
            structurePrefabs[0],
            cell_NK.transform.position,
            Quaternion.identity
            ) as GameObject;
        // Установление родительского объекта
        newStructure.transform.SetParent(cell_NK.transform);
        // Присваивание имени объекта
        newStructure.name = "Structure";

        // Обнуление типа зоны и координат вершины
        cell_NKZone.zone = 0;
        cell_NKZone.Set_zNset(0);
        cell_NKZone.Set_zKset(0);
    }
}
