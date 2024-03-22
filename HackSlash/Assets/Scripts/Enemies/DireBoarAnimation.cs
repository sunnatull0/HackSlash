using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(DireBoar))]
    public class DireBoarAnimation : MonoBehaviour
    {
        private DireBoar _direBoar;
        private Animator _animator;
        private Rigidbody2D _rb;
        private readonly int _isRunning = Animator.StringToHash("isRunning");
        private readonly int _isAttacking = Animator.StringToHash("isAttacking");

        private bool _previousRunningState;
        private bool _previousAttackingState;

        private void Awake()
        {
            _direBoar = GetComponent<DireBoar>();
            _animator = GetComponent<Animator>();
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            var isRunning = _rb.velocity.magnitude > 0f && !_direBoar.IsAttacking;

            // Only update animator parameters if the running or attacking state has changed
            if (isRunning != _previousRunningState)
            {
                _animator.SetBool(_isRunning, isRunning);
                _previousRunningState = isRunning;
            }

            if (_direBoar.IsAttacking != _previousAttackingState)
            {
                _animator.SetBool(_isAttacking, _direBoar.IsAttacking);
                _previousAttackingState = _direBoar.IsAttacking;
            }
        }
    }
}