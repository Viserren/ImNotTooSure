using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player.State_Tree.States
{
    public class Jump : State
    {
        #region Jumping variables

        [Header("Jumping")] [SerializeField] private JumpVariables[] _jumpVariables;
        private bool _resetJumping = false;
        private int _jumpCount = 0;
        [SerializeField] float _airSpeed = 1.6f;
        //private List<JumpVariables.JumpValues> _JumpValues = new List<JumpVariables.JumpValues>();

        #endregion

        public int JumpCount
        {
            get { return _jumpCount; }
            set { _jumpCount = value; }
        }

        // public List<JumpVariables.JumpValues> JumpValues
        // {
        //     get { return _JumpValues; }
        // }

        public override void SetupState()
        {
            //SetUpJumpVariables();
        }

        public override void StartState()
        {
            HandleJump();
        }

        public override void StopState()
        {
            // // Set require new jump = true
            // Context.RequireNewJumpPressed = true;
        }

        public override void UpdateState()
        {
            if (!Context.IsGrounded)
            {
                _resetJumping = false;
            }
            
            if (Context.IsGrounded && Context.IsJumpPressed)
            {
                HandleJump();
            }
            
            if (Context.IsGrounded && !Context.IsJumpPressed)
            {
                Context.ChangeState("Idle");
            }
        }

        public override void FixedUpdateState()
        {
            Context.Rigidbody.velocity = Context.Move(_airSpeed,1.005f);
        }

        void HandleJump()
        {
            if (!_resetJumping)
            {
                _resetJumping = true;
                // Jump
                if (_jumpVariables[_jumpCount].MaxJumpHeight <= 0)
                {
                    Debug.LogError("Jump height must be greater than zero.");
                    return;
                }

                // Calculate the initial velocity required to reach the desired height
                float jumpVelocity = Mathf.Sqrt(-2 * Physics.gravity.y * _jumpVariables[_jumpCount].MaxJumpHeight);

                // Context.Rigidbody.velocity = new Vector3(Context.Rigidbody.velocity.x, 0, Context.Rigidbody.velocity.z); // Reset current Y velocity
                Context.Rigidbody.AddForce(Vector3.up * (jumpVelocity * Context.Rigidbody.mass), ForceMode.Impulse);
            }
        }
    }
}