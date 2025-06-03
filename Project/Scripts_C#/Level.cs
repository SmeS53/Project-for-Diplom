using UnityEngine;
using UnityEngine.SceneManagement;

// Класс, реализующих переходы между сценами в игре и выход из нее
public class Level : MonoBehaviour
{
    // Метод загрузки сцены игры
    public void LoadGame()
    {
        // Поиск и присваивание нового значения для переменной объекта
        // Этот объект сохраняется в сценах
        FindObjectOfType<StartMoney>().startScene = false;
        // Загрузка сцены игры
        SceneManager.LoadScene("Game");
    }

    // Метод загрузки сцены проигрыша
    public void LoadGameOver()
    {
        // Загрузка сцены проигрыша
        SceneManager.LoadScene("Game over");
    }

    // Метод загрузки начальной сцены
    public void LoadStartScene()
    {
        // Уничтожение сохраняющегося объекта начального капитала
        Destroy(FindObjectOfType<StartMoney>().gameObject);
        // Уничтожение сохраняющегося объекта статуса победы
        Destroy(FindObjectOfType<WinLose>().gameObject);
        // Загрузка начальной сцены
        SceneManager.LoadScene("Start scene");
    }

    // Метод выхода из игры
    public void QuitGame()
    {
        Application.Quit();
    }
}
