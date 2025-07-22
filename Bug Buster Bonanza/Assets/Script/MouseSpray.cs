using UnityEngine;

public class MouseSpray : MonoBehaviour
{
    public int bar;
    public GameObject text;
    public GameObject zone;              // Ҫ���ɵ� zone ����
    public GameObject aim;               // ��ѡ����׼���壨��δʹ�ã�
    public GameObject sprayPrefab;       // ����Ԥ����
    public float fixedZoneY = 1f;        // ���� zone ʱ�Ĺ̶� Y ֵ
    private Camera mainCamera;
    private float fixedY;

    void Start()
    {
        mainCamera = Camera.main;
        fixedY = transform.position.y; // ��������������� Y ����
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && bar >0) // �������������
        {
            SpawnSpray(transform.position);
        }

        // ���ʵʱ���棨X-Z ƽ�棩
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
        // ��������
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

        // ���� zone���̶� Y ֵ��
        if (zone != null)
        {
            Vector3 zonePosition = new Vector3(position.x, fixedZoneY, position.z);
            Instantiate(zone, zonePosition, Quaternion.identity);
            Destroy(gameObject, 2f);
        }
        bar -= 1;
    }
}
