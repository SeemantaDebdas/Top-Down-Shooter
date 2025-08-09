using UnityEngine;

namespace TDS
{
    public class RangeEnemyMove : RangeEnemyState
    {
        Transform waypoint;

        public RangeEnemyMove(Enemy enemy, EnemyStatemachine statemachine, string animation) : base(enemy, statemachine, animation)
        {
        }

        public override void Enter()
        {
            base.Enter();
            waypoint = enemy.GetCurrentWaypoint();

            enemy.Agent.speed = enemy.WalkSpeed;
            enemy.Animator.SetFloat("speed", enemy.WalkSpeed);
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

            enemy.Agent.SetDestination(waypoint.position);

            enemy.FaceTarget(enemy.Agent.steeringTarget);

            float remainingDistance = Vector3.Distance(enemy.transform.position, waypoint.position);
            if (remainingDistance < 0.1f)
                statemachine.SwitchState(rangeEnemy.IdleState);
        }
    }
}
