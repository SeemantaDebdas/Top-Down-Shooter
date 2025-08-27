using UnityEngine;

namespace TDS
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] LayerMask targetLayerMask;
        [SerializeField] GameObject bulletHitEffect;
        [SerializeField] TrailRenderer trailRenderer; // Reference to the TrailRenderer

        float bulletRange;
        Vector3 startPosition;

        private float trailRendererInitialTime;

        public void Setup(float bulletRange, Vector3 position)
        {
            this.bulletRange = bulletRange;

            //transform.SetPositionAndRotation(position, rotation);
            startPosition = position;

            // Reset trail renderer at the beginning
            if (trailRenderer != null)
            {
                trailRenderer.Clear();
                trailRenderer.time = trailRendererInitialTime;
            }
        }

        void Awake()
        {
            // Store the initial time of the trail renderer
            if (trailRenderer != null)
            {
                trailRendererInitialTime = trailRenderer.time;
            }
        }

        void Update()
        {
            float distanceTravelled = Vector3.Distance(transform.position, startPosition);

            // Fade the trail renderer as the bullet approaches the end of its range
            if (trailRenderer != null)
            {
                float remainingDistanceNormalized = 1.0f - (distanceTravelled / bulletRange);
                trailRenderer.time = trailRendererInitialTime * remainingDistanceNormalized;
            }

            if (distanceTravelled > bulletRange)
            {
                ObjectPool.Instance.TryReturnObjectToPool(gameObject);
            }
        }

        protected virtual void OnCollisionEnter(Collision collision)
        {
            if ((targetLayerMask & (1 << collision.gameObject.layer)) != 0)
            {
                Debug.Log("Colliding with: " + collision.transform.name + " Root: " + collision.transform.root.name);

                // Check for rigidbody to prevent errors on static objects
                Rigidbody rigidbody = GetComponent<Rigidbody>();
                if (rigidbody == null)
                {
                    Debug.LogWarning("Bullet has no Rigidbody component. Impact force will not be applied.");
                }

                CreateImpactVFX(collision);

                if (collision.gameObject.TryGetComponent(out Shield shield))
                {
                    shield.Damage();
                }
                else if (collision.transform.root.TryGetComponent(out Enemy enemy))
                {
                    enemy.GetHit();
                    // Ensure rigidbody exists before trying to access its linearVelocity
                    if (rigidbody != null)
                    {
                        enemy.AddImpactForceAfterDeath(collision.rigidbody, rigidbody.linearVelocity, collision.contacts[0].point);
                    }
                }

                ObjectPool.Instance.TryReturnObjectToPool(gameObject);
            }
        }

        protected void CreateImpactVFX(Collision collision)
        {
            if (collision.contactCount > 0)
            {
                GameObject spawnedImpactFx = ObjectPool.Instance.GetObject(bulletHitEffect);
                spawnedImpactFx.transform.SetPositionAndRotation(collision.contacts[0].point, Quaternion.LookRotation(collision.contacts[0].normal));
            }
        }
    }
}