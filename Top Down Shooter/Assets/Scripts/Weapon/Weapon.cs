using System;
using UnityEngine;

namespace TDS
{
    public enum WeaponType
    {
        Pistol,
        Revolver,
        AutoRifle,
        Shotgun,
        Sniper
    }

    public enum ShootType { Single, Auto }

    [System.Serializable]
    public class Weapon
    {
        public WeaponType weaponType;

        [Header("Magazine Settings")]
        public int bulletsInMagazine;
        public int magazineCapacity;
        public int totalReserveAmmo;

        //Reason why range starts from 1.1 
        //Unity inspector treats value 1 = 0. So weapons during runtime are played with 0 speed and 0 fireRate
        [Header("Animator Settings")]
        [Range(1.1f, 2f)] public float equipSpeed;
        [Range(1.1f, 2f)] public float reloadSpeed;


        [Header("Shoot Settings")]
        public ShootType shootType;
        [Range(4f, 20f)] public float weaponRange = 4;
        [Range(1.1f, 20f)] public float fireRate;
        [Range(1.1f, 20f)] public float defaultFireRate;
        public int bulletsPerShot = 1;

        [Header("Burst Fire")]
        public bool hasBurstMode = false;
        public bool burstModeActive = false;
        public float burstFireDelay = 0.1f;
        public int burstBulletsPerShot = 3;
        public float burstFireRate = 1f;

        [Header("Spread Settings")]
        public float baseSpread = 0;
        public float currentSpread = 0;
        public float maxSpread = 3;
        public float spreadIncreaseRate = 0.15f;

        float lastSpreadUpdateTime, spreadCooldownTime = 1;

        float lastShootTime;

        public bool CanShoot() => IsReadyToShoot() && HasEnoughBullets();

        private bool HasEnoughBullets() => bulletsInMagazine > 0;

        public bool IsReadyToShoot()
        {
            if (Time.time > lastShootTime + 1 / fireRate)
            {
                lastShootTime = Time.time;
                return true;
            }

            return false;
        }

        public bool CanReload()
        {
            if (bulletsInMagazine == magazineCapacity)
                return false;

            return totalReserveAmmo > 0;
        }

        public void Reload()
        {
            int bulletsToReload = magazineCapacity;

            if (bulletsToReload > totalReserveAmmo)
                bulletsToReload = totalReserveAmmo;

            totalReserveAmmo -= bulletsToReload;
            bulletsInMagazine = bulletsToReload;
        }

        public Vector3 ApplySpread(Vector3 originalDirection)
        {
            UpdateSpread();

            float randomVal = UnityEngine.Random.Range(-currentSpread, currentSpread);

            Quaternion spreadRotation = Quaternion.Euler(randomVal, randomVal, randomVal);

            return spreadRotation * originalDirection;
        }

        void IncreaseSpread()
        {
            currentSpread = Mathf.Clamp(currentSpread + spreadIncreaseRate, baseSpread, maxSpread);
        }

        void UpdateSpread()
        {
            if (Time.time > lastSpreadUpdateTime + spreadCooldownTime)
                currentSpread = baseSpread;
            else
                IncreaseSpread();

            lastSpreadUpdateTime = Time.time;
        }

        public bool HasBurstMode => hasBurstMode;

        public void ToggleBurstMode()
        {
            if (!hasBurstMode)
                return;

            burstModeActive = !burstModeActive;

            if (burstModeActive)
            {
                bulletsPerShot = burstBulletsPerShot;
                fireRate = burstFireRate;
            }
            else
            {
                bulletsPerShot = 1;
                fireRate = defaultFireRate;
            }
        }

        public bool IsBurstModeActive()
        {
            if (weaponType == WeaponType.Shotgun)
            {
                burstFireDelay = 0;
                return true;
            }

            return burstModeActive;
        }
    }
}
