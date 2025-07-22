using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugClear : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Bug")
        {
            Destroy(other);
        }
    }
}
