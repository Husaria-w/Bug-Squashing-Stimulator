using DG.Tweening;
using UnityEngine;

public class BugDeath : MonoBehaviour
{
    private bool dead;
    public float deathHeight = 12;

    public void Die()
    {
        if (dead)
            return;

        Debug.Log("die: "+gameObject.name);

        GetComponent<Move>().StopMove();
        // 可加死亡动画或粒子效果
        dead = true;
        Destroy(gameObject, 2);
        transform.DOScale(0, 1).SetDelay(0.6f);
        float crtY = transform.position.y;
        transform.DOKill();
        transform.DOMoveY(crtY + deathHeight, 0.4f);
        transform.DOMoveY(crtY, 0.4f).SetDelay(0.5f);
        transform.DORotate(new Vector3(180, 0, 0), 0.5f).SetEase(Ease.OutBack);
        ScoreManage.instance.AddScore(1);
    }
}
