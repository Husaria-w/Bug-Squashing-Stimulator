using UnityEngine;

public class Smoke : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("crash");
        BugDeath target = other.GetComponent<BugDeath>(); // ע��Сд����
        if (target != null)
        {
            target.Die();
        }
    }
}
