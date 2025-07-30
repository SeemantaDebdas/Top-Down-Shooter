using UnityEngine;

namespace TDS
{
    public class EnemyState
    {
        protected Enemy enemy;
        protected MeleeEnemy meleeEnemy;
        protected EnemyStatemachine statemachine;
        protected string animation;

        protected float stateTimer;

        public EnemyState(Enemy enemy, EnemyStatemachine statemachine, string animation)
        {
            this.enemy = enemy;
            this.meleeEnemy = enemy as MeleeEnemy;
            this.statemachine = statemachine;
            this.animation = animation;
        }

        public virtual void Enter()
        {
        }

        public virtual void Exit()
        {

        }
        public virtual void Update()
        {
            stateTimer -= Time.deltaTime;
        }
    }
}
