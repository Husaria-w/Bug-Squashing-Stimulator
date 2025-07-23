using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;

public class MPU6050Controller : MonoBehaviour
{
    float threshold = 0.05f; // ������ֵ
    public Transform sprayHeadObj;
    public Transform cursorObj;
    private string[] sensorData;
    public float pitchSensitivity = 10f; // ���� Inspector ����
    int warmupFrames = 10;
    Queue<float> accZQueue = new Queue<float>();
    float accZOffset = 0f;

    void Start()
    {
    }

    void Update()
    {
        var sensorData = SerialManager.Instance.SensorData;
 
                if (sensorData != null && sensorData.Length >= 6)
                {
                    float roll = float.Parse(sensorData[1]);
                    float pitch = float.Parse(sensorData[0]);
                    float yaw = float.Parse(sensorData[2]);

                    float accX = float.Parse(sensorData[3]);
                    float accY = float.Parse(sensorData[4]);
                    float accZ = float.Parse(sensorData[5]);
                    if (warmupFrames > 0)
                    {
                        // �ۼ� warmup �ڼ��ֵ
                        accZQueue.Enqueue(accZ);
                        warmupFrames--;

                        if (warmupFrames == 0)
                        {
                            // warmup ����������ƽ������Ϊ offset
                            float sum = 0f;
                            foreach (var v in accZQueue) sum += v;
                            accZOffset = sum / accZQueue.Count;
                        }
                        return; // warmup �׶β�����׼��
                    }
                    accZ -= accZOffset; // ȥ������ƫ��

                    if (Mathf.Abs(accX) < threshold) accX = 0;
                    if (Mathf.Abs(accZ) < threshold) accZ = 0;

                    //Ӧ����ת����ͷ
                    sprayHeadObj.localRotation = Quaternion.Euler(pitch, 0f, roll);


                    //Ӧ�ü��ٶȵ�׼��λ�ã�ʾ������ x/y ƽ�ƣ�δ����ֵ���ٶȻ���
                    Vector3 cursorPos = cursorObj.localPosition;

                    //ֱ�Ӽ��ϼ��ٶȣ��ɸ�Ϊ���Ա���ϵ�����ۼӻ��֣������ֵ��λ��һ����
                    cursorPos.x += accX * Time.deltaTime * 10f;
                    cursorPos.y += -pitch * Time.deltaTime * pitchSensitivity;


                    //����׼�ǲ�������
                    cursorPos.x = Mathf.Clamp(cursorPos.x, -8f, 8f);
                    cursorPos.y = Mathf.Clamp(cursorPos.y, -5f, 5f);

                    cursorObj.localPosition = cursorPos;
                }
            
            
        }
    }

   
