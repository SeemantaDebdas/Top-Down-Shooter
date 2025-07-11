//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.11.2
//     from Assets/Game/Input/PlayerControls.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerControls: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Character"",
            ""id"": ""e3351e7b-7bc9-4362-b016-40dbffd5cc80"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""3db60edc-0c49-4dfe-bdc9-93a0d70d15b5"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Fire"",
                    ""type"": ""Button"",
                    ""id"": ""9a8123ff-f88b-4567-a7b5-1d4f901d569e"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Aim"",
                    ""type"": ""Value"",
                    ""id"": ""2af23ff1-e884-444e-8ac0-fef7a2c3d446"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Sprint"",
                    ""type"": ""Button"",
                    ""id"": ""5abbe649-1237-4fec-8ad1-abddf22163f0"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Reload"",
                    ""type"": ""Button"",
                    ""id"": ""c24f3bde-d6e4-4200-8d5f-cb2a0fb8469d"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""EquipWeapon1"",
                    ""type"": ""Button"",
                    ""id"": ""e492128a-0112-4598-8923-44705eb8647f"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""EquipWeapon2"",
                    ""type"": ""Button"",
                    ""id"": ""13f7b913-1740-4d8c-bb9c-9c30bcaa82e7"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""EquipWeapon3"",
                    ""type"": ""Button"",
                    ""id"": ""4f1505ad-b1d0-4d5c-999e-c8683f7d79db"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""EquipWeapon4"",
                    ""type"": ""Button"",
                    ""id"": ""f3b97e19-ab79-4923-89fe-7cbc99c3ea81"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""EquipWeapon5"",
                    ""type"": ""Button"",
                    ""id"": ""7bce3d78-a009-4c05-abc7-bde658bf2b0c"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""DropWeapon"",
                    ""type"": ""Button"",
                    ""id"": ""66748e07-3c1d-41c6-b8af-984b176c1b19"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ToggleWeaponMode"",
                    ""type"": ""Button"",
                    ""id"": ""abcea20f-06d9-4d93-838f-549646689cbb"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""1c57ae63-a112-44d4-9dc9-c1d4de931159"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""b0c3f834-50f0-4b6c-8ea2-2fa33ca51e0b"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""0c64e2cc-b64b-4556-91f6-8d772d989cf2"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""90320a5f-f622-443c-b38f-4652ae6c320b"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""6a60fe2a-c0d8-4317-9202-6deb027187a7"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""32a070ed-9f57-43e0-aa2a-6524aa82adde"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""e2db1521-9582-4a8c-9ab3-676206958b60"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1c5ed77a-447a-41ab-8a3f-8c0b0a4cf4e8"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bcf62f39-1078-42b5-95f8-a2e4314164a3"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""abfc372a-3010-4369-ad11-550b172ea2c8"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Reload"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e4f5cdc4-3fab-4c5b-92ce-f8c9e9a2ebec"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""EquipWeapon1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7895edc3-8fcd-419d-8cbd-215086606ab9"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""EquipWeapon2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""016e7d03-5219-4cc2-abb6-b79b54ce7471"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""EquipWeapon3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2ab1901b-57e7-4647-b13c-341198aa0371"",
                    ""path"": ""<Keyboard>/4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""EquipWeapon4"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""67b59742-e6aa-4fa8-8e6d-88fbabed5943"",
                    ""path"": ""<Keyboard>/5"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""EquipWeapon5"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e0014f4d-9930-4bfd-be82-deb8004cd0bc"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DropWeapon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7d0012a3-142a-43e1-8167-8a32b638a884"",
                    ""path"": ""<Keyboard>/t"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ToggleWeaponMode"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8e0d8546-7592-43d0-abd4-3851615e0fdc"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Character
        m_Character = asset.FindActionMap("Character", throwIfNotFound: true);
        m_Character_Move = m_Character.FindAction("Move", throwIfNotFound: true);
        m_Character_Fire = m_Character.FindAction("Fire", throwIfNotFound: true);
        m_Character_Aim = m_Character.FindAction("Aim", throwIfNotFound: true);
        m_Character_Sprint = m_Character.FindAction("Sprint", throwIfNotFound: true);
        m_Character_Reload = m_Character.FindAction("Reload", throwIfNotFound: true);
        m_Character_EquipWeapon1 = m_Character.FindAction("EquipWeapon1", throwIfNotFound: true);
        m_Character_EquipWeapon2 = m_Character.FindAction("EquipWeapon2", throwIfNotFound: true);
        m_Character_EquipWeapon3 = m_Character.FindAction("EquipWeapon3", throwIfNotFound: true);
        m_Character_EquipWeapon4 = m_Character.FindAction("EquipWeapon4", throwIfNotFound: true);
        m_Character_EquipWeapon5 = m_Character.FindAction("EquipWeapon5", throwIfNotFound: true);
        m_Character_DropWeapon = m_Character.FindAction("DropWeapon", throwIfNotFound: true);
        m_Character_ToggleWeaponMode = m_Character.FindAction("ToggleWeaponMode", throwIfNotFound: true);
        m_Character_Interact = m_Character.FindAction("Interact", throwIfNotFound: true);
    }

    ~@PlayerControls()
    {
        UnityEngine.Debug.Assert(!m_Character.enabled, "This will cause a leak and performance issues, PlayerControls.Character.Disable() has not been called.");
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Character
    private readonly InputActionMap m_Character;
    private List<ICharacterActions> m_CharacterActionsCallbackInterfaces = new List<ICharacterActions>();
    private readonly InputAction m_Character_Move;
    private readonly InputAction m_Character_Fire;
    private readonly InputAction m_Character_Aim;
    private readonly InputAction m_Character_Sprint;
    private readonly InputAction m_Character_Reload;
    private readonly InputAction m_Character_EquipWeapon1;
    private readonly InputAction m_Character_EquipWeapon2;
    private readonly InputAction m_Character_EquipWeapon3;
    private readonly InputAction m_Character_EquipWeapon4;
    private readonly InputAction m_Character_EquipWeapon5;
    private readonly InputAction m_Character_DropWeapon;
    private readonly InputAction m_Character_ToggleWeaponMode;
    private readonly InputAction m_Character_Interact;
    public struct CharacterActions
    {
        private @PlayerControls m_Wrapper;
        public CharacterActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Character_Move;
        public InputAction @Fire => m_Wrapper.m_Character_Fire;
        public InputAction @Aim => m_Wrapper.m_Character_Aim;
        public InputAction @Sprint => m_Wrapper.m_Character_Sprint;
        public InputAction @Reload => m_Wrapper.m_Character_Reload;
        public InputAction @EquipWeapon1 => m_Wrapper.m_Character_EquipWeapon1;
        public InputAction @EquipWeapon2 => m_Wrapper.m_Character_EquipWeapon2;
        public InputAction @EquipWeapon3 => m_Wrapper.m_Character_EquipWeapon3;
        public InputAction @EquipWeapon4 => m_Wrapper.m_Character_EquipWeapon4;
        public InputAction @EquipWeapon5 => m_Wrapper.m_Character_EquipWeapon5;
        public InputAction @DropWeapon => m_Wrapper.m_Character_DropWeapon;
        public InputAction @ToggleWeaponMode => m_Wrapper.m_Character_ToggleWeaponMode;
        public InputAction @Interact => m_Wrapper.m_Character_Interact;
        public InputActionMap Get() { return m_Wrapper.m_Character; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CharacterActions set) { return set.Get(); }
        public void AddCallbacks(ICharacterActions instance)
        {
            if (instance == null || m_Wrapper.m_CharacterActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_CharacterActionsCallbackInterfaces.Add(instance);
            @Move.started += instance.OnMove;
            @Move.performed += instance.OnMove;
            @Move.canceled += instance.OnMove;
            @Fire.started += instance.OnFire;
            @Fire.performed += instance.OnFire;
            @Fire.canceled += instance.OnFire;
            @Aim.started += instance.OnAim;
            @Aim.performed += instance.OnAim;
            @Aim.canceled += instance.OnAim;
            @Sprint.started += instance.OnSprint;
            @Sprint.performed += instance.OnSprint;
            @Sprint.canceled += instance.OnSprint;
            @Reload.started += instance.OnReload;
            @Reload.performed += instance.OnReload;
            @Reload.canceled += instance.OnReload;
            @EquipWeapon1.started += instance.OnEquipWeapon1;
            @EquipWeapon1.performed += instance.OnEquipWeapon1;
            @EquipWeapon1.canceled += instance.OnEquipWeapon1;
            @EquipWeapon2.started += instance.OnEquipWeapon2;
            @EquipWeapon2.performed += instance.OnEquipWeapon2;
            @EquipWeapon2.canceled += instance.OnEquipWeapon2;
            @EquipWeapon3.started += instance.OnEquipWeapon3;
            @EquipWeapon3.performed += instance.OnEquipWeapon3;
            @EquipWeapon3.canceled += instance.OnEquipWeapon3;
            @EquipWeapon4.started += instance.OnEquipWeapon4;
            @EquipWeapon4.performed += instance.OnEquipWeapon4;
            @EquipWeapon4.canceled += instance.OnEquipWeapon4;
            @EquipWeapon5.started += instance.OnEquipWeapon5;
            @EquipWeapon5.performed += instance.OnEquipWeapon5;
            @EquipWeapon5.canceled += instance.OnEquipWeapon5;
            @DropWeapon.started += instance.OnDropWeapon;
            @DropWeapon.performed += instance.OnDropWeapon;
            @DropWeapon.canceled += instance.OnDropWeapon;
            @ToggleWeaponMode.started += instance.OnToggleWeaponMode;
            @ToggleWeaponMode.performed += instance.OnToggleWeaponMode;
            @ToggleWeaponMode.canceled += instance.OnToggleWeaponMode;
            @Interact.started += instance.OnInteract;
            @Interact.performed += instance.OnInteract;
            @Interact.canceled += instance.OnInteract;
        }

        private void UnregisterCallbacks(ICharacterActions instance)
        {
            @Move.started -= instance.OnMove;
            @Move.performed -= instance.OnMove;
            @Move.canceled -= instance.OnMove;
            @Fire.started -= instance.OnFire;
            @Fire.performed -= instance.OnFire;
            @Fire.canceled -= instance.OnFire;
            @Aim.started -= instance.OnAim;
            @Aim.performed -= instance.OnAim;
            @Aim.canceled -= instance.OnAim;
            @Sprint.started -= instance.OnSprint;
            @Sprint.performed -= instance.OnSprint;
            @Sprint.canceled -= instance.OnSprint;
            @Reload.started -= instance.OnReload;
            @Reload.performed -= instance.OnReload;
            @Reload.canceled -= instance.OnReload;
            @EquipWeapon1.started -= instance.OnEquipWeapon1;
            @EquipWeapon1.performed -= instance.OnEquipWeapon1;
            @EquipWeapon1.canceled -= instance.OnEquipWeapon1;
            @EquipWeapon2.started -= instance.OnEquipWeapon2;
            @EquipWeapon2.performed -= instance.OnEquipWeapon2;
            @EquipWeapon2.canceled -= instance.OnEquipWeapon2;
            @EquipWeapon3.started -= instance.OnEquipWeapon3;
            @EquipWeapon3.performed -= instance.OnEquipWeapon3;
            @EquipWeapon3.canceled -= instance.OnEquipWeapon3;
            @EquipWeapon4.started -= instance.OnEquipWeapon4;
            @EquipWeapon4.performed -= instance.OnEquipWeapon4;
            @EquipWeapon4.canceled -= instance.OnEquipWeapon4;
            @EquipWeapon5.started -= instance.OnEquipWeapon5;
            @EquipWeapon5.performed -= instance.OnEquipWeapon5;
            @EquipWeapon5.canceled -= instance.OnEquipWeapon5;
            @DropWeapon.started -= instance.OnDropWeapon;
            @DropWeapon.performed -= instance.OnDropWeapon;
            @DropWeapon.canceled -= instance.OnDropWeapon;
            @ToggleWeaponMode.started -= instance.OnToggleWeaponMode;
            @ToggleWeaponMode.performed -= instance.OnToggleWeaponMode;
            @ToggleWeaponMode.canceled -= instance.OnToggleWeaponMode;
            @Interact.started -= instance.OnInteract;
            @Interact.performed -= instance.OnInteract;
            @Interact.canceled -= instance.OnInteract;
        }

        public void RemoveCallbacks(ICharacterActions instance)
        {
            if (m_Wrapper.m_CharacterActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(ICharacterActions instance)
        {
            foreach (var item in m_Wrapper.m_CharacterActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_CharacterActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public CharacterActions @Character => new CharacterActions(this);
    public interface ICharacterActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnFire(InputAction.CallbackContext context);
        void OnAim(InputAction.CallbackContext context);
        void OnSprint(InputAction.CallbackContext context);
        void OnReload(InputAction.CallbackContext context);
        void OnEquipWeapon1(InputAction.CallbackContext context);
        void OnEquipWeapon2(InputAction.CallbackContext context);
        void OnEquipWeapon3(InputAction.CallbackContext context);
        void OnEquipWeapon4(InputAction.CallbackContext context);
        void OnEquipWeapon5(InputAction.CallbackContext context);
        void OnDropWeapon(InputAction.CallbackContext context);
        void OnToggleWeaponMode(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
    }
}
