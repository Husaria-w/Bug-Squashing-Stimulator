using UnityEngine;

public class Smoke : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("crash");
        die target = other.GetComponent<die>(); // ע��Сд����
        if (target != null)
        {
            target.Die();
        }
    }
}
