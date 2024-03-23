using UnityEngine;

namespace Enemies.DireBoar
{
    [RequireComponent(typeof(DireBoarAttack))]
    public class DireBoarAnimation : CharacterAnimationBase
    {
        
        private DireBoarAttack _direBoar;
        
        
        protected override void Start()
        {
            base.Start();
            
            _direBoar = GetComponent<DireBoarAttack>();
        }

        private void Update()
        {
            HandleRunAnimation(_direBoar.IsAttacking);
            HandleAttackAnimation(_direBoar.IsAttacking);
        }
    }
}