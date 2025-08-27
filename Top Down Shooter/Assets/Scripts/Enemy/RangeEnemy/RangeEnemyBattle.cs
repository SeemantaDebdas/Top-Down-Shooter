using UnityEngine;

namespace TDS
{
    public class RangeEnemyBattle : RangeEnemyState
    {
        float lastTimeFired = -Mathf.Infinity;
        int bulletsShot = 0;
        int bulletsPerBurst;
        float weaponCooldown;

        float coverCheckInterval = 0.5f; // check twice per second
        float coverCheckTimer = 0f;

        public RangeEnemyBattle(Enemy enemy, EnemyStatemachine statemachine, string animation) : base(enemy, statemachine, animation)
        {
        }

        public override void Enter()
        {
            base.Enter();

            ResetWeapon();

            enemy.Agent.ResetPath();
            enemy.Animator.CrossFadeInFixedTime(animation, 0.25f);

            rangeEnemy.EnableRig();
            rangeEnemy.SetRigWeight(RangeEnemyRigType.HEAD, 1);
            rangeEnemy.SetRigWeight(RangeEnemyRigType.RIGHT_HAND, 1);

            coverCheckTimer = coverCheckInterval; // so it checks immediately on enter
        }

        public override void Update()
        {
            base.Update();

            if (TryChangeCoverWhenPlayerInSight())
                return;

            Debug.Log("Can't change cover");


            if (rangeEnemy.CanThrowGrenade())
            {
                statemachine.SwitchState(rangeEnemy.GrenadeThrowState);
                return;
            }

            Debug.Log("Can't throw grenade");

            if (!enemy.IsPlayerInAgressionRadius() && IsCoverTimeOver())
            {
                statemachine.SwitchState(rangeEnemy.AdvanceTowardsPlayerState);
                return;
            }

            Debug.Log("Can't advance towards player");

            enemy.FaceTarget(enemy.Player.position);

            if (bulletsShot >= bulletsPerBurst)
            {
                if (IsCooldownTimeOver())
                {
                    ResetWeapon();
                }

                return;
            }

            Debug.Log("SHOOT!");

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

        bool TryChangeCoverWhenPlayerInSight()
        {
            // --- Timer for expensive cover check ---
            coverCheckTimer -= Time.deltaTime;

            // Debug.Log("Cover Check Time: " + coverCheckTimer);
            if (coverCheckTimer <= 0f)
            {
                coverCheckTimer = coverCheckInterval;

                // Debug.Log("Can Use Cover: " + rangeEnemy.CanUseCover());
                // Debug.Log("Is Player In Clear Sight : " + rangeEnemy.IsPlayerInClearSight());


                if (rangeEnemy.IsPlayerInClearSight() && rangeEnemy.CanUseCover() && IsMinAdvanceTimeAfterStopOver())
                {
                    statemachine.SwitchState(rangeEnemy.RunToCoverState);
                    return true;
                }
            }

            return false;
        }

        bool IsCooldownTimeOver()
        {
            return Time.time > lastTimeFired + weaponCooldown;
        }

        bool IsMinAdvanceTimeAfterStopOver()
        {
            return Time.time > rangeEnemy.AdvanceTowardsPlayerState.LastTimeAdvanced + rangeEnemy.MinTimeAfterAdvanceStateAfterStopping;
        }

        bool IsCoverTimeOver()
        {
            return Time.time > rangeEnemy.MinTimeInCoverState + rangeEnemy.RunToCoverState.LastTimeTookCover;
        }
    }
}
