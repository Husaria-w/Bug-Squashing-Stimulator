using UnityEngine;

public class InsectController : MonoBehaviour
{
    public void Die()
    {
        // �ɼ���������������Ч��
        Destroy(gameObject);
        ScoreManage.instance.AddScore(1);
    }
}
