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
        public WeaponData WeaponData { get; private set; }

        [Header("Magazine Settings")]
        public int bulletsInMagazine;
        public int magazineCapacity;
        public int totalReserveAmmo;

        //Reason why range starts from 1.1 
        //Unity inspector treats value 1 = 0. So weapons during runtime are played with 0 speed and 0 fireRate
        [Header("Animator Settings")]
        public float equipSpeed { get; private set; }
        public float reloadSpeed { get; private set; }


        [Header("Shoot Settings")]
        public ShootType shootType;
        public float weaponRange { get; private set; } = 4;
        public float fireRate;
        private float defaultFireRate;
        public int bulletsPerShot { get; private set; } = 1;

        [Header("Burst Fire")]
        public bool hasBurstMode { get; private set; } = false;
        public bool burstModeActive = false;
        public float burstFireDelay { get; private set; } = 0.1f;
        private int burstBulletsPerShot = 3;
        public float burstFireRate { get; private set; } = 1f;

        [Header("Spread Settings")]
        private float baseSpread = 0;
        private float currentSpread = 0;
        private float maxSpread = 3;
        private float spreadIncreaseRate = 0.15f;

        float lastSpreadUpdateTime, spreadCooldownTime = 1;

        float lastShootTime;
        public Weapon(WeaponData weaponData)
        {
            this.WeaponData = weaponData;

            bulletsInMagazine = weaponData.bulletsInMagazine;
            magazineCapacity = weaponData.magazineCapacity;
            totalReserveAmmo = weaponData.totalReserveAmmo;

            fireRate = weaponData.fireRate;
            weaponType = weaponData.weaponType;

            bulletsPerShot = weaponData.bulletsPerShot;
            shootType = weaponData.shootType;


            hasBurstMode = weaponData.hasBurstMode;
            burstModeActive = weaponData.burstModeActive;
            burstBulletsPerShot = weaponData.burstBulletsPerShot;
            burstFireRate = weaponData.burstFireRate;
            burstFireDelay = weaponData.burstFireDelay;


            baseSpread = weaponData.baseSpread;
            maxSpread = weaponData.maxSpread;
            spreadIncreaseRate = weaponData.spreadIncreaseRate;


            reloadSpeed = weaponData.reloadSpeed;
            equipSpeed = weaponData.equipmentSpeed;
            weaponRange = weaponData.weaponRange;

            defaultFireRate = fireRate;
        }

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
