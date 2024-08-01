using System;
using System.Collections.Generic;
using Player.State_Tree.States;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.State_Tree
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(StateTree))]
    public class PlayerController : MonoBehaviour
    {
        // Reference variables
        private StateTree _stateTree;
        PlayerInput _playerInput;
        Animator _animator;

        //[SerializeField] private Transform _camera;
        [SerializeField] private float _sensitivity = 1f;
        private float _xRotation;
        private float _yRotation;

        #region Animation Variables

        int _isWalkingHash;
        int _isRunningHash;
        int _isJumpingHash;
        int _jumpCountHash;
        int _isFallingHash;

        #endregion

        #region Getters and Setters

        #region Animation

        public Animator Animator
        {
            get { return _animator; }
        }

        public int IsWalkingHash
        {
            get { return _isWalkingHash; }
        }

        public int IsRunningHash
        {
            get { return _isRunningHash; }
        }

        public int IsJumpingHash
        {
            get { return _isJumpingHash; }
        }

        public int IsFallingHash
        {
            get { return _isFallingHash; }
        }

        public int JumpCountHash
        {
            get { return _jumpCountHash; }
        }

        #endregion

        #endregion

        private void Awake()
        {
            // Initially set reference variables
            _playerInput = new PlayerInput();
            _stateTree = GetComponent<StateTree>();
            _animator = GetComponent<Animator>();

            // Set the animation hash variables
            _isWalkingHash = Animator.StringToHash("isWalking");
            _isRunningHash = Animator.StringToHash("isRunning");
            _isJumpingHash = Animator.StringToHash("isJumping");
            _jumpCountHash = Animator.StringToHash("jumpCount");
            _isFallingHash = Animator.StringToHash("isFalling");

            foreach (var states in GetComponents<State>())
            {
                states.SetupState();
            }
        }

        private void OnEnable()
        {
            // Enable the input system
            _playerInput.Player.Enable();
            _playerInput.Player.Move.performed += OnMove;
            _playerInput.Player.Move.canceled += OnMove;

            // _playerInput.Player.Run.performed += OnRun;
            // _playerInput.Player.Run.canceled += OnRun;
            //
            _playerInput.Player.Jump.started += OnJump;
            _playerInput.Player.Jump.canceled += OnJump;

            // _playerInput.Player.Look.performed += GetMouseDelta;
            // _playerInput.Player.Look.canceled += GetMouseDelta;
        }

        private void OnDisable()
        {
            // Disable the input system
            _playerInput.Player.Disable();
            _playerInput.Player.Move.performed -= OnMove;
            _playerInput.Player.Move.canceled -= OnMove;

            // _playerInput.Player.Run.performed -= OnRun;
            // _playerInput.Player.Run.canceled -= OnRun;
            //
            _playerInput.Player.Jump.started -= OnJump;
            _playerInput.Player.Jump.canceled -= OnJump;

            // _playerInput.Player.Look.performed -= GetMouseDelta;
            // _playerInput.Player.Look.canceled -= GetMouseDelta;
        }

        private void LateUpdate()
        {
            
        }

        void OnMove(InputAction.CallbackContext ctx)
        {
            // Read the value of the input action
            Vector2 currentMovementInput = ctx.ReadValue<Vector2>();

            Vector3 currentMovement = new Vector3(currentMovementInput.x, 0, currentMovementInput.y);

            _stateTree.CurrentMovement = currentMovement;

            _stateTree.IsMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
        }

        void OnRun(InputAction.CallbackContext ctx)
        {
            _stateTree.IsRunPressed = ctx.ReadValueAsButton();
        }

        void OnJump(InputAction.CallbackContext ctx)
        {
            _stateTree.IsJumpPressed = ctx.ReadValueAsButton();
        }

        // private void GetMouseDelta(InputAction.CallbackContext ctx)
        // {
        //     float mouseX = ctx.ReadValue<Vector2>().x * _sensitivity * Time.deltaTime;
        //     float mouseY = ctx.ReadValue<Vector2>().y * _sensitivity * Time.deltaTime;
        //
        //     _yRotation += mouseX;
        //
        //     _xRotation -= mouseY;
        //
        //     _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);
        //     
        //     _camera.localRotation = Quaternion.Euler(_xRotation, _yRotation, 0);
        //     // transform.Rotate(Vector3.up * mouseX);
        // }
    }
}