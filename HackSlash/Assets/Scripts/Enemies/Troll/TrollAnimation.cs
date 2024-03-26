using UnityEngine;

namespace Enemies.Troll
{
    [RequireComponent(typeof(TrollAttack))]
    public class TrollAnimation : CharacterAnimationBase
    {

        private TrollAttack _trollAttack;
        
        protected override void Start()
        {
            _trollAttack = GetComponent<TrollAttack>();
            
            base.Start();
        }

        private void Update()
        {
            HandleRunAnimation(_trollAttack.AttackStarted);
            HandleAttackAnimation(_trollAttack.AttackStarted);
        }
    }
}
