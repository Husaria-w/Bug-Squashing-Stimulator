using UnityEngine;

public class die : MonoBehaviour
{
    public void Die()
    {
        // �ɼ���������������Ч��
        Destroy(gameObject);
        ScoreManage.instance.AddScore(1);
    }
}
