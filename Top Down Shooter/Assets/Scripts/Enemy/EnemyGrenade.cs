using DG.Tweening;
using UnityEngine;

namespace TDS
{
    public class EnemyGrenade : MonoBehaviour
    {
        [SerializeField] float explosionRadius = 5f;
        [SerializeField] float explosionForce = 5f;
        [SerializeField] float upForce = 2.5f;
        [SerializeField] GameObject explosionVFX;

        Rigidbody rb;

        void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        public void SetupGrenade(Vector3 targetPosition, float timeToTarget, float countdownTime)
        {
            rb.linearVelocity = CalculateLaunchVelocity(targetPosition, timeToTarget);

            DOVirtual.Float(0, 1, timeToTarget + countdownTime, (v) => { }).OnComplete(Explode);
        }

        Vector3 CalculateLaunchVelocity(Vector3 targetPosition, float timeToTarget)
        {
            Vector3 displacement = targetPosition - transform.position;
            Vector3 displacementXZ = new(displacement.x, 0, displacement.z);

            Vector3 velocityXZ = displacementXZ / timeToTarget;

            float velocityY = (displacement.y - Physics.gravity.y * Mathf.Pow(timeToTarget, 2) / 2) / timeToTarget;

            Vector3 launchVelocity = velocityXZ + Vector3.up * velocityY;

            return launchVelocity;
        }

        void Explode()
        {

            Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

            foreach (Collider collider in colliders)
            {
                if (collider.TryGetComponent(out Rigidbody rb))
                {
                    rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, upForce, ForceMode.Impulse);
                }
            }

            GameObject explosionSpawn = ObjectPool.Instance.GetObject(explosionVFX, transform.position);
            DOVirtual.Float(0, 1, 1, (v) => { }).OnComplete(() =>
            {
                ObjectPool.Instance.TryReturnObjectToPool(explosionSpawn);
            });

            ObjectPool.Instance.TryReturnObjectToPool(gameObject);
        }
    }
}
