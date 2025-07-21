using UnityEngine;

public class InsectController : MonoBehaviour
{
    public void Die()
    {
        // 可加死亡动画或粒子效果
        Destroy(gameObject);
        ScoreManage.instance.AddScore(1);
    }
}
