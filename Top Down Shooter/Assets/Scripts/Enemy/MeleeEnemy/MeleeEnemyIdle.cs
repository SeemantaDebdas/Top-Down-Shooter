using UnityEngine;

namespace TDS
{
    public class MeleeEnemyIdle : EnemyState
    {
        public MeleeEnemyIdle(Enemy enemy, EnemyStatemachine statemachine, string animation) : base(enemy, statemachine, animation)
        {
            this.animation = animation;
        }

        public override void Enter()
        {
            base.Enter();

            stateTimer = enemy.IdleTime;

            enemy.Animator.CrossFadeInFixedTime(animation, 0.25f);
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();

            if (enemy.IsPlayerInAgressionRadius())
            {
                statemachine.SwitchState((enemy as MeleeEnemy).ReactionState);
                return;
            }

            if (stateTimer < 0)
                statemachine.SwitchState((enemy as MeleeEnemy).MoveState);
        }
    }
}
