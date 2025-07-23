using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.IO.Ports;

public class Checker : MonoBehaviour
{
    public string portName = "COM3"; // 根据你电脑实际情况设置
    public int baudRate = 9600;
    public float levelThreshold = 5f; // 允许倾斜的角度范围 ±5°
    public float requiredLevelTime = 1f; // 必须保持水平的时间
    public Canvas pauseCanvas; // 指向你的暂停画布
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
            Debug.LogError("无法打开串口，请检查连接！");
        }

        pauseCanvas.enabled = true;
        Time.timeScale = 0f; // 暂停游戏
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
                string data = serialPort.ReadLine(); // Arduino 需发送一个代表角度的字符串
                float angle = float.Parse(data);
                return angle;
            }
            catch
            {
                // 忽略解析错误
            }
        }
        return 999f; // 返回一个离谱的角度，确保不被判定为水平
    }

    void ResumeGame()
    {
        isLevel = true;
        pauseCanvas.enabled = false;
        Time.timeScale = 1f;
        Debug.Log("已恢复游戏");
    }

    void PauseGame()
    {
        isLevel = false;
        pauseCanvas.enabled = true;
        Time.timeScale = 0f;
        Debug.Log("游戏已暂停，控制器未水平");
    }

    void OnApplicationQuit()
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.Close();
        }
    }
}
