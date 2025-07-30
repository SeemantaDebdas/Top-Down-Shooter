using UnityEngine;

namespace TDS
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] LayerMask targetLayerMask;
        [SerializeField] GameObject bulletHitEffect;

        float bulletRange;
        Vector3 startPosition;

        public void Setup(float bulletRange, Vector3 position, Quaternion rotation)
        {
            this.bulletRange = bulletRange;

            transform.SetPositionAndRotation(position, rotation);
            startPosition = position;
        }

        void Update()
        {
            if (Vector3.Distance(transform.position, startPosition) > bulletRange)
                ObjectPool.Instance.TryReturnObjectToPool(gameObject);
        }

        void OnCollisionEnter(Collision collision)
        {
            if ((targetLayerMask & (1 << collision.gameObject.layer)) != 0)
            {
                Debug.Log("Colliding with: " + collision.transform.name + " Root: " + collision.transform.root.name);

                Rigidbody rigidbody = GetComponent<Rigidbody>();
                // rigidbody.constraints = RigidbodyConstraints.FreezeAll;
                // rigidbody.isKinematic = true;
                if (collision.contactCount > 0)
                {
                    GameObject spawnedImpactFx = ObjectPool.Instance.GetObject(bulletHitEffect);
                    spawnedImpactFx.transform.SetPositionAndRotation(collision.contacts[0].point, Quaternion.LookRotation(collision.contacts[0].normal));
                }

                if (collision.gameObject.TryGetComponent(out Shield shield))
                {
                    shield.Damage();
                }
                else if (collision.transform.root.TryGetComponent(out Enemy enemy))
                {
                    enemy.GetHit();
                    enemy.AddImpactForceAfterDeath(collision.rigidbody, rigidbody.linearVelocity, collision.contacts[0].point);
                }


                // Destroy(gameObject);
                ObjectPool.Instance.TryReturnObjectToPool(gameObject);
            }
        }

    }
}
