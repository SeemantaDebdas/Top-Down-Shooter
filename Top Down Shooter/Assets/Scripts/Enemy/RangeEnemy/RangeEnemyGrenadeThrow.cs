using UnityEngine;

namespace TDS
{
    public class RangeEnemyGrenadeThrow : RangeEnemyState
    {
        bool thrownGrenade = false;

        public RangeEnemyGrenadeThrow(Enemy enemy, EnemyStatemachine statemachine, string animation) : base(enemy, statemachine, animation)
        {
        }

        public override void Enter()
        {
            base.Enter();

            enemy.Animator.CrossFadeInFixedTime(animation, 0.1f);
            thrownGrenade = false;
        }

        public override void Update()
        {
            base.Update();

            enemy.FaceTarget(enemy.Player.position);

            bool isInState = enemy.Animator.IsInState(animation);
            float normalizedTime = enemy.Animator.GetNormalizedTime();

            if (enemy.Animator.HasAnimationEnded(animation, 0.9f))
            {
                statemachine.SwitchState(rangeEnemy.BattleState);
                return;
            }

            if (!thrownGrenade)
            {
                if (isInState && normalizedTime > 0.53f)
                {
                    thrownGrenade = true;
                    rangeEnemy.ThrowGrenade();
                }
            }
        }
    }
}
