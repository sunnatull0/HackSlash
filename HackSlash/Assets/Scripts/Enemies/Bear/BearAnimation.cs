using UnityEngine;

namespace Enemies.Bear
{
    [RequireComponent(typeof(BearAttack))]
    public class BearAnimation : CharacterAnimationBase
    {

        private BearAttack _bearAttack;

        protected override void Start()
        {
            base.Start();

            _bearAttack = GetComponent<BearAttack>();
        }

        private void Update()
        {
            HandleRunAnimation(_bearAttack.AttackStarted);
            HandleAttackAnimation(_bearAttack.AttackStarted);
        }
    }
}
