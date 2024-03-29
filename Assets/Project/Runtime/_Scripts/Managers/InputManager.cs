using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Project.Runtime._Scripts.Managers
{
    public enum InputType
    {
        Menu,
        Player
    }

    public class InputManager : MonoBehaviour
    {
        public static event Action<InputAction.CallbackContext> OnMovementInput;
    
        public InputType InputType
        {
            get { return _inputType; }
            set
            {
                SwitchActionMap(value, _inputType);
                _inputType = value;
            }
        }
        private InputType _inputType;

        private UserActions inputActions;
    
        private void Awake() {
            InitializeInputActions();
        }
    
        private void InitializeInputActions() {
            inputActions = new UserActions();
            InputType = InputType.Player;
        }

        #region ActionMapStates

        private void SwitchActionMap(InputType currentType, InputType previousType) {

            switch (previousType) {
                case InputType.Menu:
                    OnMenuDisable();
                    break;
                case InputType.Player:
                    OnPlayerDisable();
                    break;
            }
        
            switch (currentType) {
                case InputType.Menu:
                    OnMenuEnable();
                    break;
                case InputType.Player:
                    OnPlayerEnable();
                    break;
            }
        }

        private void OnMenuEnable() {
            inputActions.Menu.Enable();
        }
    
        private void OnMenuDisable() {
            inputActions.Menu.Disable();
        }
    
        private void OnPlayerEnable() {
            inputActions.Player.Enable();
            EnableMovementInput();
        }
    
        private void OnPlayerDisable() {
            inputActions.Player.Disable();
            DisableMovementInput();
        }
    
        #endregion
    
        #region PlayerActionStates

        private void EnableMovementInput() {
            inputActions.Player.Movement.started += OnMovementInput;
            inputActions.Player.Movement.canceled += OnMovementInput;
            inputActions.Player.Movement.performed += OnMovementInput;
        }
    
        private void DisableMovementInput() {
            inputActions.Player.Movement.started -= OnMovementInput;
            inputActions.Player.Movement.canceled -= OnMovementInput;
            inputActions.Player.Movement.performed -= OnMovementInput;
        }
        
        #endregion
    
    
    }
}