using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TDS.Input
{
    [CreateAssetMenu(fileName = "Input", menuName = "TDS/Input", order = 1)]
    public class InputSO : ScriptableObject, PlayerControls.ICharacterActions
    {
        public event Action OnFirePerformed, OnSprintPerformed, OnSprintCancelled;
        public event Action<Vector2> OnMovePerformed;
        public event Action<Vector2> OnAimPerformed;

        PlayerControls control;

        private void OnEnable()
        {
            control ??= new PlayerControls();
            control.Character.SetCallbacks(this);
            control.Enable();
        }

        private void OnDisable()
        {
            control.Disable();
        }


        public void OnAim(InputAction.CallbackContext context)
        {
            OnAimPerformed?.Invoke(context.ReadValue<Vector2>());
        }

        public void OnFire(InputAction.CallbackContext context)
        {
            if (!context.performed)
                return;

            OnFirePerformed?.Invoke();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            OnMovePerformed?.Invoke(context.ReadValue<Vector2>());
        }

        public void OnSprint(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                OnSprintPerformed?.Invoke();
            }

            if (context.canceled)
            {
                OnSprintCancelled?.Invoke();
            }
        }
    }

}
