using UnityEngine;

// Класс для триггера в конечной ячейке
public class TriggerFinalCell : MonoBehaviour
{
    // Метод, запускающийся при пересечении коллайдеров
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Создание переменной и присваивание найденной на сцене
        GameProcess gameProcess = FindObjectOfType<GameProcess>();
        // Вызов метода объекта gameProcess
        FindObjectOfType<GameProcess>().DestroyChip(collision.gameObject);

        // Создание переменной и присваивание значения из найденного объекта
        string win = gameObject.transform.parent.transform.Find("Win Score").GetComponent<TextMesh>().text;
        // Вызов метода объекта gameProcess
        gameProcess.AddWin(int.Parse(win));
    }
}
