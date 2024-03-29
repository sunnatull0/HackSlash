using UnityEngine;

namespace Enemies.TrollBoss
{
    public class TrollBossAnimation : CharacterAnimationBase
    {
        private TrollBossAttack _trollAttack;
        private readonly int JumpingParam = Animator.StringToHash("isJumping");


        protected override void Start()
        {
            _trollAttack = GetComponent<TrollBossAttack>();

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
