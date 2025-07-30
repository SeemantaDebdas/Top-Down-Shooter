using System.Collections.Generic;
using UnityEngine;

namespace TDS
{
    public class MeleeEnemy : Enemy
    {
        [field: Header("Melee Settings")]
        [SerializeField] float minDistanceToThrowMeleeWeapon = 3f;
        [field: SerializeField] public int AttackAnimationLayer { get; private set; } = 3;
        [field: SerializeField] public List<AttackData> AttackList { get; private set; }

        [field: Header("Melee Weapon")]
        [SerializeField] Transform weaponHolder;
        [SerializeField] AnimatorOverrideController weaponAnimatorOverrideController;
        [field: SerializeField] public Transform MeleeWeaponSpawnPoint { get; private set; }
        [field: SerializeField] public GameObject ThrownWeaponPrefab { get; private set; }
        [SerializeField] List<ThrownWeapon> thrownWeaponPrefabList;

        public AttackData CurrentAttack { get; private set; }

        public MeleeEnemyIdle IdleState { get; private set; }
        public MeleeEnemyMove MoveState { get; private set; }
        public MeleeEnemyChase ChaseState { get; private set; }
        public MeleeEnemyAttack AttackState { get; private set; }
        public MeleeEnemyCombatIdle CombatIdleState { get; private set; }
        public MeleeEnemyReaction ReactionState { get; private set; }
        public MeleeEnemyDead DeadState { get; private set; }
        public MeleeEnemyDodge DodgeState { get; private set; }
        public MeleeEnemyAbility AbilityState { get; private set; }

        Shield shield;
        float lastTimeThrownWeapon = -Mathf.Infinity, axeThrowRate = 5f;

        protected override void Awake()
        {
            base.Awake();

            IdleState = new MeleeEnemyIdle(this, statemachine, "Idle");
            MoveState = new MeleeEnemyMove(this, statemachine, "Move");
            ChaseState = new MeleeEnemyChase(this, statemachine, "Run");
            AttackState = new MeleeEnemyAttack(this, statemachine, "Attack");
            CombatIdleState = new MeleeEnemyCombatIdle(this, statemachine, "CombatIdle");
            ReactionState = new MeleeEnemyReaction(this, statemachine, "Reaction");
            DeadState = new MeleeEnemyDead(this, statemachine, string.Empty);
            DodgeState = new MeleeEnemyDodge(this, statemachine, "Dodge");
            AbilityState = new MeleeEnemyAbility(this, statemachine, "AxeThrow");

            CurrentAttack = AttackList[0];

            statemachine.Initialize(IdleState);

            shield = GetComponentInChildren<Shield>();

            InitiateMeleeWeapon();
            UpdateAnimator();
        }

        protected virtual void UpdateAnimator()
        {
            if (ThrownWeaponPrefab == null)
                return;

            Animator.runtimeAnimatorController = weaponAnimatorOverrideController;
            Animator.SetLayerWeight(1, 1);
        }

        protected override void Update()
        {
            base.Update();

            statemachine.Update();
        }

        public bool IsPlayerInAttackRadius() => Vector3.Distance(transform.position, Player.position) <= CurrentAttack.Range;
        public bool IsPlayerClose() => Vector3.Distance(transform.position, Player.position) <= 1.5f;

        void InitiateMeleeWeapon()
        {
            if (thrownWeaponPrefabList.Count == 0)
                return;

            ThrownWeapon randomThrownWeapon = thrownWeaponPrefabList[Random.Range(0, thrownWeaponPrefabList.Count)];
            ThrownWeaponPrefab = randomThrownWeapon.gameObject;

            Instantiate(randomThrownWeapon.WeaponVisual, weaponHolder);
        }

        public AttackData UpdateCurrentAttack()
        {
            List<AttackData> attacks = new(AttackList);
            if (IsPlayerClose())
            {
                attacks.RemoveAll(attack => attack.Type == AttackType.Charge);
            }
            else
            {
                attacks.RemoveAll(attack => attack.Type == AttackType.Close);
            }

            CurrentAttack = attacks[Random.Range(0, attacks.Count)];
            return CurrentAttack;
        }

        public override void GetHit()
        {
            base.GetHit();

            if (healthPoints > 0)
                return;

            if (statemachine.CurrentState.GetType() == typeof(MeleeEnemyDead))
                return;

            statemachine.SwitchState(DeadState);
        }

        public bool TryGetShield(out Shield shield)
        {
            shield = this.shield;
            return shield != null && !shield.Equals(null);
        }

        public override bool CanUseAbility()
        {
            if (ThrownWeaponPrefab == null)
                return false;

            if (Time.time < lastTimeThrownWeapon + axeThrowRate)
                return false;

            float distanceToPlayer = Vector3.Distance(Player.transform.position, transform.position);

            if (distanceToPlayer < minDistanceToThrowMeleeWeapon)
                return false;

            lastTimeThrownWeapon = Time.time;

            return true;
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, AgressionRadius);

            if (CurrentAttack == null)
                return;
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, CurrentAttack.Range);
        }

    }
}
