using System;
using System.Collections;
using System.Collections.Generic;
using TDS.Input;
using UnityEngine;

namespace TDS
{
    public class PlayerWeaponController : MonoBehaviour
    {
        const float REFERENCE_BULLET_SPEED = 20; //for setting the speed to mass ratio. for mass = 1, speed = 20
        [SerializeField] InputSO input;

        [Header("Weapon Settings")]
        [SerializeField] Transform weaponHolder = null;
        [field: SerializeField] public Weapon CurrentWeapon { get; private set; }

        [Header("Bullet Settings")]
        [SerializeField] GameObject bulletPrefab = null;
        [SerializeField] float bulletSpeed = 5f;

        [Header("Inventory")]
        [SerializeField] int maxSlots = 2;
        [SerializeField] List<Weapon> weaponSlots;
        PlayerAim playerAim;
        PlayerWeaponVisuals weaponVisuals;
        bool isShooting = false;

        public bool WeaponReady { get; private set; } = false;

        void Awake()
        {
            playerAim = GetComponent<PlayerAim>();
            weaponVisuals = GetComponent<PlayerWeaponVisuals>();
        }

        void Start()
        {
            EquipWeapon(0);
        }

        void OnEnable()
        {
            input.OnFirePerformed += Input_OnFirePerformed;
            input.OnFireCancelled += Input_OnFireCancelled;

            input.OnEquipWeapon1Performed += () => EquipWeapon(0);
            input.OnEquipWeapon2Performed += () => EquipWeapon(1);

            input.OnDropPerformed += Input_OnDropPerformed;
            input.OnReloadPerformed += Input_OnReloadPerformed;
        }

        void OnDisable()
        {
            input.OnFirePerformed -= Input_OnFirePerformed;
            input.OnFireCancelled -= Input_OnFireCancelled;
            input.OnDropPerformed -= Input_OnDropPerformed;
            input.OnReloadPerformed -= Input_OnReloadPerformed;
        }


        void Update()
        {
            if (isShooting)
            {
                Shoot();
            }

            if (UnityEngine.Input.GetKeyDown(KeyCode.T))
                CurrentWeapon.ToggleBurstMode();
        }

        private void Input_OnFirePerformed()
        {
            isShooting = true;
            Shoot();
        }
        private void Input_OnFireCancelled()
        {
            isShooting = false;
        }

        private void Input_OnReloadPerformed()
        {
            if (!CurrentWeapon.CanReload())
                return;

            if (!WeaponReady)
                return;

            SetWeaponReady(false);

            weaponVisuals.PlayReloadAnimation();
        }

        private void Input_OnDropPerformed()
        {
            if (weaponSlots.Count <= 1)
                return;

            weaponSlots.Remove(CurrentWeapon);
            EquipWeapon(0);
        }

        public void SetWeaponReady(bool isReady) => WeaponReady = isReady;

        private void EquipWeapon(int weaponIndex)
        {
            SetWeaponReady(false);

            CurrentWeapon = weaponSlots[weaponIndex];

            weaponVisuals.PlayWeaponEquipAnimation();

            CameraManager.Instance.ChangeCameraDistance(CurrentWeapon.weaponRange);
        }

        void Shoot()
        {
            if (!CurrentWeapon.CanShoot())
                return;

            if (!WeaponReady)
                return;

            if (CurrentWeapon.shootType == ShootType.Single)
                isShooting = false;


            if (CurrentWeapon.IsBurstModeActive())
                StartCoroutine(BurstFire());
            else
                ShootSingleBullet();


            weaponVisuals.PlayShootAnimation();
        }

        IEnumerator BurstFire()
        {
            SetWeaponReady(false);

            for (int i = 0; i < CurrentWeapon.bulletsPerShot; i++)
            {
                ShootSingleBullet();
                yield return new WaitForSeconds(CurrentWeapon.burstFireDelay);
            }

            SetWeaponReady(true);
        }

        private void ShootSingleBullet()
        {
            CurrentWeapon.bulletsInMagazine--;

            GameObject spawnedBullet = ObjectPool.Instance.GetBullet();
            spawnedBullet.transform.SetPositionAndRotation(GetBulletSpawnPoint().position, Quaternion.LookRotation(GetBulletSpawnPoint().forward));

            Bullet bullet = spawnedBullet.GetComponent<Bullet>();
            bullet.Setup(CurrentWeapon.weaponRange);

            Rigidbody bulletRb = spawnedBullet.GetComponent<Rigidbody>();
            bulletRb.mass = REFERENCE_BULLET_SPEED / bulletSpeed;

            Vector3 bulletDirection = CurrentWeapon.ApplySpread(GetBulletDirection());

            bulletRb.linearVelocity = bulletDirection * bulletSpeed;
        }

        public Vector3 GetBulletDirection()
        {
            Vector3 bulletDirection = (playerAim.AimVisual.position - GetBulletSpawnPoint().position).normalized;

            if (!playerAim.AimPrecisely && !playerAim.TryGetTargetAtMousePosition(out _))
                bulletDirection.y = 0;

            return bulletDirection;
        }

        public Transform GetBulletSpawnPoint()
        {
            return weaponVisuals.GetCurrentWeaponModel().gunPoint;
        }


        /// <summary>
        /// Called when weapon is picked up
        /// </summary>
        /// <param name="weapon"></param>
        public void AddWeapon(Weapon weapon)
        {
            if (weaponSlots.Count >= maxSlots)
                return;

            weaponSlots.Add(weapon);
            weaponVisuals.SwitchOnCurrentBackupWeaponModel();
        }

        public void Reload()
        {
            CurrentWeapon.Reload();
            SetWeaponReady(true);
        }

        public Weapon GetBackupWeapon()
        {
            foreach (Weapon weapon in weaponSlots)
            {
                if (weapon != CurrentWeapon)
                {
                    print(weapon.weaponType);
                    return weapon;
                }
            }

            return null;
        }

        public void FinishedEquippingWeapon()
        {
            SetWeaponReady(true);
        }
    }
}
