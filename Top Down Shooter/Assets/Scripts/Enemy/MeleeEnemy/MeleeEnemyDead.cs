using UnityEngine;

namespace TDS
{
    public class MeleeEnemyDead : EnemyState
    {
        public MeleeEnemyDead(Enemy enemy, EnemyStatemachine statemachine, string animation) : base(enemy, statemachine, animation)
        {
        }

        public override void Enter()
        {
            base.Enter();

            enemy.Animator.enabled = false;
            enemy.Agent.enabled = false;
            enemy.RagdollController.ActivateRagdoll();
            Debug.Log("Called activate ragdoll");
        }
    }
}
