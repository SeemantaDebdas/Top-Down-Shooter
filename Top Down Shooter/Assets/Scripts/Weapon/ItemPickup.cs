using UnityEngine;

namespace TDS
{
    public class ItemPickup : MonoBehaviour
    {
        [SerializeField] Weapon weapon;
        void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerWeaponController controller))
                controller.AddWeapon(weapon);
        }
    }
}
