using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    public float fixedY = 0f;                     // 保持在固定高度
    public GameObject Obj;              // 要生成的物体（Prefab）
    public int Bar;

    void Update()
    {
        // 射线从相机发出，指向鼠标位置
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, new Vector3(0, fixedY, 0));

        if (groundPlane.Raycast(ray, out float distance))
        {
            Vector3 hitPoint = ray.GetPoint(distance);
            transform.position = hitPoint;

            // 鼠标左键点击时，在当前位置生成新物体
            if (Input.GetMouseButtonDown(0))
            {
                if (Obj != null)
                {
                    Instantiate(Obj, transform.position, Quaternion.identity);
                    Bar -= 1;
                }
                else
                {
                    Debug.LogWarning("objectToSpawn 未设置！");
                }
            }
        }
    }
}
