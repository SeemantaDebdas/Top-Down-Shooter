using System;
using UnityEngine;

namespace TDS
{
    public class Shield : MonoBehaviour
    {
        [SerializeField] int durability = 10;

        public int CurrentDurability { get; private set; }
        public event Action OnDestroyed;

        private void Awake()
        {
            CurrentDurability = durability;
        }

        public void Damage()
        {
            CurrentDurability--;

            if (CurrentDurability <= 0)
            {
                OnDestroyed?.Invoke();
                Destroy(gameObject);
            }
        }
    }
}
