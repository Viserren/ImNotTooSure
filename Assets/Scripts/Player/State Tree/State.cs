using UnityEngine;

namespace Player.State_Tree
{
    public abstract class State : MonoBehaviour
    {
        private StateTree _context;

        public StateTree Context
        {
            get { return _context; }
        }

        [SerializeField] private string _name;

        public string Name
        {
            get { return _name; }
        }

        public abstract void SetupState();

        public void StartState(StateTree context)
        {
            _context = context;
            Debug.Log($"Entered: {_name}");
            StartState();
        }

        public abstract void StartState();
        public abstract void StopState();
        public abstract void UpdateState();
        public abstract void FixedUpdateState();

    }
}