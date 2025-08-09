using TDS;
using UnityEngine;

public class RangeEnemyState : EnemyState
{

    protected RangeEnemy rangeEnemy;
    public RangeEnemyState(Enemy enemy, EnemyStatemachine statemachine, string animation) : base(enemy, statemachine, animation)
    {
        rangeEnemy = enemy as RangeEnemy;
    }
}