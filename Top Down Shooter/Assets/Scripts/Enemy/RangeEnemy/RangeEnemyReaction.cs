using UnityEngine;

namespace TDS
{
    public class RangeEnemyReaction : RangeEnemyState
    {
        public RangeEnemyReaction(Enemy enemy, EnemyStatemachine statemachine, string animation) : base(enemy, statemachine, animation)
        {
        }

        public override void Enter()
        {
            base.Enter();

            int randomInt = Random.Range(1, 5);

            enemy.Animator.SetFloat("randomInt", randomInt);
            enemy.Animator.CrossFadeInFixedTime(animation, 0.1f);

            enemy.Agent.ResetPath();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();

            enemy.FaceTarget(enemy.Player.position);

            if (enemy.Animator.HasAnimationEnded(animation, 0.9f))
            {
                statemachine.SwitchState(rangeEnemy.BattleState);
            }
        }
    }
}
