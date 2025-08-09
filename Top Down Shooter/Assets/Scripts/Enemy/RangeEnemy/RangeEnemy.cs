using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace TDS
{
    public class RangeEnemy : Enemy
    {
        const float REFERENCE_BULLET_SPEED = 20; //for setting the speed to mass ratio. for mass = 1, speed = 20

        [field: Header("Weapon Settings")]
        [field: SerializeField] public WeaponType WeaponType { get; private set; }
        [field: SerializeField] public RangeEnemyWeaponData CurrentWeaponData { get; private set; }
        [SerializeField] Transform weaponHandler;
        [SerializeField] List<RangeEnemyWeaponData> weaponDataList;

        [field: Header("Bullet Settings")]
        [field: SerializeField] public GameObject BulletPrefab { get; private set; }
        [SerializeField] Transform bulletSpawnPoint;

        [Header("Rig Settings")]
        [SerializeField] Rig rig;

        public RangeEnemyIdle IdleState { get; private set; }
        public RangeEnemyMove MoveState { get; private set; }
        public RangeEnemyReaction ReactionState { get; private set; }
        public RangeEnemyBattle BattleState { get; private set; }

        EnemyRangeWeaponVisual weaponVisual;


        protected override void Awake()
        {
            base.Awake();

            IdleState = new RangeEnemyIdle(this, statemachine, "Idle");
            MoveState = new RangeEnemyMove(this, statemachine, "Move");
            ReactionState = new RangeEnemyReaction(this, statemachine, "Reaction");
            BattleState = new RangeEnemyBattle(this, statemachine, "Battle");

            weaponVisual = GetComponent<EnemyRangeWeaponVisual>();

            InitializeRangeWeapon();
            statemachine.Initialize(IdleState);
            DisableRig();
        }

        protected override void Update()
        {
            base.Update();
        }

        public void FireSingleBullet()
        {
            Animator.SetTrigger("fire");

            Vector3 bulletDirection = Player.position + Vector3.up * 0.8f - bulletSpawnPoint.position;
            bulletDirection.Normalize();

            GameObject bulletObj = ObjectPool.Instance.GetObject(BulletPrefab);
            EnemyBullet enemyBullet = bulletObj.GetComponent<EnemyBullet>();
            enemyBullet.Setup(100, bulletSpawnPoint.position, Quaternion.LookRotation(bulletSpawnPoint.forward));

            Rigidbody bulletRb = bulletObj.GetComponent<Rigidbody>();
            bulletRb.mass = REFERENCE_BULLET_SPEED / CurrentWeaponData.BulletSpeed;

            bulletRb.linearVelocity = CurrentWeaponData.ApplySpread(bulletDirection) * CurrentWeaponData.BulletSpeed;
        }

        void InitializeRangeWeapon()
        {
            //setup weapon data
            for (int i = 0; i < weaponDataList.Count; i++)
            {
                if (weaponDataList[i].WeaponType == WeaponType)
                    CurrentWeaponData = weaponDataList[i];
            }

            //setup weapon model
            weaponVisual.InitializeWeaponModel(WeaponType);
            bulletSpawnPoint = weaponVisual.CurrentWeaponModel.gunPoint;
        }

        public void EnableRig()
        {
            DOVirtual.Float(0, 1, 0.25f, v => rig.weight = v);
        }

        public void DisableRig()
        {
            DOVirtual.Float(1, 0, 0.25f, v => rig.weight = v);
        }
    }
}
