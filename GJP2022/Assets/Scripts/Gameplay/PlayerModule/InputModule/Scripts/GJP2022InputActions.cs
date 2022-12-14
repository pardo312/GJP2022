// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/PlayerModule/InputModule/Scripts/GJP2022InputActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @GJP2022InputActions : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @GJP2022InputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""GJP2022InputActions"",
    ""maps"": [
        {
            ""name"": ""PlayerMovement"",
            ""id"": ""6d01c992-c782-4326-aad1-3a6e4d984665"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""95f57b1b-2596-4972-ad3c-edfeca33a6b2"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""646ebc79-0646-4469-9fcd-bdf34e3bfdd1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Attack1"",
                    ""type"": ""Button"",
                    ""id"": ""a968537e-20bb-4104-b859-30eaeb77e610"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Attack2"",
                    ""type"": ""Button"",
                    ""id"": ""fb6767f8-dc12-4d70-a6b9-61331d8d2cfa"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""8e91e494-361d-4eec-a591-842984aa2979"",
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
                    ""id"": ""f969ef8e-6724-4e60-a7f7-50f0c27700e5"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PlayerController"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""aecdf5d4-22ec-48ed-a03a-4b4203a9e86f"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PlayerController"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""ab815fcf-210c-4c61-9af7-4e440b694ae9"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PlayerController"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""c6224253-940c-47fe-9ce7-11a9a89df3b9"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PlayerController"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""7cb8fa94-f6d2-4469-9ad1-1c185ad4bca4"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b3a03587-0f3e-471a-b80a-f16bbeb58e37"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c419a7e5-19cd-46d1-82e6-bf2664e586d2"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PlayerMovement
        m_PlayerMovement = asset.FindActionMap("PlayerMovement", throwIfNotFound: true);
        m_PlayerMovement_Move = m_PlayerMovement.FindAction("Move", throwIfNotFound: true);
        m_PlayerMovement_Jump = m_PlayerMovement.FindAction("Jump", throwIfNotFound: true);
        m_PlayerMovement_Attack1 = m_PlayerMovement.FindAction("Attack1", throwIfNotFound: true);
        m_PlayerMovement_Attack2 = m_PlayerMovement.FindAction("Attack2", throwIfNotFound: true);
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

    // PlayerMovement
    private readonly InputActionMap m_PlayerMovement;
    private IPlayerMovementActions m_PlayerMovementActionsCallbackInterface;
    private readonly InputAction m_PlayerMovement_Move;
    private readonly InputAction m_PlayerMovement_Jump;
    private readonly InputAction m_PlayerMovement_Attack1;
    private readonly InputAction m_PlayerMovement_Attack2;
    public struct PlayerMovementActions
    {
        private @GJP2022InputActions m_Wrapper;
        public PlayerMovementActions(@GJP2022InputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_PlayerMovement_Move;
        public InputAction @Jump => m_Wrapper.m_PlayerMovement_Jump;
        public InputAction @Attack1 => m_Wrapper.m_PlayerMovement_Attack1;
        public InputAction @Attack2 => m_Wrapper.m_PlayerMovement_Attack2;
        public InputActionMap Get() { return m_Wrapper.m_PlayerMovement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerMovementActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerMovementActions instance)
        {
            if (m_Wrapper.m_PlayerMovementActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMove;
                @Jump.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnJump;
                @Attack1.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnAttack1;
                @Attack1.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnAttack1;
                @Attack1.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnAttack1;
                @Attack2.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnAttack2;
                @Attack2.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnAttack2;
                @Attack2.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnAttack2;
            }
            m_Wrapper.m_PlayerMovementActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Attack1.started += instance.OnAttack1;
                @Attack1.performed += instance.OnAttack1;
                @Attack1.canceled += instance.OnAttack1;
                @Attack2.started += instance.OnAttack2;
                @Attack2.performed += instance.OnAttack2;
                @Attack2.canceled += instance.OnAttack2;
            }
        }
    }
    public PlayerMovementActions @PlayerMovement => new PlayerMovementActions(this);
    public interface IPlayerMovementActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnAttack1(InputAction.CallbackContext context);
        void OnAttack2(InputAction.CallbackContext context);
    }
}
