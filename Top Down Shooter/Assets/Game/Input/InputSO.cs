using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TDS.Input
{
    [CreateAssetMenu(fileName = "Input", menuName = "TDS/Input", order = 1)]
    public class InputSO : ScriptableObject, PlayerControls.ICharacterActions
    {
        public event Action
        OnFirePerformed, OnFireCancelled,
        OnReloadPerformed,
        OnEquipWeapon1Performed, OnEquipWeapon2Performed, OnEquipWeapon3Performed, OnEquipWeapon4Performed, OnEquipWeapon5Performed,
        OnSprintPerformed, OnSprintCancelled,
        OnDropPerformed,
        OnTogglePerformed,
        OnInteractPerformed;
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
            if (context.performed)
            {
                OnFirePerformed?.Invoke();
            }

            if (context.canceled)
            {
                OnFireCancelled?.Invoke();
            }
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

        public void OnReload(InputAction.CallbackContext context)
        {
            if (!context.performed)
                return;

            OnReloadPerformed?.Invoke();
        }

        public void OnEquipWeapon1(InputAction.CallbackContext context)
        {
            if (!context.performed)
                return;

            OnEquipWeapon1Performed?.Invoke();
        }

        public void OnEquipWeapon2(InputAction.CallbackContext context)
        {
            if (!context.performed)
                return;

            OnEquipWeapon2Performed?.Invoke();
        }

        public void OnEquipWeapon3(InputAction.CallbackContext context)
        {
            if (!context.performed)
                return;

            OnEquipWeapon3Performed?.Invoke();
        }

        public void OnEquipWeapon4(InputAction.CallbackContext context)
        {
            if (!context.performed)
                return;

            OnEquipWeapon4Performed?.Invoke();
        }

        public void OnEquipWeapon5(InputAction.CallbackContext context)
        {
            if (!context.performed)
                return;

            OnEquipWeapon5Performed?.Invoke();
        }

        public void OnDropWeapon(InputAction.CallbackContext context)
        {
            if (!context.performed)
                return;

            OnDropPerformed?.Invoke();
        }

        public void OnToggleWeaponMode(InputAction.CallbackContext context)
        {
            if (!context.performed)
                return;

            OnTogglePerformed?.Invoke();
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            if (!context.performed)
                return;

            OnInteractPerformed?.Invoke();
        }
    }

}
