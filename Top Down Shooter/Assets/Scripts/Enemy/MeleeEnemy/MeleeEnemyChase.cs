using UnityEngine;

namespace TDS
{
    public class MeleeEnemyChase : EnemyState
    {
        float lastTimeUpdated = 0;
        public MeleeEnemyChase(Enemy enemy, EnemyStatemachine statemachine, string animation) : base(enemy, statemachine, animation)
        {
            meleeEnemy = enemy as MeleeEnemy;
        }

        public override void Enter()
        {
            base.Enter();

            enemy.Agent.isStopped = false;

            enemy.Agent.speed = enemy.ChaseSpeed;
            animation = "Run";

            if (meleeEnemy.TryGetShield(out Shield shield))
            {
                enemy.Agent.speed = 2.78f; //shield run speed;
                animation = "ShieldRun";

                shield.OnDestroyed += () =>
                {
                    statemachine.SwitchState(meleeEnemy.ReactionState);
                };
            }

            // enemy.Animator.SetFloat("speed", enemy.ChaseSpeed);

            enemy.Animator.CrossFadeInFixedTime(animation, 0.25f);

            enemy.OnDodgeTriggered += () =>
            {
                statemachine.SwitchState(meleeEnemy.DodgeState);
            };
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();

            if (meleeEnemy.IsPlayerInAttackRadius())
            {
                statemachine.SwitchState(meleeEnemy.AttackState);
                return;
            }

            if (meleeEnemy.CanUseAbility())
            {
                statemachine.SwitchState(meleeEnemy.AbilityState);
                return;
            }

            if (CanUpdateDestination())
            {
                enemy.Agent.SetDestination(enemy.Player.position);
            }

            enemy.FaceTarget(enemy.Agent.steeringTarget);
        }

        bool CanUpdateDestination()
        {
            if (Time.time > lastTimeUpdated + 0.25f)
            {
                lastTimeUpdated = Time.time;
                return true;
            }

            return false;
        }
    }
}
