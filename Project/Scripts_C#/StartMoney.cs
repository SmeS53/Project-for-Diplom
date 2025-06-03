using UnityEngine;
using UnityEngine.UI;

// ����� ��� ���������� ��������
// ����������� � ��������� ����� � ����� ����
public class StartMoney : MonoBehaviour
{
    // ���������� ���������� ���� ���������� ��������
    Text moneyText;

    // ���������� ������������ � ������������� �������� ��������
    static int moneyMin = 100;
    static int moneyMax = 10000;
    // ���������� ���������� �������� ��������
    int startMoney = moneyMin;

    // ���������� ���������� ��� ����������� �����
    public bool startScene;
    
    // �����, ������������� ����� ������� ����� � ��������� ����� �������
    // ������������ ��� ���������� ���������� ������� � ������
    private void Awake()
    {
        SetUpSingleton();
    }

    // �����, ������������� ��� ��������� ������� �� �����
    void Start()
    {
        // ����� � ������������ ���������� ����������
        moneyText = GameObject.Find("Text Money").GetComponent<Text>();
        // ������������ ������ �������� ����������
        startScene = true;

        UpdateStartMoneyText();
    }

    // ����� ��� ���������� ������� � ������
    private void SetUpSingleton()
    {
        // ���������� ���������� �������� ������ ����
        int numStartMoney = FindObjectsOfType<StartMoney>().Length;
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

    // ����� ��� ���������� ���������� ��������
    public void StartMoneyUp()
    {
        // ���������� ���������� �������� ��������, ����� ������� ��������� ������� ������ �������������
        if (startMoney < moneyMax)
        {
            StartMoneyChange(1);
        }
    }

    // ����� ��� ���������� ���������� ��������
    public void StartMoneyDown()
    {
        // ���������� ���������� �������� ��������, ����� ������� ��������� ������� ������ ������������
        if (startMoney > moneyMin)
        {
            StartMoneyChange(-1);
        }
    }

    // ����� ��� ��������� ���������� ��������
    private void StartMoneyChange(int sign)
    {
        // ��������� ���������� �������� �� 100
        startMoney += sign * 100;
        // ���������� ���������� ����
        UpdateStartMoneyText();
    }

    // ����� ��������� ���������� ��������
    public int GetStartMoney()
    {
        return startMoney;
    }

    // ����� ���������� ���������� ���� ���������� ��������
    private void UpdateStartMoneyText()
    {
        moneyText.text = startMoney.ToString();
    }
} 

//� � ���� �����!!!
//������ �����
//���� ������-����������
//(.)(.)
//������� ����, � ����� ���� ���� ��������� � ������
//����� �����, � �� ������ ����