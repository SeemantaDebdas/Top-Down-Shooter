using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace TDS
{
    public enum RangeEnemyRigType { HEAD, RIGHT_HAND, LEFT_HAND }
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
        [SerializeField] MultiAimConstraint headRig, rightHandRig;
        [SerializeField] TwoBoneIKConstraint leftHandRig;

        [field: Header("Cover System")]
        [field: SerializeField] public CoverPoint LastCover { get; private set; }
        [field: SerializeField] public CoverPoint CurrentCover { get; private set; }
        // [field: SerializeField] public bool CanUseCover { get; private set; } = true;
        [field: SerializeField] public float RunSpeed { get; private set; } = 3.59f;
        [field: SerializeField] public float MinTimeInCoverState { get; private set; } = 3f;

        [field: Header("Advance State")]
        [field: SerializeField] public float AdvanceStopDistance { get; private set; } = 5f;
        [field: SerializeField] public float MinTimeAfterAdvanceStateAfterStopping { get; private set; } = 5f;

        public RangeEnemyIdle IdleState { get; private set; }
        public RangeEnemyMove MoveState { get; private set; }
        public RangeEnemyReaction ReactionState { get; private set; }
        public RangeEnemyBattle BattleState { get; private set; }
        public RangeEnemyRunToCover RunToCoverState { get; private set; }
        public RangeEnemyAdvanceTowardsPlayer AdvanceTowardsPlayerState { get; private set; }
        public RangeEnemyGrenadeThrow GrenadeThrowState { get; private set; }
        public RangeEnemyDead DeadState { get; private set; }

        [Header("Grenade State")]
        [SerializeField] bool canThrowGrenade = true;
        [SerializeField] GameObject grenadePrefab;
        [field: SerializeField] public float GrenadeThrowCooldownTime { get; private set; } = 5;
        [SerializeField] Transform grenadeLaunchTransform;
        float lastTimeThrownGrenade = -Mathf.Infinity;

        public EnemyRangeWeaponVisual WeaponVisual { get; private set; }


        protected override void Awake()
        {
            base.Awake();

            IdleState = new RangeEnemyIdle(this, statemachine, "Idle");
            MoveState = new RangeEnemyMove(this, statemachine, "Walk");
            ReactionState = new RangeEnemyReaction(this, statemachine, "Reaction");
            BattleState = new RangeEnemyBattle(this, statemachine, "Battle");
            RunToCoverState = new RangeEnemyRunToCover(this, statemachine, "Run");
            AdvanceTowardsPlayerState = new RangeEnemyAdvanceTowardsPlayer(this, statemachine, "Advance");
            GrenadeThrowState = new RangeEnemyGrenadeThrow(this, statemachine, "Throw Grenade");
            DeadState = new RangeEnemyDead(this, statemachine, "");
        }

        void Start()
        {
            WeaponVisual = GetComponent<EnemyRangeWeaponVisual>();

            InitializeRangeWeapon();
            statemachine.Initialize(IdleState);
            //DisableRig();
        }

        protected override void Update()
        {
            base.Update();
        }

        public void FireSingleBullet()
        {
            Animator.CrossFadeInFixedTime("Fire", 0.1f);

            Vector3 bulletDirection = Player.position + Vector3.up * 0.8f - bulletSpawnPoint.position;
            bulletDirection.Normalize();

            GameObject bulletObj = ObjectPool.Instance.GetObject(BulletPrefab, bulletSpawnPoint.position, Quaternion.LookRotation(bulletSpawnPoint.forward));
            EnemyBullet enemyBullet = bulletObj.GetComponent<EnemyBullet>();
            enemyBullet.Setup(100, bulletSpawnPoint.position);

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
            WeaponVisual.InitializeWeaponModel(WeaponType);
            bulletSpawnPoint = WeaponVisual.CurrentWeaponModel.gunPoint;
        }

        public void EnableRig()
        {
            DOVirtual.Float(0, 1, 0.25f, v => rig.weight = v);
        }

        public void DisableRig()
        {
            DOVirtual.Float(1, 0, 0.25f, v => rig.weight = v);
        }


        public void SetRigWeight(RangeEnemyRigType rigType, float weight)
        {
            switch (rigType)
            {
                case RangeEnemyRigType.HEAD:
                    DOVirtual.Float(headRig.weight, weight, 0.25f, v => headRig.weight = v);
                    break;
                case RangeEnemyRigType.RIGHT_HAND:
                    DOVirtual.Float(rightHandRig.weight, weight, 0.25f, v => rightHandRig.weight = v);
                    break;
                case RangeEnemyRigType.LEFT_HAND:
                    DOVirtual.Float(leftHandRig.weight, weight, 0.25f, v => leftHandRig.weight = v);
                    break;
            }
        }

        #region Cover State

        public List<Cover> GetAllCovers()
        {
            Collider[] coverColliders = Physics.OverlapSphere(transform.position, AgressionRadius);
            List<Cover> covers = new();

            foreach (Collider collider in coverColliders)
            {
                if (collider.TryGetComponent(out Cover cover) && !covers.Contains(cover))
                    covers.Add(cover);
            }

            return covers;
        }

        public bool CanUseCover()
        {
            CoverPoint coverPoint = GetCoverPoint();

            if (coverPoint != LastCover && coverPoint != null)
                return true;

            return false;
        }

        public void TakeCover()
        {
            if (CurrentCover != null)
                CurrentCover.SetOccupied(false);

            LastCover = CurrentCover;
            CurrentCover = GetCoverPoint();

            CurrentCover.SetOccupied(true);
        }

        CoverPoint GetCoverPoint()
        {
            List<Cover> allCovers = GetAllCovers();
            List<CoverPoint> coverPointList = new();

            foreach (Cover cover in allCovers)
            {
                coverPointList.AddRange(cover.GetValidCoverPoints(transform));
            }

            coverPointList.RemoveAll(cp => cp == LastCover);


            //get closest coverPoint
            CoverPoint closestCoverPoint = null;
            float shortestDistance = Mathf.Infinity;

            for (int i = 0; i < coverPointList.Count; i++)
            {
                float distance = Vector3.Distance(transform.position, coverPointList[i].transform.position);
                if (distance < shortestDistance)
                {
                    closestCoverPoint = coverPointList[i];
                    shortestDistance = distance;
                }
            }

            if (closestCoverPoint != null)
            {
                return closestCoverPoint;
            }

            return null;
        }

        #endregion

        public bool CanThrowGrenade()
        {
            if (!canThrowGrenade)
                return false;

            if (Time.time < lastTimeThrownGrenade + GrenadeThrowCooldownTime)
                return false;

            return true;
        }

        public void ThrowGrenade()
        {
            Debug.Log("Throwing Grenade!!!");

            GameObject grenadeSpawn = ObjectPool.Instance.GetObject(grenadePrefab, grenadeLaunchTransform.position);
            // grenadeSpawn.transform.position = grenadeLaunchTransform.position;

            EnemyGrenade enemyGrenade = grenadeSpawn.GetComponent<EnemyGrenade>();
            enemyGrenade.SetupGrenade(Player.position, 1f, 0.5f);

            lastTimeThrownGrenade = Time.time;
        }

        public bool IsPlayerInClearSight()
        {
            Vector3 direction = (Player.position - transform.position).normalized;
            if (Physics.Raycast(transform.position + Vector3.up * 0.5f, direction, out RaycastHit hitInfo))
            {
                if (hitInfo.collider.transform.CompareTag("Player"))
                    return true;
            }

            return false;
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
    }
}
