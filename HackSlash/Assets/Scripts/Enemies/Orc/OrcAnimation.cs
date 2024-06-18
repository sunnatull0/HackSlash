using UnityEngine;

namespace Enemies.Orc
{
    [RequireComponent(typeof(OrcAttack))]
    [RequireComponent(typeof(OrcBehaviour))]
    public class OrcAnimation : CharacterAnimationBase
    {
        private OrcAttack _orcAttack;
        private OrcBehaviour _orcBehaviour;

        private readonly int FallingParam = Animator.StringToHash("isFalling");

        private bool _previousFallingState;


        protected override void Start()
        {
            _orcAttack = GetComponent<OrcAttack>();
            _orcBehaviour = GetComponent<OrcBehaviour>();

            base.Start();
        }

        private void Update()
        {
            HandleRunAnimation(_orcAttack.AttackStarted);
            HandleAttackAnimation(_orcAttack.AttackStarted);
            HandleFallingAnimation();
        }

        private void HandleFallingAnimation()
        {
            bool isFalling = !_orcBehaviour.IsGrounded();

            if (isFalling == _previousFallingState)
                return;

            SFXManager.Instance.PlaySFX(isFalling ? SFXType.OrcJump : SFXType.OrcLand);

            animator.SetBool(FallingParam, isFalling);
            _previousFallingState = isFalling;
        }
    }
}