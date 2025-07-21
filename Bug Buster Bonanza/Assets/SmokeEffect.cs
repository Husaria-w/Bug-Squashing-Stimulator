using UnityEngine;

public class SprayController : MonoBehaviour
{
    public ParticleSystem sprayParticle;  // 拖拽引用
    public KeyCode sprayKey = KeyCode.Space; // 按空格键喷雾，可自定义

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
