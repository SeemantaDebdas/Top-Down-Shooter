using UnityEngine;

namespace TDS
{
    public enum AttackType
    {
        Close,
        Charge
    }

    [CreateAssetMenu(fileName = "New Attack", menuName = "TDS/Attack", order = 1)]
    public class AttackData : ScriptableObject
    {
        [field: SerializeField] public AttackType Type { get; private set; }
        [field: SerializeField] public float Range { get; private set; }
        [field: SerializeField] public float Force { get; private set; }
        [field: SerializeField] public string Animation { get; private set; }
    }
}
