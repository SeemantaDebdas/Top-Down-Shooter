using TDS.Input;
using UnityEngine;

namespace TDS
{
    public class PlayerWeaponController : MonoBehaviour
    {
        [SerializeField] InputSO input;

        [Header("Weapon Settings")]
        [SerializeField] Transform weaponHolder = null;

        [Header("Bullet Settings")]
        [SerializeField] GameObject bulletPrefab = null;
        [SerializeField] float bulletSpeed = 5f;
        [field: SerializeField] public Transform BulletSpawnPoint { get; private set; } = null;

        Animator animator;
        PlayerAim playerAim;

        void Awake()
        {
            animator = GetComponent<Animator>();
            playerAim = GetComponent<PlayerAim>();
        }

        void OnEnable()
        {
            input.OnFirePerformed += Input_OnFirePerformed;
        }

        void OnDisable()
        {
            input.OnFirePerformed -= Input_OnFirePerformed;
        }

        private void Input_OnFirePerformed()
        {
            Shoot();
        }


        void Shoot()
        {
            weaponHolder.LookAt(playerAim.AimVisual);
            BulletSpawnPoint.LookAt(playerAim.AimVisual);

            GameObject spawnedBullet = Instantiate(bulletPrefab, BulletSpawnPoint.position, Quaternion.LookRotation(BulletSpawnPoint.forward));

            spawnedBullet.GetComponent<Rigidbody>().linearVelocity = GetBulletDirection() * bulletSpeed;

            Destroy(spawnedBullet, 3f);

            animator.SetTrigger("fire");
        }

        public Vector3 GetBulletDirection()
        {
            Vector3 bulletDirection = (playerAim.AimVisual.position - BulletSpawnPoint.position).normalized;

            if (!playerAim.AimPrecisely && !playerAim.TryGetTargetAtMousePosition(out _))
                bulletDirection.y = 0;

            return bulletDirection;
        }
    }
}
