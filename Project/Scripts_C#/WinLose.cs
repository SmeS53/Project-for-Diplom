using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// ����� ��� ���������� ������� ������
// ����������� � ������ Game � Game over
public class WinLose : MonoBehaviour
{
    // ���������� ���������� ��� �������� ������/���������
    // true - ������
    // false - ��������
    private bool winLose;

    // ���������� ���������� ��� �������� ��������� ������ ������
    private bool textWinInstall;

    // �����, ������������� ����� ������� ����� � ��������� ����� �������
    // ������������ ��� ���������� ���������� ������� � ������
    private void Awake()
    {
        SetUpSingleton();
    }

    // ����� ��� ���������� ������� � ������
    private void SetUpSingleton()
    {
        // ���������� ���������� �������� ������ ����
        int numStartMoney = FindObjectsOfType<Level>().Length;
        // ���� �������� ������ ������, �� ��� ���������
        // � ����� �������� ������ ���� ������ ������� ������
        if (numStartMoney > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // �����, ������������� � ������ �����
    private void Update()
    {
        // ��������, ����� ������� ����� Game over � ���� ��������� ������ �� �����������
        if (SceneManager.GetActiveScene().name.Equals("Game over") && !textWinInstall)
        {
            UpdateTextWinLose();
        }
    }

    // ����� ���������� ������ ������/���������
    private void UpdateTextWinLose()
    {
        // � ����������� �� �������� ��������� �������� ����� ������� �� �����
        if (winLose)
        {
            GameObject.Find("Text Win/Lose").GetComponent<Text>().text = "�� ��������!";
        }
        else
        {
            GameObject.Find("Text Win/Lose").GetComponent<Text>().text = "���, �� ���������";
        }
        // ������������ ���������� ������ ��������
        textWinInstall = true;
    }

    // ����� ��������� �������� ���������� winLose
    public void SetWinLose(bool winLose)
    {
        // ������������ ���������� ������ ��������
        this.winLose = winLose;
        FindObjectOfType<Level>().LoadGameOver();
    }
}
