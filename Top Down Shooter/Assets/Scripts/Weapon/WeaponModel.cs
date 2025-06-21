using UnityEngine;

namespace TDS
{
    public enum WeaponEquipType { BackEquip, SideEquip }
    public enum WeaponHoldType { CommonHold = 1, LowHold, HighHold }//helps with animation layers
    public class WeaponModel : MonoBehaviour
    {
        public WeaponType weaponType;
        public WeaponEquipType equipType;
        public WeaponHoldType holdType;

        public Transform gunPoint, holdPoint;
    }
}
