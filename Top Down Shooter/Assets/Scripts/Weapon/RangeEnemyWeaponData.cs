using UnityEngine;

namespace TDS
{
    [CreateAssetMenu(fileName = "New Range Enemy Weapon Data", menuName = "TDS/Weapon System/Range Enemy Weapon Data")]

    public class RangeEnemyWeaponData : ScriptableObject
    {
        [field: Header("Weapon Settings")]
        [field: SerializeField] public WeaponType WeaponType { get; private set; }
        [field: SerializeField] public int BulletsPerSecond { get; private set; } = 1;
        [SerializeField] int minBulletsPerBurst = 1, maxBulletsPerBurst = 1;
        [SerializeField] int minCooldownTime = 2, maxCooldownTime = 5;

        [field: Header("Bullet Settings")]
        [field: SerializeField] public float BulletSpeed { get; private set; } = 20;
        [SerializeField] float bulletSpread = 0.1f;

        public int GetBulletsPerBurst() => Random.Range(minBulletsPerBurst, maxBulletsPerBurst + 1);
        public float GetCooldownTime() => Random.Range(minCooldownTime, maxCooldownTime + 1);

        public Vector3 ApplySpread(Vector3 originalDirection)
        {
            float randomVal = Random.Range(-bulletSpread, bulletSpread);

            Quaternion spreadRotation = Quaternion.Euler(randomVal, randomVal, randomVal);

            return spreadRotation * originalDirection;
        }
    }
}
