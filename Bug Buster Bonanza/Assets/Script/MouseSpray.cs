using UnityEngine;

public class MouseSpray : MonoBehaviour
{
    public int bar;
    public GameObject text;
    public GameObject zone;              // 要生成的 zone 对象
    public GameObject aim;               // 可选的瞄准物体（暂未使用）
    public GameObject sprayPrefab;       // 喷雾预制体
    public float fixedZoneY = 1f;        // 生成 zone 时的固定 Y 值
    private Camera mainCamera;
    private float fixedY;

    void Start()
    {
        mainCamera = Camera.main;
        fixedY = transform.position.y; // 锁定喷雾跟随器的 Y 坐标
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && bar >0) // 鼠标左键点击生成
        {
            SpawnSpray(transform.position);
        }

        // 鼠标实时跟随（X-Z 平面）
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane xzPlane = new Plane(Vector3.up, new Vector3(0, fixedY, 0));

        if (xzPlane.Raycast(ray, out float distance))
        {
            Vector3 hitPoint = ray.GetPoint(distance);
            transform.position = new Vector3(hitPoint.x, fixedY, hitPoint.z);
        }
        if (bar == 0)
        {
            text.SetActive(true);
        }
    }

    void SpawnSpray(Vector3 position)
    {
        // 生成喷雾
        if (sprayPrefab != null)
        {
            GameObject spray = Instantiate(sprayPrefab, position, Quaternion.identity);
            ParticleSystem ps = spray.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                ps.Play();
            }
            Destroy(spray, 2f);
        }

        // 生成 zone（固定 Y 值）
        if (zone != null)
        {
            Vector3 zonePosition = new Vector3(position.x, fixedZoneY, position.z);
            Instantiate(zone, zonePosition, Quaternion.identity);
            Destroy(gameObject, 2f);
        }
        bar -= 1;
    }
}
