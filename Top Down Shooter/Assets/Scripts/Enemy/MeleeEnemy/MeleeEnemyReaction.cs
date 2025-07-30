using UnityEngine;

namespace TDS
{
    public class MeleeEnemyReaction : EnemyState
    {
        public MeleeEnemyReaction(Enemy enemy, EnemyStatemachine statemachine, string animation) : base(enemy, statemachine, animation)
        {
        }

        public override void Enter()
        {
            base.Enter();

            int randomInt = Random.Range(1, 5);

            enemy.Animator.SetFloat("randomInt", randomInt);
            enemy.Animator.CrossFadeInFixedTime(animation, 0.1f);

            enemy.Agent.isStopped = true;
        }

        public override void Exit()
        {
            base.Exit();
            enemy.Agent.isStopped = false;
        }

        public override void Update()
        {
            base.Update();

            enemy.FaceTarget(enemy.Player.position);

            if (enemy.Animator.HasAnimationEnded(animation, 0.9f))
            {
                statemachine.SwitchState((enemy as MeleeEnemy).ChaseState);
            }
        }
    }
}
