using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugSpawner : MonoBehaviour
{
    public GameObject insectPrefab; // ����ģ��Ԥ����
    public int insectCount = 10; // Ҫ���ɵ���������
    public float spawnRadius = 50f; // ��������뾶
    public float minDistance = 2f; // ����֮����С����
    public float fixedHeight = 0f; // ��������Ĺ̶��߶�
    private List<Vector3> spawnPositions = new List<Vector3>(); // �洢������λ��
    public float timer;

    void Start()
    {
        for (int i = 0; i < insectCount; i++)
        {
            SpawnInsects();
        }
    }

    void SpawnInsects()
    {
        Vector3 spawnPos = GetRandomPosition();
        // ȷ������λ�ò��ص�
        while (IsOverlapping(spawnPos))
        {
            spawnPos = GetRandomPosition();
        }

        spawnPositions.Add(spawnPos);

        // ʵ��������ģ��
        GameObject newInsect = Instantiate(insectPrefab, spawnPos, Quaternion.identity);

        // ���ýű����������ɵ����治����ִ�иýű�
        BugSpawner insectScript = newInsect.GetComponent<BugSpawner>();
        if (insectScript != null)
        {
            insectScript.enabled = false;
        }
    }

    // �������һ��λ�ã��̶��߶�
    Vector3 GetRandomPosition()
    {
        float x = Random.Range(-spawnRadius, spawnRadius);
        float z = Random.Range(-spawnRadius, spawnRadius);
        return new Vector3(x, fixedHeight, z);
    }

    // ������ɵ�λ���Ƿ�������λ���ص�
    bool IsOverlapping(Vector3 position)
    {
        foreach (Vector3 spawnPos in spawnPositions)
        {
            if (Vector3.Distance(spawnPos, position) < minDistance)
            {
                return true; // �������С����С���룬����Ϊ�ص�
            }
        }
        return false;
    }


    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= 10f)
        {
            SpawnInsects();
            Debug.Log("spawn");
            timer = 0;
        }
       
    }
}