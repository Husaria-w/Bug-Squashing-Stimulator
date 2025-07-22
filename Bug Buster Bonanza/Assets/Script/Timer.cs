using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float targetTime = 60f;           // 游戏总时间
    public GameObject endCanvas;             // 游戏结束画布
    public TextMeshProUGUI timerText;        // 显示剩余时间的文本

    private float timer = 0f;
    private bool gameEnded = false;

    void Start()
    {
        timer = 0f;
        if (endCanvas != null)
            endCanvas.SetActive(false);
    }

    void Update()
    {
        if (gameEnded) return;

        timer += Time.deltaTime;

        // 计算剩余时间
        float timeLeft = Mathf.Clamp(targetTime - timer, 0f, targetTime);

        // 更新 UI 显示
        if (timerText != null)
        {
            timerText.text = "Time Left: " + timeLeft.ToString("F1") + "s";
        }

        // 时间结束时触发
        if (timeLeft <= 0f)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        gameEnded = true;

        if (endCanvas != null)
            endCanvas.SetActive(true);

        // 可选：暂停游戏
        // Time.timeScale = 0f;
    }
}
