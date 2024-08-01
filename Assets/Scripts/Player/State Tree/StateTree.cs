using UnityEngine;
using Utilities;

namespace Player.State_Tree
{
    public class StateTree : MonoBehaviour
    {
        Rigidbody _rigidbody;
        [SerializeField] private State _defaultState;
        private StateTree _stateTree;
        private State _currentState;

        #region Movement Variables

        private float _speed;
        private Vector3 _decayMovement;
        Vector2 _currentMovementInput;
        Vector3 _currentMovement;
        Vector3 _cameraRelativeMovement;
        bool _isMovementPressed = false;
        bool _isRunPressed = false;
        bool _isJumpPressed = false;
        float _rotationFactorPerFrame = 15;
        [SerializeField] private float _gravityMultiplier = 1.05f;
        private bool _isGrounded = false;
        [SerializeField] private LayerMask _groundLayers;
        [SerializeField] private float _groundCheckDistance = 1.15f;

        #endregion

        public Rigidbody Rigidbody
        {
            get { return _rigidbody; }
        }

        #region Bools

        public bool IsGrounded
        {
            get { return _isGrounded; }
        }

        public bool IsRunPressed
        {
            get { return _isRunPressed; }
            set { _isRunPressed = value; }
        }

        public bool IsMovementPressed
        {
            get { return _isMovementPressed; }
            set { _isMovementPressed = value; }
        }

        public bool IsJumpPressed
        {
            get { return _isJumpPressed; }
            set { _isJumpPressed = value; }
        }

        #endregion

        #region Movement

        public Vector3 CurrentMovement
        {
            get { return _currentMovement; }
            set { _currentMovement = value; }
        }

        // public Vector2 CurrentMovementInput
        // {
        //     get { return _currentMovementInput; }
        //     set { _currentMovementInput = value; }
        // }

        #endregion

        private void Awake()
        {
            _stateTree = GetComponent<StateTree>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            _currentState.UpdateState();
        }

        private void FixedUpdate()
        {
            _currentState.FixedUpdateState();
            _rigidbody.velocity = ApplyGravity();
            // Send Raycast down to check if player is grounded
            RaycastHit hit;
            Debug.DrawRay(transform.position, Vector3.down * _groundCheckDistance, Color.red);
            if (Physics.SphereCast(transform.position, .5f, Vector3.down, out hit, _groundCheckDistance, _groundLayers)) _isGrounded = true;
            else _isGrounded = false;
        }

        private void OnDrawGizmos()
        {
            RaycastHit hit;
            if (Physics.SphereCast(transform.position, .5f, Vector3.down, out hit, _groundCheckDistance, _groundLayers))
                Gizmos.DrawSphere(hit.point, .5f);
        }

        public Vector3 Move(float speed, float speedDecay = 1.25f)
        {
            _speed = _isMovementPressed ? speed : _speed;
            _decayMovement = _isMovementPressed ? _currentMovement : _decayMovement;

            _decayMovement = _speed <= 0.0001f ? Vector3.zero : _decayMovement;

            Vector3 velocity = (IsMovementPressed ? new Vector3(speed, 0, speed) : new Vector3(_speed /= speedDecay, 0, _speed /= speedDecay)) * Rigidbody.mass;

            Vector3 appliedVelocity = new Vector3(_decayMovement.x * velocity.x, velocity.y, _decayMovement.z * velocity.z);
            Vector3 cameraAppliedVelocity = CameraUtils.ConvertToCameraSpace(appliedVelocity);
            
            Debug.Log($"Applied Velocity: {appliedVelocity} \n Camera Applied Velocity: {cameraAppliedVelocity}");

            Rigidbody.AddForce(cameraAppliedVelocity, ForceMode.Impulse);

            float x = Mathf.Min(Mathf.Abs(Rigidbody.velocity.x), speed) * Mathf.Sign(Rigidbody.velocity.x);
            float z = Mathf.Min(Mathf.Abs(Rigidbody.velocity.z), speed) * Mathf.Sign(Rigidbody.velocity.z);

            return new Vector3(x, Rigidbody.velocity.y, z);
        }

        Vector3 ApplyGravity()
        {
            float g = Mathf.Min(Mathf.Abs(_rigidbody.velocity.y), 50) * Mathf.Sign(_rigidbody.velocity.y);
            g = _rigidbody.velocity.y < 0 ? g * _gravityMultiplier : g;
            return new Vector3(_rigidbody.velocity.x, g, _rigidbody.velocity.z);
        }

        private void Start()
        {
            ChangeState("Idle");
        }

        public void ChangeState(string newState)
        {
            State foundState = GetComponentOrNull(newState);
            if (!foundState)
            {
                Debug.Log($"Error changing to {newState} in {name} switch to {_defaultState} instead.");
                ChangeState(_defaultState.Name);
                return;
            }

            if (foundState == _currentState)
            {
                return;
            }

            SetState(foundState);
        }

        void SetState(State newState)
        {
            if (_currentState) _currentState.StopState();
            _currentState = newState;
            _currentState.StartState(this);
        }

        State GetComponentOrNull(string newStateName)
        {
            foreach (State lookingAtState in GetComponents<State>())
            {
                if (lookingAtState.Name == newStateName)
                {
                    return lookingAtState;
                }
            }

            return null;
        }
    }
}