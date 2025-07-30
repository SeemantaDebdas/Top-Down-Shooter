using System.Collections.Generic;
using UnityEngine;

namespace TDS
{
    public class RagdollController : MonoBehaviour
    {
        Rigidbody[] rigidBodies;

        void Awake()
        {
            rigidBodies = GetComponentsInChildren<Rigidbody>();
            DeactivateRagdoll();
        }

        public void DeactivateRagdoll()
        {
            for (int i = 0; i < rigidBodies.Length; i++)
                rigidBodies[i].isKinematic = true;
        }

        public void ActivateRagdoll()
        {
            for (int i = 0; i < rigidBodies.Length; i++)
            {
                if (rigidBodies[i] != null)
                    rigidBodies[i].isKinematic = false;
            }
        }
    }
}
