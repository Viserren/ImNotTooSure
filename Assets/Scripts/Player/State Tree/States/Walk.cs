using UnityEngine;

namespace Player.State_Tree.States
{
    public class Walk : State
    {
        [Header("Movement")] 
        [SerializeField] float _runMultiplier = 2f;
        [SerializeField] float _walkSpeed = 1.6f;
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
            if(Context.IsJumpPressed) Context.ChangeState("Jump");
            //if (!Context.IsGrounded) Context.ChangeState("Fall");
            if(!Context.IsMovementPressed) Context.ChangeState("Idle");
        }
        
        public override void FixedUpdateState()
        {
            Context.Rigidbody.velocity = Context.Move(_walkSpeed);
        }
    }
}