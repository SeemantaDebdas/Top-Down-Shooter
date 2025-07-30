using UnityEngine;

namespace TDS
{
    public class MeleeEnemyAbility : EnemyState
    {
        bool abilityTriggered = false;
        public MeleeEnemyAbility(Enemy enemy, EnemyStatemachine statemachine, string animation) : base(enemy, statemachine, animation)
        {
        }

        public override void Enter()
        {
            base.Enter();

            abilityTriggered = false;
            enemy.Animator.CrossFadeInFixedTime(animation, 0.1f, meleeEnemy.AttackAnimationLayer);
            enemy.Agent.speed = 1.15f;
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();

            bool isInState = enemy.Animator.IsInState(animation, meleeEnemy.AttackAnimationLayer);
            float normalizedTime = enemy.Animator.GetNormalizedTime(meleeEnemy.AttackAnimationLayer);

            if (enemy.Animator.HasAnimationEnded(animation, 0.9f, meleeEnemy.AttackAnimationLayer))
            {
                statemachine.SwitchState(meleeEnemy.ReactionState);
                return;
            }

            if (!abilityTriggered)
            {
                if (isInState && normalizedTime > 0.57f)
                {
                    abilityTriggered = true;
                    GameObject meleeWeaponSpawn = ObjectPool.Instance.GetObject(meleeEnemy.ThrownWeaponPrefab);
                    meleeWeaponSpawn.GetComponent<ThrownWeapon>().Setup(meleeEnemy.MeleeWeaponSpawnPoint.position, enemy.Player);
                }
            }


            enemy.Agent.SetDestination(enemy.Player.position);

            if (isInState && normalizedTime < 0.2)
                enemy.FaceTarget(enemy.Player.position);
        }
    }
}
