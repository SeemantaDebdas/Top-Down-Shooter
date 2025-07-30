using UnityEngine;

namespace TDS
{
    public class MeleeEnemyAttack : EnemyState
    {
        Vector3 attackEndPosition;
        public MeleeEnemyAttack(Enemy enemy, EnemyStatemachine statemachine, string animation) : base(enemy, statemachine, animation)
        {
            meleeEnemy = enemy as MeleeEnemy;
        }

        public override void Enter()
        {
            base.Enter();
            meleeEnemy.UpdateCurrentAttack();

            animation = meleeEnemy.CurrentAttack.Animation;

            enemy.Animator.CrossFadeInFixedTime(animation, 0.05f, meleeEnemy.AttackAnimationLayer);

            attackEndPosition = enemy.transform.position + enemy.transform.forward * meleeEnemy.CurrentAttack.Force;
            enemy.Agent.ResetPath();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();

            if (enemy.Animator.HasAnimationEnded(animation, 0.9f, meleeEnemy.AttackAnimationLayer))
            {
                bool isPlayerInAgressionRadius = meleeEnemy.IsPlayerInAgressionRadius();
                bool isPlayerInAttackRadius = meleeEnemy.IsPlayerInAttackRadius();

                if (isPlayerInAttackRadius)
                {
                    statemachine.SwitchState(meleeEnemy.CombatIdleState);
                    return;
                }

                if (!isPlayerInAttackRadius && isPlayerInAgressionRadius)
                {
                    statemachine.SwitchState(meleeEnemy.ChaseState);
                    return;
                }
            }

            //Add force towards player
            if (Vector3.Distance(enemy.Player.position, enemy.transform.position) > 1f)
                enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, attackEndPosition, Time.deltaTime * 3.5f);

            //Add rotation when attacking
            if (meleeEnemy.CurrentAttack.Type == AttackType.Close)
            {
                meleeEnemy.FaceTarget(meleeEnemy.Player.position);
            }
        }
    }
}
