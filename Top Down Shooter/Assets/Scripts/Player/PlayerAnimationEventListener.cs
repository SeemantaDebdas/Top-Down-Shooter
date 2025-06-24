using UnityEngine;

namespace TDS
{
    public class PlayerAnimationEventListener : MonoBehaviour
    {
        PlayerWeaponController weaponController;
        PlayerWeaponVisuals weaponVisuals;

        void Awake()
        {
            weaponController = GetComponent<PlayerWeaponController>();
            weaponVisuals = GetComponent<PlayerWeaponVisuals>();
        }

        public void ReloadOver()
        {
            weaponVisuals.OnReloadOver();
            weaponController.Reload();
        }

        public void EquippingOver()
        {
            weaponController.FinishedEquippingWeapon();
        }

        public void SwitchOnWeaponModel() => weaponVisuals.SwitchOnCurrentWeaponModel();
    }
}
