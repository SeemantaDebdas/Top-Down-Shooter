using UnityEngine;

namespace TDS
{
    [CreateAssetMenu(fileName = "New Weapon Data", menuName = "TDS/Weapon System/Weapon Data")]
    public class WeaponData : ScriptableObject
    {
        public string weaponName;

        [Header("Magazine details")]
        public int bulletsInMagazine;
        public int magazineCapacity;
        public int totalReserveAmmo;


        [Header("Regular shot")]
        public ShootType shootType;
        public int bulletsPerShot = 1;
        public float fireRate;


        [Header("Burst shot")]
        public bool hasBurstMode;
        public bool burstModeActive;
        public int burstBulletsPerShot;
        public float burstFireRate;
        public float burstFireDelay = .1f;

        [Header("Weapon spread")]
        public float baseSpread;
        public float maxSpread;
        public float spreadIncreaseRate = .15f;

        [Header("Weapon generics")]
        public WeaponType weaponType;
        [Range(1, 3)]
        public float reloadSpeed = 1;
        [Range(1, 3)]
        public float equipmentSpeed = 1;
        [Range(4, 8)]
        public float weaponRange = 4;
        [Range(4, 8)]
        public float cameraDistance = 6;

    }
}
