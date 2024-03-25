using UnityEngine;

namespace Enemies.Orc
{
    [RequireComponent(typeof(OrcAttack))]
    public class OrcAnimation : CharacterAnimationBase
    {
        
        private OrcAttack _orcAttack;


        protected override void Start()
        {
            _orcAttack = GetComponent<OrcAttack>();

            base.Start();
        }

        private void Update()
        {
            HandleRunAnimation(_orcAttack.AttackStarted);
            HandleAttackAnimation(_orcAttack.AttackStarted);
        }
    }
}
