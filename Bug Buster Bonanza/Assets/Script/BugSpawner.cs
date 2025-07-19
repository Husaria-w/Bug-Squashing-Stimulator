using UnityEngine;

public class BugSpawner : MonoBehaviour
{
    public GameObject[] bugPrefabs;      // 拖进去三种虫子的预制体
    public Transform[] spawnPoints;      // 拖进去6个空物体的 Transform

    void Start()
    {
        SpawnFixedBugs();
    }

    void SpawnFixedBugs()
    {
        foreach (Transform point in spawnPoints)
        {
            int index = Random.Range(0, bugPrefabs.Length);
            Instantiate(bugPrefabs[index], point.position, Quaternion.identity);
        }
    }
}
