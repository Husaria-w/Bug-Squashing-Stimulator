using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spray : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("crash");
        die insect = other.GetComponent<die>();
        if (insect != null)
        {
            insect.Die(); // ���������Ҫ��ʧ

        }
    }
}
