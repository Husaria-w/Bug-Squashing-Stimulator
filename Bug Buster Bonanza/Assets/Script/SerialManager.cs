using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class SerialManager : MonoBehaviour
{
    public static SerialManager Instance;

    SerialPort serial;

    public string portName = "COM3";
    public int baudRate = 19200;

    public string LatestLine { get; private set; }
    public string[] SensorData { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            serial = new SerialPort(portName, baudRate);
            serial.ReadTimeout = 50;

            try
            {
                serial.Open();
                Debug.Log("SerialManager: Serial connected.");
            }
            catch
            {
                Debug.LogError("SerialManager: Failed to open serial port.");
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (serial != null && serial.IsOpen)
        {
            try
            {
                LatestLine = serial.ReadLine();
                SensorData = LatestLine.Split('/');
            }
            catch (System.Exception e)
            {
                Debug.LogWarning("Serial read failed: " + e.Message);
                // ¶ÁÈ¡³¬Ê±¿ÉºöÂÔ
            }
        }
    }

    void OnApplicationQuit()
    {
        if (serial != null && serial.IsOpen)
        {
            serial.Close();
            Debug.Log("SerialManager: Serial closed.");
        }
    }
}

