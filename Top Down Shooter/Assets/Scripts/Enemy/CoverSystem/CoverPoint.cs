using UnityEngine;

namespace TDS
{
    public class CoverPoint : MonoBehaviour
    {
        public bool IsOccupied { get; private set; } = false;

        public bool SetOccupied(bool state)
        {
            IsOccupied = state;
            return IsOccupied;
        }

    }
}
