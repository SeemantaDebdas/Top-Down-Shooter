using UnityEngine;

namespace TDS
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] LayerMask targetLayerMask;
        void OnCollisionEnter(Collision collision)
        {
            if ((targetLayerMask & (1 << collision.gameObject.layer)) != 0)
            {
                Rigidbody rigidbody = GetComponent<Rigidbody>();
                rigidbody.constraints = RigidbodyConstraints.FreezeAll;
                rigidbody.isKinematic = true;
            }
        }
    }
}
