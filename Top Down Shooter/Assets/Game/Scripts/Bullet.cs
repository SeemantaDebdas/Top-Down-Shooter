using UnityEngine;

namespace TDS
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] LayerMask targetLayerMask;
        [SerializeField] GameObject bulletHitEffect;
        void OnCollisionEnter(Collision collision)
        {
            if ((targetLayerMask & (1 << collision.gameObject.layer)) != 0)
            {
                Rigidbody rigidbody = GetComponent<Rigidbody>();
                // rigidbody.constraints = RigidbodyConstraints.FreezeAll;
                // rigidbody.isKinematic = true;
                if (collision.contactCount > 0)
                {
                    Instantiate(bulletHitEffect, collision.contacts[0].point, Quaternion.LookRotation(collision.contacts[0].normal));
                }

                Destroy(gameObject);
            }
        }
    }
}
