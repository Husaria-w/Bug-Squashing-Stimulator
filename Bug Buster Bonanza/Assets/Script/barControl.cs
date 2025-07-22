using UnityEngine;
using UnityEngine.UI;

public class barControl : MonoBehaviour
{
    public Image fillImage;   // 拖拽你的血条 Image
    public MouseSpray follow;
    public GameObject text;
    public float maxBar = 20f; // 最大血量

    void Update()
    {
        float fillAmount = Mathf.Clamp01(follow.bar / maxBar);
        fillImage.fillAmount = fillAmount;
        if(follow.bar == 0)
        {
            text.SetActive(true);
        }
        if (Input.GetMouseButtonDown(1))
        {
            follow.bar += 4;
            text.SetActive(false);
        }
    }
}
