using UnityEngine;

namespace TDS
{
    public class CoverPoint : MonoBehaviour
    {
        public bool IsOccupied { get; private set; } = false;

        [SerializeField] private Material freeMaterial;
        [SerializeField] private Material occupiedMaterial;

        private Renderer rend;

        void Awake()
        {
            rend = GetComponentInChildren<Renderer>();
            UpdateMaterial();
        }

        public bool SetOccupied(bool state)
        {
            IsOccupied = state;
            UpdateMaterial();
            return IsOccupied;
        }

        void UpdateMaterial()
        {
            if (rend == null) return;

            if (IsOccupied && occupiedMaterial != null)
                rend.material = occupiedMaterial;
            else if (!IsOccupied && freeMaterial != null)
                rend.material = freeMaterial;
        }
    }
}
