using UnityEngine;

namespace TDS
{
    public class BulletImpactFX : MonoBehaviour
    {
        void OnParticleSystemStopped()
        {
            ObjectPool.Instance.TryReturnObjectToPool(gameObject);
        }
    }
}
