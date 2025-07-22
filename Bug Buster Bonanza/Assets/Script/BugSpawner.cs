using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugSpawner : MonoBehaviour
{
    public GameObject insectPrefab; // 昆虫模型预制体
    public int insectCount = 10; // 要生成的昆虫数量
    public float spawnRadius = 50f; // 生成区域半径
    public float minDistance = 2f; // 昆虫之间最小距离
    public float fixedHeight = 0f; // 所有昆虫的固定高度
    private List<Vector3> spawnPositions = new List<Vector3>(); // 存储已生成位置
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
        // 确保生成位置不重叠
        while (IsOverlapping(spawnPos))
        {
            spawnPos = GetRandomPosition();
        }

        spawnPositions.Add(spawnPos);

        // 实例化昆虫模型
        GameObject newInsect = Instantiate(insectPrefab, spawnPos, Quaternion.identity);

        // 禁用脚本，让新生成的昆虫不继续执行该脚本
        BugSpawner insectScript = newInsect.GetComponent<BugSpawner>();
        if (insectScript != null)
        {
            insectScript.enabled = false;
        }
    }

    // 随机生成一个位置，固定高度
    Vector3 GetRandomPosition()
    {
        float x = Random.Range(-spawnRadius, spawnRadius);
        float z = Random.Range(-spawnRadius, spawnRadius);
        return new Vector3(x, fixedHeight, z);
    }

    // 检查生成的位置是否与已有位置重叠
    bool IsOverlapping(Vector3 position)
    {
        foreach (Vector3 spawnPos in spawnPositions)
        {
            if (Vector3.Distance(spawnPos, position) < minDistance)
            {
                return true; // 如果距离小于最小距离，则认为重叠
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