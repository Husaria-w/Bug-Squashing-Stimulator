using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spray : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("crash");
        BugDeath insect = other.GetComponent<BugDeath>();
        if (insect != null)
        {
            insect.Die(); // ���������Ҫ��ʧ

        }
    }
}
