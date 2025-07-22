using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Move : MonoBehaviour
{
    public float moveSpeed = 2f;              // 移动速度
    public float minMoveDistance = 1f;        // 最小移动距离
    public float maxMoveDistance = 5f;        // 最大移动距离
    public float waitTime = 2f;               // 每次移动后停留时间
    public float fixedHeight = 0f;            // 保持固定高度
    private Animator animator;
    private bool isMoving = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(Wander());
    }

    public void StopMove()
    {
        StopAllCoroutines();
    }
    IEnumerator Wander()
    {
        while (true)
        {
            // 随机方向和距离
            Vector2 randomDir = Random.insideUnitCircle.normalized;
            float moveDistance = Random.Range(minMoveDistance, maxMoveDistance);
            Vector3 startPos = transform.position;
            Vector3 targetPos = startPos + new Vector3(randomDir.x, 0f, randomDir.y) * moveDistance;

            // 保持在固定高度
            targetPos.y = fixedHeight;

            isMoving = true;
            //animator.SetBool("isMoving", true);

            // 移动过程
            while (Vector3.Distance(transform.position, targetPos) > 0.1f)
            {
                // 面朝目标方向
                Vector3 dir = (targetPos - transform.position).normalized;
                transform.forward = new Vector3(dir.x, 0, dir.z);

                // 移动
                transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

                // 保持高度
                transform.position = new Vector3(transform.position.x, fixedHeight, transform.position.z);

                yield return null;
            }

            isMoving = false;
            //animator.SetBool("isMoving", false);

            // 等待一段时间再继续
            yield return new WaitForSeconds(waitTime);
        }
    }
}

