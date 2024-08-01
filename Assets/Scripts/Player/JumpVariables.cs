using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    [System.Serializable]
    public class JumpVariables
    {
        [Tooltip("This is the height that the jump will aim to reach. 1 Being the default value.")]
        [SerializeField] float _maxJumpHeight = 1f;
        //[Tooltip("Effects how long you are in the air for")]
        //[Min(1f)]
        //[SerializeField] float _JumpTimeMultiplier;

        public float MaxJumpHeight { get {  return _maxJumpHeight; } set { _maxJumpHeight = value; } }
        //public float JumpTimeMultiplier { get { return _JumpTimeMultiplier; } set { _JumpTimeMultiplier = value; } }
        //
        // public class JumpValues
        // {
        //     float _timeToApex;
        //     float _initialGravity;
        //     float _initialJumpVelocity;
        //     
        //     public float TimeToApex
        //     {
        //         get { return _timeToApex; }
        //     }
        //     
        //     public float InitialGravity
        //     {
        //         get { return _initialGravity; }
        //     }
        //     
        //     public float InitialJumpVelocity
        //     {
        //         get { return _initialJumpVelocity; }
        //     }
        //
        //     public JumpValues(float maxJumpTimeMultiplier, float maxJumpHeight, float maxJumpTime = .75f)
        //     {
        //         this._timeToApex = maxJumpTime / 5f;
        //         this._initialGravity = (-1 * (maxJumpHeight)) / Mathf.Pow((_timeToApex * maxJumpTimeMultiplier), 2);
        //         this._initialJumpVelocity = (2 * (maxJumpHeight)) / (_timeToApex * maxJumpTimeMultiplier);
        //     }
        // }
    }
}