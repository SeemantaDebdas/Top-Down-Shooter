using DG.Tweening;
using UnityEngine;

namespace TDS
{
    public class ThrownWeapon : MonoBehaviour
    {
        [SerializeField] float moveSpeed = 5f;
        [SerializeField] float rotationSpeed = 90f;
        [SerializeField] float rotateTowardsTargetTime = 5f;
        [SerializeField] GameObject hitEffect;
        [SerializeField] TrailRenderer trail = null;
        [field: SerializeField] public GameObject WeaponVisual { get; private set; } = null;
        Vector3 direction;
        Transform target;

        float spinAngle = 0;

        Rigidbody rb;
        void Awake()
        {
            rb = GetComponent<Rigidbody>();
            rb.useGravity = false;
            trail.enabled = false;
        }

        public void Setup(Vector3 spawnPosition, Transform target)
        {
            rb.position = spawnPosition;
            this.target = target;
            DOVirtual.Float(0, 1f, 0.1f, (_) => { }).OnComplete(() => { trail.enabled = true; });
        }

        void Update()
        {
            if (rotateTowardsTargetTime > 0)
            {
                direction = (target.position + Vector3.up * 1f - transform.position).normalized;
                rotateTowardsTargetTime -= Time.deltaTime;
            }

            spinAngle += rotationSpeed * Time.deltaTime;

            rb.linearVelocity = direction * moveSpeed;

            Quaternion lookRotation = Quaternion.LookRotation(rb.linearVelocity);
            Quaternion spinRotation = Quaternion.AngleAxis(spinAngle, Vector3.right);

            transform.rotation = lookRotation * spinRotation;
        }

        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out Bullet _) || collision.gameObject.CompareTag("Player"))
            {
                if (collision.contactCount > 0)
                {
                    GameObject spawnedImpactFx = ObjectPool.Instance.GetObject(hitEffect);
                    spawnedImpactFx.transform.SetPositionAndRotation(collision.contacts[0].point, Quaternion.LookRotation(collision.contacts[0].normal));
                }

                ObjectPool.Instance.TryReturnObjectToPool(gameObject);
            }
        }
    }
}
