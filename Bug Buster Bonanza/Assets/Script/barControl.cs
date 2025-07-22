using UnityEngine;
using UnityEngine.UI;

public class barControl : MonoBehaviour
{
    public Image fillImage;   // ��ק���Ѫ�� Image
    public MouseSpray follow;
    public float maxBar = 20f; // ���Ѫ��

    void Update()
    {
        float fillAmount = Mathf.Clamp01(follow.bar / maxBar);
        fillImage.fillAmount = fillAmount;
    }
}
