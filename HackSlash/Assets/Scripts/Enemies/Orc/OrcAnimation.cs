using UnityEngine;

namespace Enemies.Orc
{
    [RequireComponent(typeof(OrcAttack))]
    [RequireComponent(typeof(OrcBehaviour))]
    public class OrcAnimation : CharacterAnimationBase
    {
        
        private OrcAttack _orcAttack;
        private OrcBehaviour _orcBehaviour;


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
            if (!_orcBehaviour.IsGrounded())
            {
                animator.SetBool("isFalling", true);   
            }
            else
            {
                animator.SetBool("isFalling", false);   
            }
        }
    }
}
