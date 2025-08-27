using System.Collections.Generic;
using UnityEngine;

namespace TDS
{
    public class Cover : MonoBehaviour
    {
        [SerializeField] CoverPoint coverPointPrefab;
        [SerializeField] Vector3 offset;
        [field: SerializeField] public List<CoverPoint> CoverPoints { get; private set; } = new();

        Transform player;

        void Awake()
        {
            if (CoverPoints.Count == 0)
                GenerateCoverPoints();
        }

        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        public List<CoverPoint> GetValidCoverPoints(Transform enemyTransform)
        {
            List<CoverPoint> validCoverPoints = new();

            foreach (CoverPoint coverPoint in CoverPoints)
            {
                if (IsCoverPointValid(coverPoint, enemyTransform))
                    validCoverPoints.Add(coverPoint);
            }

            return validCoverPoints;
        }

        bool IsCoverPointValid(CoverPoint coverPoint, Transform enemyTransform)
        {
            if (coverPoint.IsOccupied) { Debug.Log($"{coverPoint.name} rejected: occupied", coverPoint.gameObject); return false; }
            if (!IsCoverCloserOrAwayFromPlayer(coverPoint, enemyTransform)) { return false; }
            if (IsCoverPointCloseToPlayer(coverPoint)) { Debug.Log($"{coverPoint.name} rejected: too close to player", coverPoint.gameObject); return false; }
            if (!IsFurthestFromThePlayer(coverPoint)) { Debug.Log($"{coverPoint.name} rejected: not furthest", coverPoint.gameObject); return false; }

            Debug.Log($"{coverPoint.name} VALID", coverPoint.gameObject);
            return true;
        }


        bool IsCoverCloserOrAwayFromPlayer(CoverPoint coverPoint, Transform enemyTransform)
        {
            float distanceToCoverPoint = Vector3.Distance(coverPoint.transform.position, enemyTransform.position);
            float distanceToPlayer = Vector3.Distance(player.position, enemyTransform.position);

            bool coverCloserThanPlayer = distanceToCoverPoint < distanceToPlayer;

            if (coverCloserThanPlayer)
                return true;

            Vector3 dirToCoverPoint = (coverPoint.transform.position - enemyTransform.position).normalized;
            Vector3 dirToPlayer = (player.position - enemyTransform.position).normalized;

            bool coverTowardsPlayer = Vector3.Dot(dirToCoverPoint, dirToPlayer) > 0;

            return !coverTowardsPlayer;
        }

        bool IsCoverPointCloseToPlayer(CoverPoint coverPoint, float distanceThreshold = 2f)
        {
            return Vector3.Distance(coverPoint.transform.position, player.position) < distanceThreshold;
        }

        bool IsFurthestFromThePlayer(CoverPoint coverPoint)
        {
            CoverPoint furthestPoint = null;
            float furthestDistance = -Mathf.Infinity;

            foreach (CoverPoint point in CoverPoints)
            {
                float distanceToPlayer = Vector3.Distance(point.transform.position, player.position);
                if (distanceToPlayer > furthestDistance)
                {
                    furthestPoint = point;
                    furthestDistance = distanceToPlayer;
                }
            }

            return furthestPoint == coverPoint;
        }

        [ContextMenu("Generate Cover Points")]
        void GenerateCoverPoints()
        {
            DestroyExistingCoverPoints();

            MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
            if (meshRenderer == null)
            {
                Debug.LogWarning("No MeshRenderer found on this object.");
                return;
            }

            // Get local-space bounds
            Bounds localBounds = meshRenderer.localBounds;

            // Offsets from the center in local space
            Vector3[] localOffsets =
            {
                new(0f, 0f, localBounds.extents.z + offset.z),    // front
                new(0f, 0f, -(localBounds.extents.z + offset.z)), // back
                new(localBounds.extents.x + offset.x, 0f, 0f),    // right
                new(-(localBounds.extents.x + offset.x), 0f, 0f)  // left
            };

            foreach (Vector3 localOffset in localOffsets)
            {
                Vector3 worldPos = transform.TransformPoint(localBounds.center + localOffset);
                CoverPoint coverPoint = Instantiate(coverPointPrefab, worldPos, transform.rotation, transform);
                CoverPoints.Add(coverPoint);
            }
        }

        void DestroyExistingCoverPoints()
        {
            for (int i = CoverPoints.Count - 1; i >= 0; i--)
            {
                if (CoverPoints[i] != null)
                    DestroyImmediate(CoverPoints[i].gameObject);
            }

            CoverPoints.Clear();
        }

    }
}
