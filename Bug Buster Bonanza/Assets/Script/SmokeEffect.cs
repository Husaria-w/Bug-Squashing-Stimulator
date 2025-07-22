using UnityEngine;

public class SprayController : MonoBehaviour
{
    public ParticleSystem sprayParticle;  // ��ק����
    public KeyCode sprayKey = KeyCode.Space; // ���ո���������Զ���

    void Update()
    {
        if (Input.GetKeyDown(sprayKey))
        {
            Spray();
        }
    }

    void Spray()
    {
        if (sprayParticle != null)
        {
            sprayParticle.Play();
        }
    }
}
