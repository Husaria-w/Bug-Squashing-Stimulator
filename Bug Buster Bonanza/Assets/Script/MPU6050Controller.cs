using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;

public class MPU6050Controller : MonoBehaviour
{
    float threshold = 0.05f; // 噪声阈值
    public Transform sprayHeadObj;
    public Transform cursorObj;
    private string[] sensorData;
    public float pitchSensitivity = 10f; // 可在 Inspector 调整
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
                        // 累计 warmup 期间的值
                        accZQueue.Enqueue(accZ);
                        warmupFrames--;

                        if (warmupFrames == 0)
                        {
                            // warmup 结束，计算平均，作为 offset
                            float sum = 0f;
                            foreach (var v in accZQueue) sum += v;
                            accZOffset = sum / accZQueue.Count;
                        }
                        return; // warmup 阶段不更新准星
                    }
                    accZ -= accZOffset; // 去除启动偏置

                    if (Mathf.Abs(accX) < threshold) accX = 0;
                    if (Mathf.Abs(accZ) < threshold) accZ = 0;

                    //应用旋转到喷头
                    sprayHeadObj.localRotation = Quaternion.Euler(pitch, 0f, roll);


                    //应用加速度到准星位置（示例：仅 x/y 平移，未做阈值与速度积分
                    Vector3 cursorPos = cursorObj.localPosition;

                    //直接加上加速度（可改为乘以比例系数后累加积分，或检测峰值后位移一步）
                    cursorPos.x += accX * Time.deltaTime * 10f;
                    cursorPos.y += -pitch * Time.deltaTime * pitchSensitivity;


                    //限制准星不出画面
                    cursorPos.x = Mathf.Clamp(cursorPos.x, -8f, 8f);
                    cursorPos.y = Mathf.Clamp(cursorPos.y, -5f, 5f);

                    cursorObj.localPosition = cursorPos;
                }
            
            
        }
    }

   
