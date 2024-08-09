using UnityEngine;

namespace Enemies.Boar
{
    [RequireComponent(typeof(BoarAttack))]
    public class BoarAnimation : CharacterAnimationBase
    {
        
        private BoarAttack _boarAttack;
        [SerializeField] private Animator _hitAnimator;
        [SerializeField] private GameObject _hitEffect;
        
        protected override void Start()
        {
            base.Start();
            
            _boarAttack = GetComponent<BoarAttack>();
        }

        private void Update()
        {
            HandleRunAnimation(_boarAttack.IsAttacking);
            HandleAttackAnimation(_boarAttack.IsAttacking);
        }

        public void PlayHitEffectAnimation(Vector2 pos)
        {
            _hitEffect.transform.position = pos;
            _hitAnimator.SetTrigger("Hit");
        }
        
    }
}