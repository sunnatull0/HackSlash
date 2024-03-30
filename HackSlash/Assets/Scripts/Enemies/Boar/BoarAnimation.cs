using UnityEngine;

namespace Enemies.Boar
{
    [RequireComponent(typeof(BoarAttack))]
    public class BoarAnimation : CharacterAnimationBase
    {
        
        private BoarAttack _boarAttack;
        
        
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
    }
}