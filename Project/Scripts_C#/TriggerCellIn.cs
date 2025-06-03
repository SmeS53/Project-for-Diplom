using UnityEngine;

// Класс для триггера внутри ячейки
public class TriggerCellIn : MonoBehaviour
{
    // Метод, запускающийся при пересечении коллайдеров
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Активация дочернего объекта
        // Закрытие верхней двери
        gameObject.transform.parent.transform.Find("Structure").Find("Door_U").gameObject.SetActive(true);
        
        // Вызов метода объекта GameProcess
        FindObjectOfType<GameProcess>().MoveChip(gameObject);
    }
}
