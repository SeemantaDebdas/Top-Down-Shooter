using UnityEngine;

namespace TDS
{
    public class RangeEnemyAdvanceTowardsPlayer : RangeEnemyState
    {
        public float LastTimeAdvanced { get; private set; }
        public RangeEnemyAdvanceTowardsPlayer(Enemy enemy, EnemyStatemachine statemachine, string animation) : base(enemy, statemachine, animation)
        {
        }

        public override void Enter()
        {
            base.Enter();

            enemy.Animator.CrossFadeInFixedTime(animation, 0.1f);
        }

        public override void Exit()
        {
            base.Exit();

            LastTimeAdvanced = Time.time;
        }

        public override void Update()
        {
            base.Update();

            enemy.Agent.SetDestination(enemy.Player.position);
            enemy.FaceTarget(enemy.Agent.steeringTarget);

            if (Vector3.Distance(enemy.Player.position, enemy.transform.position) < rangeEnemy.AdvanceStopDistance)
            {
                statemachine.SwitchState(rangeEnemy.BattleState);
                return;
            }
        }
    }
}
