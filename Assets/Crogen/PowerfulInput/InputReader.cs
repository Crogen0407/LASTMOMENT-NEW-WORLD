using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Crogen.PowerfulInput
{
    [CreateAssetMenu(fileName = "InputReader", menuName = "Crogen/InputReader", order = 0)]
    public class InputReader : ScriptableObject, Controls.IPlayerActions, Controls.IUIActions
    {
        public Vector2 mousePositionClampSize = new Vector2(1200f, 1200f);
        #region Input Event

        public event Action<Vector3> StartMoveDirectionEvent;
        public event Action EndMoveDirectionEvent;
        public event Action StartRunEvent;
        public event Action SpeedUpEvent;
        public event Action SpeedDownEvent;
        public event Action InteractionEvent;
        public event Action ResetDirectionEvent;
        
        //Attack
        public event Action AttackStartEvent;
        public event Action AttackEndEvent;
        
        public event Action MouseClickEvent; 
        public event Action<Vector2> MoveMouseEvent; 
    
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
            if(context.started)
                StartRunEvent?.Invoke();
            if (context.performed)
                SpeedUpEvent?.Invoke();
            if(context.canceled)
                SpeedDownEvent?.Invoke();
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
            if(context.performed)
                StartMoveDirectionEvent?.Invoke(position);
            if(context.canceled)
                EndMoveDirectionEvent?.Invoke();
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

        public void OnMoveMouse(InputAction.CallbackContext context)
        {
            Vector2 mousePosition = context.ReadValue<Vector2>();
            Vector2 screenSize = new Vector2(Screen.width, Screen.height);
            
            Vector2 minOffset = (screenSize - mousePositionClampSize) * 0.5f;
            Vector2 maxOffset = minOffset + mousePositionClampSize;
            mousePosition = MathExtension.VectorClamp(mousePosition, minOffset, maxOffset);
            
            MoveMouseEvent?.Invoke(mousePosition);
        }
        
        
    }
}