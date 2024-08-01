using UnityEngine;

namespace Player.State_Tree.States
{
    public class Idle : State
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
            if (Context.IsMovementPressed) Context.ChangeState("Walk");
            if (Context.IsJumpPressed && Context.IsGrounded) Context.ChangeState("Jump");
            //if (!Context.IsGrounded) Context.ChangeState("Fall");
        }

        public override void FixedUpdateState()
        {
            Context.Rigidbody.velocity = Context.Move(1.6f);
        }
    }
}