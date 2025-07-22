using TMPro;
using UnityEngine;

public class ScoreManage : MonoBehaviour
{
    public static ScoreManage instance;
    public int score = 0;
    public TextMeshProUGUI scoreText;

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
}
