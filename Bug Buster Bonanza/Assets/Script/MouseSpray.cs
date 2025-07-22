using UnityEngine;

public class MouseSpray : MonoBehaviour
{
    public Camera mainCamera;          // 主摄像机
    public float sprayDistance = 100f; // 射线检测距离
    public LayerMask hitLayers;
    public int bar;
    public float radius;
    public GameObject aim;               // 可选的瞄准物体（暂未使用）
    public GameObject sprayPrefab;       // 喷雾预制体
    public float fixedZoneY = 1f;        // 生成 zone 时的固定 Y 值
    private float fixedY;

    void Start()
    {
        mainCamera = Camera.main;
        fixedY = transform.position.y; // 锁定喷雾跟随器的 Y 坐标
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 鼠标左键点击生成
        {
            TrySpawnSpray(transform.position);
        }

        // 鼠标实时跟随（X-Z 平面）
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane xzPlane = new Plane(Vector3.up, new Vector3(0, fixedY, 0));

        if (xzPlane.Raycast(ray, out float distance))
        {
            Vector3 hitPoint = ray.GetPoint(distance);
            transform.position = new Vector3(hitPoint.x, fixedY, hitPoint.z);
        }

    }

    void TrySpawnSpray(Vector3 position)
    {
        Debug.Log("SpawnSpray"+bar);
        if (bar <= 0)
        {
            return;
        }
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, sprayDistance, hitLayers))
        {
            Debug.Log(hit.point);// 生成喷雾效果（例如粒子系统）
           var newSpray = Instantiate(sprayPrefab, hit.point, Quaternion.LookRotation(hit.normal));
            newSpray.GetComponent<ParticleSystem>().Play();
            // 判断是否命中昆虫

           var cols= Physics.OverlapSphere(hit.point, radius);
            foreach (var c in cols)
            {
                die insect = c.GetComponent<die>();
                if (insect != null)
                {
                    insect.Die(); // 昆虫死亡
                }
            }
          
            Destroy(newSpray, 2f);
        }

        // 生成 zone（固定 Y 值）
        bar -= 1;
    }
}
