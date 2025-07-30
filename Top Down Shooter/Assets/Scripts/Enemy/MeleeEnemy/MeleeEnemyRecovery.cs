using UnityEngine;

namespace TDS
{
    public class MeleeEnemyRecovery : EnemyState
    {
        public MeleeEnemyRecovery(Enemy enemy, EnemyStatemachine statemachine, string animation) : base(enemy, statemachine, animation)
        {
        }

        public override void Enter()
        {
            base.Enter();
            enemy.Animator.CrossFadeInFixedTime(animation, 0.25f);

            enemy.Agent.isStopped = true;
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();

            enemy.FaceTarget(enemy.Player.position);

            if (enemy.Animator.HasAnimationEnded(animation, 0.9f))
            {
                statemachine.SwitchState((enemy as MeleeEnemy).ChaseState);
                return;
            }
        }
    }
}
