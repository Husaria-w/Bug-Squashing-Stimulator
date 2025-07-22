using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float targetTime = 60f;           // ��Ϸ��ʱ��
    public GameObject endCanvas;             // ��Ϸ��������
    public TextMeshProUGUI timerText;        // ��ʾʣ��ʱ����ı�

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

        // ����ʣ��ʱ��
        float timeLeft = Mathf.Clamp(targetTime - timer, 0f, targetTime);

        // ���� UI ��ʾ
        if (timerText != null)
        {
            timerText.text = "Time Left: " + timeLeft.ToString("F1") + "s";
        }

        // ʱ�����ʱ����
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

        // ��ѡ����ͣ��Ϸ
        // Time.timeScale = 0f;
    }
}
