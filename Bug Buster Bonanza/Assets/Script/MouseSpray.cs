using UnityEngine;

public class MouseSpray : MonoBehaviour
{
    public Camera mainCamera;          // �������
    public float sprayDistance = 100f; // ���߼�����
    public LayerMask hitLayers;
    public int bar;
    public float radius;
    public GameObject aim;               // ��ѡ����׼���壨��δʹ�ã�
    public GameObject sprayPrefab;       // ����Ԥ����
    public float fixedZoneY = 1f;        // ���� zone ʱ�Ĺ̶� Y ֵ
    private float fixedY;

    void Start()
    {
        mainCamera = Camera.main;
        fixedY = transform.position.y; // ��������������� Y ����
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // �������������
        {
            TrySpawnSpray(transform.position);
        }

        // ���ʵʱ���棨X-Z ƽ�棩
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
            Debug.Log(hit.point);// ��������Ч������������ϵͳ��
           var newSpray = Instantiate(sprayPrefab, hit.point, Quaternion.LookRotation(hit.normal));
            newSpray.GetComponent<ParticleSystem>().Play();
            // �ж��Ƿ���������

           var cols= Physics.OverlapSphere(hit.point, radius);
            foreach (var c in cols)
            {
                die insect = c.GetComponent<die>();
                if (insect != null)
                {
                    insect.Die(); // ��������
                }
            }
          
            Destroy(newSpray, 2f);
        }

        // ���� zone���̶� Y ֵ��
        bar -= 1;
    }
}
