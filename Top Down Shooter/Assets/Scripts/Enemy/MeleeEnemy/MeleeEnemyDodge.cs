using UnityEngine;

namespace TDS
{
    public class MeleeEnemyDodge : EnemyState
    {
        public MeleeEnemyDodge(Enemy enemy, EnemyStatemachine statemachine, string animation) : base(enemy, statemachine, animation)
        {
        }

        public override void Enter()
        {
            base.Enter();

            enemy.Animator.CrossFadeInFixedTime(animation, 0.1f);

            enemy.Agent.speed = enemy.DodgeSpeed;
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();

            if (enemy.Animator.HasAnimationEnded(animation, 0.9f))
            {
                statemachine.SwitchState(meleeEnemy.ChaseState);
            }
        }
    }
}
