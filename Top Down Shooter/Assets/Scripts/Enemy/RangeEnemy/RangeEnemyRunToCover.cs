using UnityEngine;

namespace TDS
{
    public class RangeEnemyRunToCover : RangeEnemyState
    {
        Vector3 destination;
        public float LastTimeTookCover;
        public RangeEnemyRunToCover(Enemy enemy, EnemyStatemachine statemachine, string animation) : base(enemy, statemachine, animation)
        {
        }

        public override void Enter()
        {
            base.Enter();

            rangeEnemy.DisableRig();

            rangeEnemy.TakeCover();

            destination = rangeEnemy.CurrentCover.transform.position;
            // rangeEnemy.CurrentCover.SetOccupied(true);
            enemy.Animator.CrossFadeInFixedTime(animation, 0.1f);

            enemy.Agent.speed = rangeEnemy.RunSpeed;
        }

        public override void Exit()
        {
            base.Exit();

            // rangeEnemy.CurrentCover.SetOccupied(false);
        }

        public override void Update()
        {
            base.Update();

            float distance = Vector3.Distance(enemy.transform.position, destination);

            // Debug.Log(distance);

            if (distance < 0.2f)
            {
                statemachine.SwitchState(rangeEnemy.BattleState);
                return;
            }

            destination.y = enemy.transform.position.y;
            enemy.Agent.SetDestination(destination);
        }
    }
}
