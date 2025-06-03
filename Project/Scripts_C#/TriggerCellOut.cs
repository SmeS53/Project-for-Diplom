using UnityEngine;

// Класс для триггера снаружи ячейки
public class TriggerCellOut : MonoBehaviour
{
    // Метод, запускающийся при пересечении коллайдеров
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Деактивация дочернего объекта
        // Открытие верхней двери
        gameObject.transform.parent.transform.Find("Structure").Find("Door_U").gameObject.SetActive(false);
    }
}
