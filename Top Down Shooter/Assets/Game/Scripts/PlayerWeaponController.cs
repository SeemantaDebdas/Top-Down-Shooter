using TDS.Input;
using UnityEngine;

namespace TDS
{
    public class PlayerWeaponController : MonoBehaviour
    {
        [SerializeField] InputSO input;
        Animator animator;

        void Awake()
        {
            animator = GetComponent<Animator>();
        }

        void OnEnable()
        {
            input.OnFirePerformed += Input_OnFirePerformed;
        }

        void OnDisable()
        {
            input.OnFirePerformed -= Input_OnFirePerformed;
        }

        private void Input_OnFirePerformed()
        {
            Shoot();
        }


        void Shoot()
        {
            animator.SetTrigger("fire");
        }
    }
}
