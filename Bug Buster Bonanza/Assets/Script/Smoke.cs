using UnityEngine;

public class Smoke : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("crash");
        die target = other.GetComponent<die>(); // 注意小写类名
        if (target != null)
        {
            target.Die();
        }
    }
}
