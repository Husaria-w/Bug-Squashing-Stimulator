using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.IO.Ports;

public class Checker : MonoBehaviour
{
    public string portName = "COM3"; // ���������ʵ���������
    public int baudRate = 9600;
    public float levelThreshold = 5f; // ������б�ĽǶȷ�Χ ��5��
    public float requiredLevelTime = 1f; // ���뱣��ˮƽ��ʱ��
    public Canvas pauseCanvas; // ָ�������ͣ����
    private SerialPort serialPort;
    private float levelTimer = 0f;
    private bool isLevel = false;

    void Start()
    {
        serialPort = new SerialPort(portName, baudRate);
        try
        {
            serialPort.Open();
            serialPort.ReadTimeout = 100;
        }
        catch
        {
            Debug.LogError("�޷��򿪴��ڣ��������ӣ�");
        }

        pauseCanvas.enabled = true;
        Time.timeScale = 0f; // ��ͣ��Ϸ
    }

    void Update()
    {
        float angle = ReadAngleFromArduino();

        if (Mathf.Abs(angle) <= levelThreshold)
        {
            levelTimer += Time.unscaledDeltaTime;
            if (levelTimer >= requiredLevelTime && !isLevel)
            {
                ResumeGame();
            }
        }
        else
        {
            levelTimer = 0f;
            if (isLevel)
            {
                PauseGame();
            }
        }
    }

    float ReadAngleFromArduino()
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            try
            {
                string data = serialPort.ReadLine(); // Arduino �跢��һ������Ƕȵ��ַ���
                float angle = float.Parse(data);
                return angle;
            }
            catch
            {
                // ���Խ�������
            }
        }
        return 999f; // ����һ�����׵ĽǶȣ�ȷ�������ж�Ϊˮƽ
    }

    void ResumeGame()
    {
        isLevel = true;
        pauseCanvas.enabled = false;
        Time.timeScale = 1f;
        Debug.Log("�ѻָ���Ϸ");
    }

    void PauseGame()
    {
        isLevel = false;
        pauseCanvas.enabled = true;
        Time.timeScale = 0f;
        Debug.Log("��Ϸ����ͣ��������δˮƽ");
    }

    void OnApplicationQuit()
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.Close();
        }
    }
}
