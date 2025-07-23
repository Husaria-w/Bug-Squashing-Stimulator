using TMPro;
using UnityEngine;

public class ScoreManage : MonoBehaviour
{
    public static ScoreManage instance;
    public int score = 0;
    public int ckp;
    public BugSpawner bug;
    public TextMeshProUGUI scoreText;
    public GameObject hint;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
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
        if(score == ckp)
        {
            hint.SetActive(true);
            for(int i =0; i<20; i++)
            {
                bug.SpawnInsects();
            }

        }
    }
}
