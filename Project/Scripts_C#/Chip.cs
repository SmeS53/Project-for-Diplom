using UnityEngine;

// Класс, реализующий выброс фишки из ячейки
public class Chip : MonoBehaviour
{
    // Метод, реализующий выброс вправо или влево
    // переменная dir - направление движения
    public void Move(int dir)
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(dir * 5, 0);
    }
}
