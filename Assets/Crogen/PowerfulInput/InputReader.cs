using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Crogen.PowerfulInput
{
    [CreateAssetMenu(fileName = "InputReader", menuName = "Crogen/InputReader", order = 0)]
    public class InputReader : ScriptableObject, Controls.IPlayerActions
    {
        #region Input Event

        public event Action<Vector2> MovePlayerEvent;
        public event Action<float> ChangeScrollEvent;
        public event Action AttackEvent;
    
        #endregion

        private Controls _controls;

        private void OnEnable()
        {
            if (_controls == null)
            {
                _controls = new Controls();
                _controls.Player.SetCallbacks(this);
            }
            _controls.Enable();
        }

        private void OnDisable()
        {
            _controls.Disable();
        }

        public void OnSpeedChange(InputAction.CallbackContext context)
        {
            ChangeScrollEvent?.Invoke(context.ReadValue<float>());
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            if(context.performed)
                AttackEvent?.Invoke();
        }

        public void OnMoveDirection(InputAction.CallbackContext context)
        {
            Vector2 dir = context.ReadValue<Vector2>();
            MovePlayerEvent?.Invoke(dir);
        }
    }
}