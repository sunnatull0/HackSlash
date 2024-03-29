using System.Collections;
using Player;
using UnityEngine;

namespace Enemies.TrollBoss
{
    [RequireComponent(typeof(TrollBossBehaviour))]
    public class TrollBossAttack : SurfaceAttack
    {
        private TrollBossBehaviour _trollBossBehaviour;

        [HideInInspector] public bool JumpAttackStarted;
        [HideInInspector] public bool isWaiting;

        [SerializeField] private float _jumpAttackDelay = 5f;
        [SerializeField] private float _delayAfterJumpAttack;

        private float _nextJumpAttackTime;


        private void Start()
        {
            _trollBossBehaviour = GetComponent<TrollBossBehaviour>();
            isWaiting = false;
            ExtendAttackDelay();
        }


        public void JumpAttackDamage(PlayerBehaviour playerBehaviour)
        {
            //var playerHealth = playerBehaviour.GetComponent<Health>();
            Debug.Log("DamagePlayer!");
            isWaiting = true;
            StartCoroutine(ResetAfterDelay());
        }

        public void FinishJumpAttack()
        {
            JumpAttackStarted = false;
            ExtendAttackDelay();
        }

        private IEnumerator ResetAfterDelay()
        {
            yield return new WaitForSeconds(_delayAfterJumpAttack);
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