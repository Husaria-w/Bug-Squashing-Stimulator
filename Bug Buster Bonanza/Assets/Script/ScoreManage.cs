using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScoreManage : MonoBehaviour
{
    public static ScoreManage instance;
    public int score = 0;
    public int ckp;
    public int spawn;
    public BugSpawner bug;
    public int add;
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
        if (score >= ckp)
        {
            ckp += add;
            add += 50;
            spawn += 20;
            Debug.Log("trig");

            for (int i = 0; i < spawn; i++)
            {
                bug.SpawnInsects();
            }
           StartCoroutine(ExecuteAfterTime(3f, HideHint));
            bug.SpawnTime /= 2;
            
        }
    }
    IEnumerator ExecuteAfterTime(float time,Action a)
    {
        // 等待指定的时间（以秒为单位）
        yield return new WaitForSeconds(time);

        a?.Invoke();
    }

    void HideHint()
    {
        hint.SetActive(false);
    }
}
