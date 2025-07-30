using UnityEngine;

namespace TDS
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "New MeleeWeaponData", menuName = "TDS/Melee Weapon Data", order = 0)]
    public class MeleeWeaponData : ScriptableObject
    {
        public GameObject handVisualPrefab;
        public GameObject throwPrefab;
    }
}
