// GENERATED AUTOMATICALLY FROM 'Assets/Input/PlayerInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""inputActions"",
            ""id"": ""d2667d74-abc8-4c20-92ef-c22c94a6090e"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""857f270d-ca42-4478-879b-c306932e1d56"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Fire"",
                    ""type"": ""Button"",
                    ""id"": ""ee74b391-d608-4e0d-b02a-45fdbd2a5a49"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Tap(duration=0.3,pressPoint=0.2)""
                },
                {
                    ""name"": ""ChargeShot"",
                    ""type"": ""Button"",
                    ""id"": ""6a61f785-6929-4bdb-ab15-9399c53bae0f"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Hold""
                },
                {
                    ""name"": ""RapidFire"",
                    ""type"": ""Button"",
                    ""id"": ""c715ce0d-0fde-4e36-bb91-fdd9e6be0428"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Look"",
                    ""type"": ""PassThrough"",
                    ""id"": ""acfd22a7-b81f-4abb-8ca6-a12a9351df73"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""64a60894-0e21-4dfc-84df-9b47bdac5a04"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Melee"",
                    ""type"": ""Button"",
                    ""id"": ""f61f8e20-0b39-4190-bb20-06e4a34524af"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Run"",
                    ""type"": ""Button"",
                    ""id"": ""96528310-180f-4916-8318-e5b1e2fe32db"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""WeaponMode"",
                    ""type"": ""Button"",
                    ""id"": ""3f62751d-3266-48a8-a99c-c1b32321cd94"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""24a16859-2f64-490b-ad50-74fbdb919387"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""e49859ac-bc4a-42c9-90de-74467c29bb1f"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""297da149-2f03-4eae-baa0-f57d94b7f3e0"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""ee10c4d7-f12e-42a6-ac76-78e721bd39c6"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""b52a9912-2119-458c-9b6f-2d148c70a3a4"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""b63c4f3b-7828-4986-913f-006b688ab1a0"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""378268da-8fb7-43fa-a8f9-ebd8deba0e4d"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": ""StickDeadzone(min=0.3)"",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""472f5b65-7f1d-418e-8052-41cbe7721cdb"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""955a56ad-60b0-47d2-9fc3-812ed1f07187"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""304c8f55-b09d-4bbe-a0d2-5f4a1b8380ef"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": ""InvertVector2(invertX=false),StickDeadzone(min=0.3)"",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c4877d1b-f455-44cd-b7d9-314ed2cc23ba"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": ""Clamp(min=0.5,max=1),ScaleVector2(x=0.05,y=0.05)"",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c5e34e2b-9dd3-4171-ac4e-3f965af9b671"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ddd15a27-d0aa-4fcd-ab0b-f1936b98966a"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""45f3367f-1f6e-41d9-90f1-d1b0fd945341"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Melee"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d52cceb9-506c-4fac-bcd1-b18dcaee6198"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Melee"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a901d167-0448-4b66-b905-0a0e8291fce6"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""832dc1e2-ec60-4518-8488-0b185d4aca82"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""834dd10d-91b9-406d-8935-30a2b91008ca"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""WeaponMode"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fd5fc703-d2b8-472c-b9e5-d3515b2770fb"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""WeaponMode"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""544def79-f27f-4bd5-be4b-a46a51a8196e"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""RapidFire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d680dd46-5e1b-4754-a8c5-54f210031919"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""RapidFire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1de1e18f-c274-40f8-a98f-41ba6a19e5b8"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""ChargeShot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""560c135a-a769-4723-bf59-e013f5fcfc99"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""ChargeShot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""217731c6-070b-47a6-bb61-58a67d440897"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7efc88eb-7612-449f-905c-c2ea4974d451"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // inputActions
        m_inputActions = asset.FindActionMap("inputActions", throwIfNotFound: true);
        m_inputActions_Move = m_inputActions.FindAction("Move", throwIfNotFound: true);
        m_inputActions_Fire = m_inputActions.FindAction("Fire", throwIfNotFound: true);
        m_inputActions_ChargeShot = m_inputActions.FindAction("ChargeShot", throwIfNotFound: true);
        m_inputActions_RapidFire = m_inputActions.FindAction("RapidFire", throwIfNotFound: true);
        m_inputActions_Look = m_inputActions.FindAction("Look", throwIfNotFound: true);
        m_inputActions_Jump = m_inputActions.FindAction("Jump", throwIfNotFound: true);
        m_inputActions_Melee = m_inputActions.FindAction("Melee", throwIfNotFound: true);
        m_inputActions_Run = m_inputActions.FindAction("Run", throwIfNotFound: true);
        m_inputActions_WeaponMode = m_inputActions.FindAction("WeaponMode", throwIfNotFound: true);
        m_inputActions_Interact = m_inputActions.FindAction("Interact", throwIfNotFound: true);
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

    // inputActions
    private readonly InputActionMap m_inputActions;
    private IInputActionsActions m_InputActionsActionsCallbackInterface;
    private readonly InputAction m_inputActions_Move;
    private readonly InputAction m_inputActions_Fire;
    private readonly InputAction m_inputActions_ChargeShot;
    private readonly InputAction m_inputActions_RapidFire;
    private readonly InputAction m_inputActions_Look;
    private readonly InputAction m_inputActions_Jump;
    private readonly InputAction m_inputActions_Melee;
    private readonly InputAction m_inputActions_Run;
    private readonly InputAction m_inputActions_WeaponMode;
    private readonly InputAction m_inputActions_Interact;
    public struct InputActionsActions
    {
        private @PlayerInput m_Wrapper;
        public InputActionsActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_inputActions_Move;
        public InputAction @Fire => m_Wrapper.m_inputActions_Fire;
        public InputAction @ChargeShot => m_Wrapper.m_inputActions_ChargeShot;
        public InputAction @RapidFire => m_Wrapper.m_inputActions_RapidFire;
        public InputAction @Look => m_Wrapper.m_inputActions_Look;
        public InputAction @Jump => m_Wrapper.m_inputActions_Jump;
        public InputAction @Melee => m_Wrapper.m_inputActions_Melee;
        public InputAction @Run => m_Wrapper.m_inputActions_Run;
        public InputAction @WeaponMode => m_Wrapper.m_inputActions_WeaponMode;
        public InputAction @Interact => m_Wrapper.m_inputActions_Interact;
        public InputActionMap Get() { return m_Wrapper.m_inputActions; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(InputActionsActions set) { return set.Get(); }
        public void SetCallbacks(IInputActionsActions instance)
        {
            if (m_Wrapper.m_InputActionsActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_InputActionsActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_InputActionsActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_InputActionsActionsCallbackInterface.OnMove;
                @Fire.started -= m_Wrapper.m_InputActionsActionsCallbackInterface.OnFire;
                @Fire.performed -= m_Wrapper.m_InputActionsActionsCallbackInterface.OnFire;
                @Fire.canceled -= m_Wrapper.m_InputActionsActionsCallbackInterface.OnFire;
                @ChargeShot.started -= m_Wrapper.m_InputActionsActionsCallbackInterface.OnChargeShot;
                @ChargeShot.performed -= m_Wrapper.m_InputActionsActionsCallbackInterface.OnChargeShot;
                @ChargeShot.canceled -= m_Wrapper.m_InputActionsActionsCallbackInterface.OnChargeShot;
                @RapidFire.started -= m_Wrapper.m_InputActionsActionsCallbackInterface.OnRapidFire;
                @RapidFire.performed -= m_Wrapper.m_InputActionsActionsCallbackInterface.OnRapidFire;
                @RapidFire.canceled -= m_Wrapper.m_InputActionsActionsCallbackInterface.OnRapidFire;
                @Look.started -= m_Wrapper.m_InputActionsActionsCallbackInterface.OnLook;
                @Look.performed -= m_Wrapper.m_InputActionsActionsCallbackInterface.OnLook;
                @Look.canceled -= m_Wrapper.m_InputActionsActionsCallbackInterface.OnLook;
                @Jump.started -= m_Wrapper.m_InputActionsActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_InputActionsActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_InputActionsActionsCallbackInterface.OnJump;
                @Melee.started -= m_Wrapper.m_InputActionsActionsCallbackInterface.OnMelee;
                @Melee.performed -= m_Wrapper.m_InputActionsActionsCallbackInterface.OnMelee;
                @Melee.canceled -= m_Wrapper.m_InputActionsActionsCallbackInterface.OnMelee;
                @Run.started -= m_Wrapper.m_InputActionsActionsCallbackInterface.OnRun;
                @Run.performed -= m_Wrapper.m_InputActionsActionsCallbackInterface.OnRun;
                @Run.canceled -= m_Wrapper.m_InputActionsActionsCallbackInterface.OnRun;
                @WeaponMode.started -= m_Wrapper.m_InputActionsActionsCallbackInterface.OnWeaponMode;
                @WeaponMode.performed -= m_Wrapper.m_InputActionsActionsCallbackInterface.OnWeaponMode;
                @WeaponMode.canceled -= m_Wrapper.m_InputActionsActionsCallbackInterface.OnWeaponMode;
                @Interact.started -= m_Wrapper.m_InputActionsActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_InputActionsActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_InputActionsActionsCallbackInterface.OnInteract;
            }
            m_Wrapper.m_InputActionsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Fire.started += instance.OnFire;
                @Fire.performed += instance.OnFire;
                @Fire.canceled += instance.OnFire;
                @ChargeShot.started += instance.OnChargeShot;
                @ChargeShot.performed += instance.OnChargeShot;
                @ChargeShot.canceled += instance.OnChargeShot;
                @RapidFire.started += instance.OnRapidFire;
                @RapidFire.performed += instance.OnRapidFire;
                @RapidFire.canceled += instance.OnRapidFire;
                @Look.started += instance.OnLook;
                @Look.performed += instance.OnLook;
                @Look.canceled += instance.OnLook;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Melee.started += instance.OnMelee;
                @Melee.performed += instance.OnMelee;
                @Melee.canceled += instance.OnMelee;
                @Run.started += instance.OnRun;
                @Run.performed += instance.OnRun;
                @Run.canceled += instance.OnRun;
                @WeaponMode.started += instance.OnWeaponMode;
                @WeaponMode.performed += instance.OnWeaponMode;
                @WeaponMode.canceled += instance.OnWeaponMode;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
            }
        }
    }
    public InputActionsActions @inputActions => new InputActionsActions(this);
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    public interface IInputActionsActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnFire(InputAction.CallbackContext context);
        void OnChargeShot(InputAction.CallbackContext context);
        void OnRapidFire(InputAction.CallbackContext context);
        void OnLook(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnMelee(InputAction.CallbackContext context);
        void OnRun(InputAction.CallbackContext context);
        void OnWeaponMode(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
    }
}
