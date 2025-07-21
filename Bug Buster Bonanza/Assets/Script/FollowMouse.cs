using UnityEngine;

public class MouseSpray : MonoBehaviour
{
    public GameObject aim;
    public GameObject sprayPrefab;     // 喷雾预制体
    private Camera mainCamera;
    private bool isFollowing = false;
    private float fixedY;

    void Start()
    {
        mainCamera = Camera.main;
        fixedY = transform.position.y; // Keep the object's current Y
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left click
        {
            SpawnSpray(transform.position);
        }


        isFollowing = true;

        if (isFollowing)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            Plane xzPlane = new Plane(Vector3.up, new Vector3(0, fixedY, 0)); // X-Z plane at object's Y

            if (xzPlane.Raycast(ray, out float distance))
            {
                Vector3 hitPoint = ray.GetPoint(distance);
                transform.position = new Vector3(hitPoint.x, fixedY, hitPoint.z);
            }
        }
    }

    void SpawnSpray(Vector3 position)
    {
        if (sprayPrefab != null)
        {
            GameObject spray = Instantiate(sprayPrefab, position, Quaternion.identity);
            ParticleSystem ps = spray.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                ps.Play();
            }

            Destroy(spray, 2f); // 自动销毁
        }
    }
}
