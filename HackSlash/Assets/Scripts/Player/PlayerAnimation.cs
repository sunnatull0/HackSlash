using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(PlayerBehaviour))]
    public class PlayerAnimation : MonoBehaviour
    {
    
        private PlayerBehaviour _playerBehaviour;
        private Rigidbody2D _rb;
        private Animator _animator;
        private const string RunParameter = "isRunning";
        private const string JumpParameter = "isJumping";
        private const string AttackParameter = "isAttacking";
        private const string BeingPushedParameter = "beingPushed";

        private bool _wasRunning;
        private bool _wasBeingPushed;



        private void Awake()
        {
            _playerBehaviour = GetComponent<PlayerBehaviour>();
            _animator = GetComponentInChildren<Animator>();
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            EventManager.OnAttack += PlayAttackingAnimation;
            EventManager.OnJump += PlayJumpingAnimation;
            EventManager.OnLand += OnLandActions;
            EventManager.OnAttackFinish += ResetAttackAnimation;
        }

        private void OnDestroy()
        {
            EventManager.OnAttack -= PlayAttackingAnimation;
            EventManager.OnJump -= PlayJumpingAnimation;
            EventManager.OnLand -= OnLandActions;
            EventManager.OnAttackFinish -= ResetAttackAnimation;
        }


        private void Update()
        {
            PlayRunningAnimation();
            PlayPushingAnimation();
        }

        private void OnLandActions()
        {
            ResetJumpAnimation();
        }

        

        private void PlayRunningAnimation()
        {
            bool isRunning = _rb.velocity.magnitude > 0f && _playerBehaviour.IsGrounded();
            
            if (isRunning == _wasRunning) return;
            
            
            _animator.SetBool(RunParameter, isRunning);
            _wasRunning = isRunning;
        }

        private void PlayJumpingAnimation()
        {
            _animator.SetBool(JumpParameter, true);
        }

        private void PlayAttackingAnimation()
        {
            _animator.SetBool(AttackParameter, true);
        }

        private void PlayPushingAnimation()
        {
            bool isBeingPushed = _playerBehaviour.BeingPushed;

            if (isBeingPushed == _wasBeingPushed) return;
            
            _animator.SetBool(BeingPushedParameter, isBeingPushed);
            _wasBeingPushed = isBeingPushed;
        }

        

        private void ResetJumpAnimation()
        {
            _animator.SetBool(JumpParameter, false);
        }
    
        private void ResetAttackAnimation()
        {
            _animator.SetBool(AttackParameter, false);
        }
    }
}