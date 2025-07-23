using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManage : MonoBehaviour
{
    public static ScoreManage instance;
    public int score = 0;
    public int ckp;
    public BugSpawner bug;
    public TextMeshProUGUI scoreText;
    public GameObject hint;
    private bool _bigWaveSpawned = false;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        _bigWaveSpawned = false;
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
    }
    void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score;
    }
    void Update()
    {
        if (!_bigWaveSpawned && score >= ckp)
        {
            Debug.Log("trig");
            hint.SetActive(true);
            for (int i = 0; i < 100; i++)
            {
                bug.SpawnInsects();
            }
            ExecuteAfterTime(3f);
            _bigWaveSpawned = true;
            hint.SetActive(false);


        }
    }
    IEnumerator ExecuteAfterTime(float time)
    {
        // 等待指定的时间（以秒为单位）
        yield return new WaitForSeconds(time);
    }
}
