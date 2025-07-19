using UnityEngine;
using System.Collections.Generic;

public class BugSpawner : MonoBehaviour
{
    public GameObject[] bugPrefabs;       // 虫子预制体数组
    public int totalBugs = 5;
    public float spawnRange = 5f;
    public float minDistance = 2f;


    private List<Vector3> usedPositions = new List<Vector3>();

    void Start()
    {
        for (int i = 0; i < totalBugs; i++)
        {
            Vector3 spawnPos = GetNonOverlappingPosition();

            int prefabIndex = Random.Range(0, bugPrefabs.Length);
            GameObject bug = Instantiate(bugPrefabs[prefabIndex], spawnPos, Quaternion.identity);

            usedPositions.Add(spawnPos);
        }
    }

    // 找一个不与已生成虫子重叠的位置
    Vector3 GetNonOverlappingPosition()
    {
        int maxTries = 100;
        for (int i = 0; i < maxTries; i++)
        {
            Vector3 pos = new Vector3(
                Random.Range(-spawnRange, spawnRange),
                Random.Range(-spawnRange, spawnRange),
                5f
            );

            bool tooClose = false;

            foreach (Vector3 usedPos in usedPositions)
            {
                if (Vector3.Distance(pos, usedPos) < minDistance)
                {
                    tooClose = true;
                    break;
                }
            }

            if (!tooClose)
                return pos;
        }
}
