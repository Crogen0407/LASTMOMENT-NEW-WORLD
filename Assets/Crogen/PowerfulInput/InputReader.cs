using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Crogen.PowerfulInput
{
    [CreateAssetMenu(fileName = "InputReader", menuName = "Crogen/InputReader", order = 0)]
    public class InputReader : ScriptableObject, Controls.IPlayerActions, Controls.IUIActions
    {
        #region Input Event

        public event Action<Vector2> MoveDirectionEvent;
        public event Action SpeedUpEvent;
        public event Action SpeedDownEvent;
        public event Action AttackEvent;
        public event Action MouseClickEvent; 
    
        #endregion

        private Controls _controls;

        private void OnEnable()
        {
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
            if (context.performed)
            {
                SpeedUpEvent?.Invoke();
            }

            if (context.canceled)
            {
                SpeedDownEvent?.Invoke();
            }
        }
        
        public void OnAttack(InputAction.CallbackContext context)
        {
            if(context.performed)
                AttackEvent?.Invoke();
        }

        public void OnMoveDirection(InputAction.CallbackContext context)
        {
            Vector2 dir = context.ReadValue<Vector2>();
            MoveDirectionEvent?.Invoke(dir);
        }

        public void OnMouseClick(InputAction.CallbackContext context)
        {
            MouseClickEvent?.Invoke();
        }
    }
}