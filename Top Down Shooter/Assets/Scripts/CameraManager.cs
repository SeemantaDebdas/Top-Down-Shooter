using Unity.Cinemachine;
using UnityEngine;
namespace TDS
{

    public class CameraManager : MonoBehaviour
    {
        public static CameraManager Instance;
        private new CinemachineCamera camera;
        private CinemachinePositionComposer positionComposer;

        private float targetCameraDistance;
        private float distanceChangeRate = 1f;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Debug.LogError("There is more than one CameraManager in the scene!");
                Destroy(gameObject);
            }
            camera = GetComponentInChildren<CinemachineCamera>();
            positionComposer = camera.GetComponent<CinemachinePositionComposer>();
        }

        private void Update()
        {
            UpdateCameraDistance();
        }

        private void UpdateCameraDistance()
        {
            float currentCameraDistance = positionComposer.CameraDistance;
            float cameraTreshold = 0.1f;

            if (Mathf.Abs(targetCameraDistance - currentCameraDistance) < cameraTreshold) return;

            positionComposer.CameraDistance =
                Mathf.Lerp(currentCameraDistance, targetCameraDistance, Time.deltaTime * distanceChangeRate);

        }

        public void ChangeCameraDistance(float distance) => targetCameraDistance = distance;

    }
}
