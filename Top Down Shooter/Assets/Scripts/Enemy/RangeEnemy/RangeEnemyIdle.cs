using UnityEngine;

namespace TDS
{
    public class RangeEnemyIdle : RangeEnemyState
    {
        public RangeEnemyIdle(Enemy enemy, EnemyStatemachine statemachine, string animation) : base(enemy, statemachine, animation)
        {
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
                statemachine.SwitchState(rangeEnemy.ReactionState);
                return;
            }

            if (stateTimer < 0)
                statemachine.SwitchState(rangeEnemy.MoveState);
        }
    }
}
