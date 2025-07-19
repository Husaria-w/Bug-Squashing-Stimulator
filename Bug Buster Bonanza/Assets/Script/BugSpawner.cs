using UnityEngine;

public class BugSpawner : MonoBehaviour
{
    public GameObject[] bugPrefabs;      // �Ͻ�ȥ���ֳ��ӵ�Ԥ����
    public Transform[] spawnPoints;      // �Ͻ�ȥ6��������� Transform

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
