using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Move : MonoBehaviour
{
    public float moveSpeed = 2f;              // �ƶ��ٶ�
    public float minMoveDistance = 1f;        // ��С�ƶ�����
    public float maxMoveDistance = 5f;        // ����ƶ�����
    public float waitTime = 2f;               // ÿ���ƶ���ͣ��ʱ��
    public float fixedHeight = 0f;            // ���̶ֹ��߶�
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
            // �������;���
            Vector2 randomDir = Random.insideUnitCircle.normalized;
            float moveDistance = Random.Range(minMoveDistance, maxMoveDistance);
            Vector3 startPos = transform.position;
            Vector3 targetPos = startPos + new Vector3(randomDir.x, 0f, randomDir.y) * moveDistance;

            // �����ڹ̶��߶�
            targetPos.y = fixedHeight;

            isMoving = true;
            //animator.SetBool("isMoving", true);

            // �ƶ�����
            while (Vector3.Distance(transform.position, targetPos) > 0.1f)
            {
                // �泯Ŀ�귽��
                Vector3 dir = (targetPos - transform.position).normalized;
                transform.forward = new Vector3(dir.x, 0, dir.z);

                // �ƶ�
                transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

                // ���ָ߶�
                transform.position = new Vector3(transform.position.x, fixedHeight, transform.position.z);

                yield return null;
            }

            isMoving = false;
            //animator.SetBool("isMoving", false);

            // �ȴ�һ��ʱ���ټ���
            yield return new WaitForSeconds(waitTime);
        }
    }
}

