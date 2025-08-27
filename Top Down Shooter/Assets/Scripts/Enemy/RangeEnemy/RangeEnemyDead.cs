using UnityEngine;

namespace TDS
{
    public class RangeEnemyDead : RangeEnemyState
    {
        public RangeEnemyDead(Enemy enemy, EnemyStatemachine statemachine, string animation) : base(enemy, statemachine, animation)
        {
        }

        public override void Enter()
        {
            base.Enter();

            enemy.Animator.enabled = false;
            enemy.Agent.enabled = false;
            enemy.RagdollController.ActivateRagdoll();
        }
    }
}
