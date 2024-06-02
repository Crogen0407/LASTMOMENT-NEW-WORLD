using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Crogen.PowerfulInput
{
    [CreateAssetMenu(fileName = "InputReader", menuName = "Crogen/InputReader", order = 0)]
    public class InputReader : ScriptableObject, Controls.IPlayerActions, Controls.IUIActions
    {
        private GameSettingManager _gameSettingManager;
        
        public Vector2 mousePositionClampSize = new Vector2(1200f, 1200f);
        #region Input Event

        public event Action<Vector3> ChangeMoveDirectionEvent;
        public event Action StartRunEvent;
        public event Action<bool> SpeedChangeEvent;
        public event Action InteractionEvent;
        public event Action ResetDirectionEvent;
        public event Action EscEvent;
        
        //Attack
        public event Action AttackStartEvent;
        public event Action AttackEndEvent;
        
        public event Action MouseClickEvent; 
    
        #endregion

        private Controls _controls;

        private void OnEnable()
        {
            _gameSettingManager = GameSettingManager.Instance;
            if (_controls == null)
            {
                _controls = new Controls();
                _controls.Player.SetCallbacks(this);
                _controls.UI.SetCallbacks(this);
            }
            _controls.Enable();
        }

        private void OnDisable()
        {
            _controls.Disable();
        }

        public void OnSpeedUp(InputAction.CallbackContext context)
        {
            if(context.started)
                StartRunEvent?.Invoke();
            if (context.performed)
                SpeedChangeEvent?.Invoke(true);
            if(context.canceled)
                SpeedChangeEvent?.Invoke(false);
        }
        
        public void OnAttack(InputAction.CallbackContext context)
        {
            if(context.started)
                AttackStartEvent?.Invoke();
            if(context.canceled)
                AttackEndEvent?.Invoke();
        }

        public void OnMoveDirection(InputAction.CallbackContext context)
        {
            Vector2 position = context.ReadValue<Vector2>();
            ChangeMoveDirectionEvent?.Invoke(position);
        }

        public void OnInteraction(InputAction.CallbackContext context)
        {
            InteractionEvent?.Invoke();
        }

        public void OnResetDirection(InputAction.CallbackContext context)
        {
            ResetDirectionEvent?.Invoke();
        }

        public void OnMouseClick(InputAction.CallbackContext context)
        {
            MouseClickEvent?.Invoke();
        }

        public void OnEsc(InputAction.CallbackContext context)
        {
            EscEvent?.Invoke();
        }

        public void ChangeMovementBindingKey(bool mouseMode)
        {
            InputBinding newBinding = new InputBinding("");
        }
        
        public void DisablePlayerActions() => _controls.Player.Disable();
        public void EnablePlayerActions() => _controls.Player.Enable();
        public void DisableUIActions() => _controls.UI.Disable();
        public void EnableUIActions() => _controls.UI.Enable();
    }
}