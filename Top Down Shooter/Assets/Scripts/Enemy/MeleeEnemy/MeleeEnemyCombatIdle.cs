using UnityEngine;

namespace TDS
{
    public class MeleeEnemyCombatIdle : EnemyState
    {
        MeleeEnemy meleeEnemy;
        public MeleeEnemyCombatIdle(Enemy enemy, EnemyStatemachine statemachine, string animation) : base(enemy, statemachine, animation)
        {
            meleeEnemy = enemy as MeleeEnemy;
        }

        public override void Enter()
        {
            base.Enter();

            stateTimer = Random.Range(enemy.CombatIdleTime - 1f, enemy.CombatIdleTime + 1f);

            enemy.Animator.CrossFadeInFixedTime(animation, 0.25f);
        }

        public override void Update()
        {
            base.Update();

            bool isPlayerInAttackRadius = meleeEnemy.IsPlayerInAttackRadius();
            bool isPlayerInAgressionRadius = enemy.IsPlayerInAgressionRadius();

            if (!isPlayerInAttackRadius && isPlayerInAgressionRadius)
            {
                Debug.Log("Switching to chase state from idle state");
                statemachine.SwitchState(meleeEnemy.ChaseState);
                return;
            }

            if (stateTimer <= 0f)
            {
                statemachine.SwitchState(meleeEnemy.AttackState);
                return;
            }

            enemy.FaceTarget(enemy.Player.position);
        }
    }
}
