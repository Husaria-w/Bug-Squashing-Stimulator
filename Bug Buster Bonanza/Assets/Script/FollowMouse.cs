using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    public float fixedY = 0f;                     // �����ڹ̶��߶�
    public GameObject Obj;              // Ҫ���ɵ����壨Prefab��
    public int Bar;

    void Update()
    {
        // ���ߴ����������ָ�����λ��
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, new Vector3(0, fixedY, 0));

        if (groundPlane.Raycast(ray, out float distance))
        {
            Vector3 hitPoint = ray.GetPoint(distance);
            transform.position = hitPoint;

            // ���������ʱ���ڵ�ǰλ������������
            if (Input.GetMouseButtonDown(0))
            {
                if (Obj != null)
                {
                    Instantiate(Obj, transform.position, Quaternion.identity);
                    Bar -= 1;
                }
                else
                {
                    Debug.LogWarning("objectToSpawn δ���ã�");
                }
            }
        }
    }
}
