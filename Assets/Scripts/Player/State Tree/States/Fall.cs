using UnityEngine;

namespace Player.State_Tree.States
{
    public class Fall : State

    {
        public override void SetupState()
        {
            
        }

        public override void StartState()
        {
            
        }

        public override void StopState()
        {
            
        }

        public override void UpdateState()
        {
            float fallMultiplier = 2;
            Vector3 gravity = Vector3.up * Physics.gravity.y;
            gravity.y += Context.Rigidbody.velocity.y;
            Context.Rigidbody.AddRelativeForce(gravity * (fallMultiplier * Time.deltaTime));
            if (Context.IsGrounded && !Context.IsJumpPressed)
            {
                Context.ChangeState("Idle");
            }
        }

        public override void FixedUpdateState()
        {
            throw new System.NotImplementedException();
        }
    }
}