using UnityEngine;

namespace Enemies.Troll
{
    [RequireComponent(typeof(TrollAttack))]
    public class TrollAnimation : CharacterAnimationBase
    {
        private TrollAttack _trollAttack;
        private readonly int JumpingParam = Animator.StringToHash("isJumping");

        protected override void Start()
        {
            _trollAttack = GetComponent<TrollAttack>();

            base.Start();
        }

        private void Update()
        {
            HandleRunAnimation(_trollAttack.AttackStarted);
            HandleAttackAnimation(_trollAttack.AttackStarted);
            HandleJumpAttackAnimation();
        }

        

        private bool _previousJumpingState;

        private void HandleJumpAttackAnimation()
        {
            if (_trollAttack.JumpAttackStarted == _previousJumpingState) return;
            
            
            animator.SetBool(JumpingParam, _trollAttack.JumpAttackStarted);
            _previousJumpingState = _trollAttack.JumpAttackStarted;
        }
    }
}