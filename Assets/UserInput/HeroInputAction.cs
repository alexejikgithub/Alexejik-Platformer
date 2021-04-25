// GENERATED AUTOMATICALLY FROM 'Assets/UserInput/HeroInputAction.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @HeroInputAction : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @HeroInputAction()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""HeroInputAction"",
    ""maps"": [
        {
            ""name"": ""Hero"",
            ""id"": ""3cf3f46a-017d-4055-a35e-a33602611bc2"",
            ""actions"": [
                {
                    ""name"": ""HorizontalMovement"",
                    ""type"": ""Value"",
                    ""id"": ""ceb47c7c-1f14-4026-b642-9945b2a73e2a"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SaySomething"",
                    ""type"": ""Button"",
                    ""id"": ""bcfacc00-62d7-4ee3-9b0a-3a3a31ffdddf"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""38fe6331-ba1e-4295-b91b-9915d770f00c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SpeedUp"",
                    ""type"": ""Button"",
                    ""id"": ""aa08831b-7cf2-41c9-a617-c871d1711ec9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Attak"",
                    ""type"": ""Button"",
                    ""id"": ""221b630e-a621-4158-a9e5-71389be52ea3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Throw"",
                    ""type"": ""Button"",
                    ""id"": ""647300b0-9d4d-4e98-8b6e-30afcbc2a4ad"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ThrowBunch"",
                    ""type"": ""Button"",
                    ""id"": ""ac0af2a0-b69a-4779-be29-f74b92a1d84d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""UseHealPotion"",
                    ""type"": ""Button"",
                    ""id"": ""252199be-27fa-481d-a448-94071473fa0f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector keyboard"",
                    ""id"": ""d2b57bc6-cf01-422d-ad45-deceb08afe57"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HorizontalMovement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""63f22c77-97ce-47b5-add7-e40abbefa9de"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HorizontalMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""514ad3cd-079c-4b9c-842a-772a30d51ca0"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HorizontalMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""96bdeaae-9c7b-4203-8f9b-791affe6ca7c"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HorizontalMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""243a2e29-cfc8-4c65-a1cc-3bbded8ff611"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HorizontalMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""a99163fe-5fdd-4ac1-845e-e21a1c485b34"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HorizontalMovement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""5e4bbdf9-58b6-4c2b-aa86-a9b24ab130ff"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HorizontalMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""7eda551a-b261-41f7-980c-98f7c4d68129"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HorizontalMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""1603dee0-290e-4047-8697-5a12fe51f8a8"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HorizontalMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""dbe38759-edc5-4ba2-9732-b9791b0135dc"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HorizontalMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""3216600d-6ad1-4cbe-9c24-775aa474c8a2"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SaySomething"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2d5431b5-3fa9-4990-a1a4-3f65e1608e7c"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6517e20f-3812-4b9d-9f66-2feb9ede8939"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SpeedUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1407d4b0-b6aa-4acc-8fc5-659ceba60123"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attak"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9613ed7f-55b5-4498-9a5c-63e958acf4d1"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Throw"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5bd58a90-8028-440f-9d25-6c48e367e364"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": ""Hold(duration=1)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ThrowBunch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b56e0f19-29b3-4c51-9c7d-b087977b0c92"",
                    ""path"": ""<Keyboard>/h"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UseHealPotion"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Hero
        m_Hero = asset.FindActionMap("Hero", throwIfNotFound: true);
        m_Hero_HorizontalMovement = m_Hero.FindAction("HorizontalMovement", throwIfNotFound: true);
        m_Hero_SaySomething = m_Hero.FindAction("SaySomething", throwIfNotFound: true);
        m_Hero_Interact = m_Hero.FindAction("Interact", throwIfNotFound: true);
        m_Hero_SpeedUp = m_Hero.FindAction("SpeedUp", throwIfNotFound: true);
        m_Hero_Attak = m_Hero.FindAction("Attak", throwIfNotFound: true);
        m_Hero_Throw = m_Hero.FindAction("Throw", throwIfNotFound: true);
        m_Hero_ThrowBunch = m_Hero.FindAction("ThrowBunch", throwIfNotFound: true);
        m_Hero_UseHealPotion = m_Hero.FindAction("UseHealPotion", throwIfNotFound: true);
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

    // Hero
    private readonly InputActionMap m_Hero;
    private IHeroActions m_HeroActionsCallbackInterface;
    private readonly InputAction m_Hero_HorizontalMovement;
    private readonly InputAction m_Hero_SaySomething;
    private readonly InputAction m_Hero_Interact;
    private readonly InputAction m_Hero_SpeedUp;
    private readonly InputAction m_Hero_Attak;
    private readonly InputAction m_Hero_Throw;
    private readonly InputAction m_Hero_ThrowBunch;
    private readonly InputAction m_Hero_UseHealPotion;
    public struct HeroActions
    {
        private @HeroInputAction m_Wrapper;
        public HeroActions(@HeroInputAction wrapper) { m_Wrapper = wrapper; }
        public InputAction @HorizontalMovement => m_Wrapper.m_Hero_HorizontalMovement;
        public InputAction @SaySomething => m_Wrapper.m_Hero_SaySomething;
        public InputAction @Interact => m_Wrapper.m_Hero_Interact;
        public InputAction @SpeedUp => m_Wrapper.m_Hero_SpeedUp;
        public InputAction @Attak => m_Wrapper.m_Hero_Attak;
        public InputAction @Throw => m_Wrapper.m_Hero_Throw;
        public InputAction @ThrowBunch => m_Wrapper.m_Hero_ThrowBunch;
        public InputAction @UseHealPotion => m_Wrapper.m_Hero_UseHealPotion;
        public InputActionMap Get() { return m_Wrapper.m_Hero; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(HeroActions set) { return set.Get(); }
        public void SetCallbacks(IHeroActions instance)
        {
            if (m_Wrapper.m_HeroActionsCallbackInterface != null)
            {
                @HorizontalMovement.started -= m_Wrapper.m_HeroActionsCallbackInterface.OnHorizontalMovement;
                @HorizontalMovement.performed -= m_Wrapper.m_HeroActionsCallbackInterface.OnHorizontalMovement;
                @HorizontalMovement.canceled -= m_Wrapper.m_HeroActionsCallbackInterface.OnHorizontalMovement;
                @SaySomething.started -= m_Wrapper.m_HeroActionsCallbackInterface.OnSaySomething;
                @SaySomething.performed -= m_Wrapper.m_HeroActionsCallbackInterface.OnSaySomething;
                @SaySomething.canceled -= m_Wrapper.m_HeroActionsCallbackInterface.OnSaySomething;
                @Interact.started -= m_Wrapper.m_HeroActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_HeroActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_HeroActionsCallbackInterface.OnInteract;
                @SpeedUp.started -= m_Wrapper.m_HeroActionsCallbackInterface.OnSpeedUp;
                @SpeedUp.performed -= m_Wrapper.m_HeroActionsCallbackInterface.OnSpeedUp;
                @SpeedUp.canceled -= m_Wrapper.m_HeroActionsCallbackInterface.OnSpeedUp;
                @Attak.started -= m_Wrapper.m_HeroActionsCallbackInterface.OnAttak;
                @Attak.performed -= m_Wrapper.m_HeroActionsCallbackInterface.OnAttak;
                @Attak.canceled -= m_Wrapper.m_HeroActionsCallbackInterface.OnAttak;
                @Throw.started -= m_Wrapper.m_HeroActionsCallbackInterface.OnThrow;
                @Throw.performed -= m_Wrapper.m_HeroActionsCallbackInterface.OnThrow;
                @Throw.canceled -= m_Wrapper.m_HeroActionsCallbackInterface.OnThrow;
                @ThrowBunch.started -= m_Wrapper.m_HeroActionsCallbackInterface.OnThrowBunch;
                @ThrowBunch.performed -= m_Wrapper.m_HeroActionsCallbackInterface.OnThrowBunch;
                @ThrowBunch.canceled -= m_Wrapper.m_HeroActionsCallbackInterface.OnThrowBunch;
                @UseHealPotion.started -= m_Wrapper.m_HeroActionsCallbackInterface.OnUseHealPotion;
                @UseHealPotion.performed -= m_Wrapper.m_HeroActionsCallbackInterface.OnUseHealPotion;
                @UseHealPotion.canceled -= m_Wrapper.m_HeroActionsCallbackInterface.OnUseHealPotion;
            }
            m_Wrapper.m_HeroActionsCallbackInterface = instance;
            if (instance != null)
            {
                @HorizontalMovement.started += instance.OnHorizontalMovement;
                @HorizontalMovement.performed += instance.OnHorizontalMovement;
                @HorizontalMovement.canceled += instance.OnHorizontalMovement;
                @SaySomething.started += instance.OnSaySomething;
                @SaySomething.performed += instance.OnSaySomething;
                @SaySomething.canceled += instance.OnSaySomething;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @SpeedUp.started += instance.OnSpeedUp;
                @SpeedUp.performed += instance.OnSpeedUp;
                @SpeedUp.canceled += instance.OnSpeedUp;
                @Attak.started += instance.OnAttak;
                @Attak.performed += instance.OnAttak;
                @Attak.canceled += instance.OnAttak;
                @Throw.started += instance.OnThrow;
                @Throw.performed += instance.OnThrow;
                @Throw.canceled += instance.OnThrow;
                @ThrowBunch.started += instance.OnThrowBunch;
                @ThrowBunch.performed += instance.OnThrowBunch;
                @ThrowBunch.canceled += instance.OnThrowBunch;
                @UseHealPotion.started += instance.OnUseHealPotion;
                @UseHealPotion.performed += instance.OnUseHealPotion;
                @UseHealPotion.canceled += instance.OnUseHealPotion;
            }
        }
    }
    public HeroActions @Hero => new HeroActions(this);
    public interface IHeroActions
    {
        void OnHorizontalMovement(InputAction.CallbackContext context);
        void OnSaySomething(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnSpeedUp(InputAction.CallbackContext context);
        void OnAttak(InputAction.CallbackContext context);
        void OnThrow(InputAction.CallbackContext context);
        void OnThrowBunch(InputAction.CallbackContext context);
        void OnUseHealPotion(InputAction.CallbackContext context);
    }
}
