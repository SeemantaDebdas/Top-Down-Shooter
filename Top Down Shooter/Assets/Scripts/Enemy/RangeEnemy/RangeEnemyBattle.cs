using UnityEngine;

namespace TDS
{
    public class RangeEnemyBattle : RangeEnemyState
    {
        float lastTimeFired = -Mathf.Infinity;
        int bulletsShot = 0;
        int bulletsPerBurst;
        float weaponCooldown;

        public RangeEnemyBattle(Enemy enemy, EnemyStatemachine statemachine, string animation) : base(enemy, statemachine, animation)
        {
        }

        public override void Enter()
        {
            base.Enter();

            ResetWeapon();

            enemy.Animator.CrossFadeInFixedTime(animation, 0.1f);
            rangeEnemy.EnableRig();
        }

        public override void Update()
        {
            base.Update();

            enemy.FaceTarget(enemy.Player.position);

            if (bulletsShot >= bulletsPerBurst)
            {
                if (IsCooldownTimeOver())
                {
                    ResetWeapon();
                }

                return;
            }

            if (CanShoot())
            {
                Shoot();
            }
        }

        private void ResetWeapon()
        {
            bulletsShot = 0;
            bulletsPerBurst = rangeEnemy.CurrentWeaponData.GetBulletsPerBurst();
            weaponCooldown = rangeEnemy.CurrentWeaponData.GetCooldownTime();
        }

        private bool CanShoot()
        {
            return Time.time > lastTimeFired + 1f / rangeEnemy.CurrentWeaponData.BulletsPerSecond;
        }

        private void Shoot()
        {
            rangeEnemy.FireSingleBullet();
            lastTimeFired = Time.time;
            bulletsShot++;
        }

        bool IsCooldownTimeOver()
        {
            return Time.time > lastTimeFired + weaponCooldown;
        }
    }
}
