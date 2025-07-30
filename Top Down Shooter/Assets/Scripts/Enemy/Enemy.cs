using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace TDS
{
    public class Enemy : MonoBehaviour
    {
        [field: SerializeField] protected int healthPoints { get; private set; } = 1;

        [field: Header("State Settings")]
        [field: SerializeField] public float IdleTime { get; private set; }
        [field: SerializeField] public float CombatIdleTime { get; private set; }
        [field: SerializeField] public float WalkSpeed { get; private set; }
        [field: SerializeField] public float ChaseSpeed { get; private set; }
        [field: SerializeField] public float DodgeSpeed { get; private set; }
        [field: SerializeField] public float RotationSpeed { get; private set; }

        [field: Header("Behaviour Settings")]
        [field: SerializeField] public float AgressionRadius { get; private set; }
        [SerializeField] List<Transform> waypointList;

        int currentWaypointIndex = -1;

        public NavMeshAgent Agent { get; private set; }
        public Animator Animator { get; private set; }
        public RagdollController RagdollController { get; private set; }
        protected EnemyStatemachine statemachine;

        public Transform Player { get; private set; }

        float lastTimeDodged, dodgeRate = 2f;
        public event Action OnDodgeTriggered;

        protected virtual void Awake()
        {
            statemachine = new EnemyStatemachine();

            Agent = GetComponent<NavMeshAgent>();
            Animator = GetComponent<Animator>();
            RagdollController = GetComponent<RagdollController>();

            InitializeWaypoints();

            Player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        protected virtual void Update()
        {
            statemachine.Update();
        }

        public Transform GetCurrentWaypoint()
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypointList.Count;
            Transform waypoint = waypointList[currentWaypointIndex];

            return waypoint;
        }

        public void FaceTarget(Vector3 target)
        {
            Vector3 dirToTarget = target - transform.position;
            if (dirToTarget == Vector3.zero)
                dirToTarget = transform.forward;

            Quaternion targetRotation = Quaternion.LookRotation(dirToTarget);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, RotationSpeed * Time.deltaTime);
        }

        void InitializeWaypoints()
        {
            foreach (Transform waypoint in waypointList)
                waypoint.parent = null;
        }

        public bool IsPlayerInAgressionRadius() => Vector3.Distance(transform.position, Player.position) <= AgressionRadius;

        public virtual void GetHit()
        {
            healthPoints--;
        }

        public virtual void AddImpactForceAfterDeath(Rigidbody rb, Vector3 force, Vector3 position)
        {
            StartCoroutine(AddImpactForceAfterDeathAfterDelay(rb, force, position));
        }

        IEnumerator AddImpactForceAfterDeathAfterDelay(Rigidbody rb, Vector3 force, Vector3 position)
        {
            yield return new WaitForEndOfFrame();

            rb.AddForceAtPosition(force * 2.5f, position, ForceMode.Impulse);
        }

        public void TryTriggerDodge()
        {
            if (Time.time < lastTimeDodged + dodgeRate)
                return;

            if (Vector3.Distance(Player.position, transform.position) < 2f)
                return;

            lastTimeDodged = Time.time;
            OnDodgeTriggered?.Invoke();
        }

        public virtual bool CanUseAbility()
        {
            return true;
        }
    }
}
