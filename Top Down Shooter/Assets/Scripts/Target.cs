using UnityEngine;

namespace TDS
{
    [RequireComponent(typeof(Rigidbody))]
    public class Target : MonoBehaviour
    {
        void Awake()
        {
            gameObject.layer = LayerMask.NameToLayer("Enemy");
        }
    }
}
