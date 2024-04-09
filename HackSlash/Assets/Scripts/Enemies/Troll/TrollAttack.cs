using System.Collections;
using UnityEngine;

namespace Enemies.Troll
{
    public class TrollAttack : SurfaceAttack
    {
        

        [HideInInspector] public bool JumpAttackStarted;
        [HideInInspector] public bool isWaiting;
        
        [SerializeField] private float _jumpAttackDelay = 5f;
        [SerializeField] private float _waitingAfterJumpAttack;
        
        private float _nextJumpAttackTime;


        protected override void Start()
        {
            base.Start();
            isWaiting = false;
            ExtendAttackDelay();
        }


        public void JumpAttackDamage(Health targetHealth)
        {
            Damage(targetHealth);
        }

        public void FinishJumpAttack()
        {
            JumpAttackStarted = false;
            ExtendAttackDelay();
        }

        public void StartWaiting()
        {
            isWaiting = true;
            StartCoroutine(ResetWaitingAfterDelay());
        }

        private IEnumerator ResetWaitingAfterDelay()
        {
            yield return new WaitForSeconds(_waitingAfterJumpAttack);
            isWaiting = false;
        }

        private void ExtendAttackDelay()
        {
            _nextJumpAttackTime = Time.time + _jumpAttackDelay;
        }
        
        public bool CanJumpAttack()
        {
            return Time.time >= _nextJumpAttackTime;
        }
    }
}