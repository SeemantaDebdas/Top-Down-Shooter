using TDS.Input;
using UnityEngine;

namespace TDS
{
    public class PlayerAim : MonoBehaviour
    {
        [SerializeField] InputSO input;

        [Header("Camera Settings")]
        [SerializeField] Transform cameraVisual;
        [SerializeField] float cameraSensitivity;
        [SerializeField] float maxDistance;
        [SerializeField] float minDistance;

        [Header("Aim Settings")]
        [SerializeField] LayerMask aimLayerMask;
        [field: SerializeField] public Transform AimVisual { get; private set; }
        [SerializeField] LineRenderer aimLaserLineRenderer;

        [field: SerializeField] public bool AimPrecisely { get; private set; } = false;
        [field: SerializeField] public bool LockToTarget { get; private set; } = false;

        Vector2 aimInput;
        RaycastHit lastKnowHit;
        PlayerWeaponController weaponController;
        PlayerWeaponVisuals weaponVisuals;

        void OnEnable()
        {
            input.OnAimPerformed += Input_OnAimPerformed;
        }

        void OnDisable()
        {
            input.OnAimPerformed -= Input_OnAimPerformed;
        }

        void Awake()
        {
            weaponController = GetComponent<PlayerWeaponController>();
            weaponVisuals = GetComponent<PlayerWeaponVisuals>();
        }

        void Input_OnAimPerformed(Vector2 aimInput)
        {
            this.aimInput = aimInput;
        }

        void Update()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.P))
                AimPrecisely = !AimPrecisely;


            UpdateCameraPosition();
            UpdateAimPosition();
            UpdateAimLaser();
        }

        private void UpdateAimPosition()
        {
            if (TryGetTargetAtMousePosition(out Target target))
            {
                if (target.TryGetComponent(out Renderer renderer))
                    AimVisual.position = renderer.bounds.center;
                else
                    AimVisual.position = target.transform.position;

                return;
            }

            AimVisual.position = GetRaycastHitInfo().point;

            if (!AimPrecisely)
                AimVisual.position = new Vector3(AimVisual.position.x, transform.position.y + 1, AimVisual.position.z);
        }

        private void UpdateCameraPosition()
        {
            Vector3 desiredCameraPosition = GetDesiredCameraPosition();

            cameraVisual.position = Vector3.Lerp(cameraVisual.position, desiredCameraPosition, cameraSensitivity * Time.deltaTime);
        }

        void UpdateAimLaser()
        {
            aimLaserLineRenderer.enabled = weaponController.WeaponReady;

            if (!weaponController.WeaponReady)
                return;


            WeaponModel currentWeaponModel = weaponVisuals.GetCurrentWeaponModel();
            currentWeaponModel.transform.LookAt(AimVisual);
            currentWeaponModel.gunPoint.LookAt(AimVisual);

            Vector3 spawnPoint = weaponController.GetBulletSpawnPoint().position;
            aimLaserLineRenderer.SetPosition(0, spawnPoint);

            float weaponRange = weaponController.CurrentWeapon.weaponRange;
            Vector3 laserDirection = weaponController.GetBulletDirection();
            Vector3 endPoint = spawnPoint + laserDirection * weaponRange;
            aimLaserLineRenderer.SetPosition(1, endPoint);

            if (Physics.Raycast(spawnPoint, laserDirection, out RaycastHit hitInfo, weaponRange))
            {
                aimLaserLineRenderer.SetPosition(2, endPoint);
            }

            aimLaserLineRenderer.SetPosition(2, endPoint + laserDirection * 0.1f);
        }

        public bool TryGetTargetAtMousePosition(out Target target)
        {
            return GetRaycastHitInfo().collider.TryGetComponent(out target);
        }

        public RaycastHit GetRaycastHitInfo()
        {
            Ray ray = Camera.main.ScreenPointToRay(aimInput);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, aimLayerMask))
            {
                lastKnowHit = hitInfo;
            }

            return lastKnowHit;
        }

        Vector3 GetDesiredCameraPosition()
        {
            Vector3 desiredCameraPosition = GetRaycastHitInfo().point;

            float distanceFromPlayer = Vector3.Distance(transform.position, desiredCameraPosition);

            Vector3 aimDirection = (desiredCameraPosition - transform.position).normalized;

            float clampedDistance = Mathf.Clamp(distanceFromPlayer, minDistance, maxDistance);

            desiredCameraPosition = transform.position + aimDirection * clampedDistance;
            desiredCameraPosition.y = transform.position.y + 1;

            return desiredCameraPosition;
        }
    }
}
