using UnityEngine;
using UnityEngine.UI;

public class barControl : MonoBehaviour
{
    public Image fillImage;   // ��ק���Ѫ�� Image
    public MouseSpray follow;
    public GameObject text;
    public float maxBar = 20f; // ���Ѫ��

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
