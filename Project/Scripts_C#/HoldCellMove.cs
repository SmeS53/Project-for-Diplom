using UnityEngine;

// Класс для ячейки на удержании
public class HoldCellMove : MonoBehaviour
{

    // Логическая переменная, указывающая на нахождение ячейки на удержании внутри текущей ячейки
    public bool onCell = false;

    // Метод, запускающийся в каждом кадре
    void Update()
    {
        // Если ячейка на удержании не внутри текущей ячейки,
        // то позиция первой меняется на позицию мыши на экране
        if (!onCell)
        {
            Vector3 mousePos = FindObjectOfType<Camera>().ScreenToWorldPoint(Input.mousePosition);
            Vector3 newPos= new Vector3(mousePos.x, mousePos.y, -2);
            gameObject.transform.position = newPos;
        }
    }
}
