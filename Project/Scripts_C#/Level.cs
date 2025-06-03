using UnityEngine;
using UnityEngine.SceneManagement;

// �����, ����������� �������� ����� ������� � ���� � ����� �� ���
public class Level : MonoBehaviour
{
    // ����� �������� ����� ����
    public void LoadGame()
    {
        // ����� � ������������ ������ �������� ��� ���������� �������
        // ���� ������ ����������� � ������
        FindObjectOfType<StartMoney>().startScene = false;
        // �������� ����� ����
        SceneManager.LoadScene("Game");
    }

    // ����� �������� ����� ���������
    public void LoadGameOver()
    {
        // �������� ����� ���������
        SceneManager.LoadScene("Game over");
    }

    // ����� �������� ��������� �����
    public void LoadStartScene()
    {
        // ����������� �������������� ������� ���������� ��������
        Destroy(FindObjectOfType<StartMoney>().gameObject);
        // ����������� �������������� ������� ������� ������
        Destroy(FindObjectOfType<WinLose>().gameObject);
        // �������� ��������� �����
        SceneManager.LoadScene("Start scene");
    }

    // ����� ������ �� ����
    public void QuitGame()
    {
        Application.Quit();
    }
}
